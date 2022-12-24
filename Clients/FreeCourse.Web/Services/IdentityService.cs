using FreeCourse.Shared.Dtos;
using FreeCourse.Web.Models;
using FreeCourse.Web.Services.Interfaces;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Shared.FreeCourse.Shared.Dtos;
using System.Globalization;
using System.Security.Claims;
using System.Text.Json;

namespace FreeCourse.Web.Services
{
    public class IdentityService : IIdentityService
    {
        // IdentityServer microservisine istek yollamak icin gerekli olacak.
        private readonly HttpClient _httpClient;

        // Bununla Cookie ye ulasacagiz.
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly ClientSettings _clientSettings;
        private readonly ServiceApiSettings _serviceApiSettings;

        public IdentityService(HttpClient httpClient, IHttpContextAccessor contextAccessor, IOptions<ClientSettings> clientSettings, IOptions<ServiceApiSettings> serviceApiSettings)
        {
            _httpClient = httpClient;
            _httpContextAccessor = contextAccessor;
            _clientSettings = clientSettings.Value; ;
            _serviceApiSettings = serviceApiSettings.Value;
        }

        public Task<TokenResponse> GetAccessTokenByRefreshToken()
        {
            throw new NotImplementedException();
        }

        public Task RevokeRefreshToken()
        {
            throw new NotImplementedException();
        }

        public async Task<Response<bool>> SignIn(SigninInput signinInput)
        {
            // https kullanmadigimiz icin ayar verdik.
            var discovery = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = _serviceApiSettings.BaseUri,
                Policy = new DiscoveryPolicy { RequireHttps = false }
            });

            if (discovery.IsError) throw discovery.Exception;

            PasswordTokenRequest passwordTokenRequest = new()
            {
                ClientId = _clientSettings.WebClientForUser.ClientId,
                ClientSecret = _clientSettings.WebClientForUser.ClientSecret,
                UserName = signinInput.Email,
                Password = signinInput.Password,
                Address = discovery.TokenEndpoint
            };

            TokenResponse token = await _httpClient.RequestPasswordTokenAsync(passwordTokenRequest);

            if (token.IsError)
            {
                string responseContent = await token.HttpResponse.Content.ReadAsStringAsync();

                // Buyuk kucuk harf duyarliligini kaldirdik. responseContent' i ErrorDto' ya cevirdik.
                ErrorDto errorDto = JsonSerializer.Deserialize<ErrorDto>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return Response<bool>.Fail(errorDto.Errors, 400);
            }

            UserInfoRequest userInfoRequest = new()
            {
                Token = token.AccessToken,
                Address = discovery.UserInfoEndpoint
            };

            UserInfoResponse userInfo = await _httpClient.GetUserInfoAsync(userInfoRequest);

            if (userInfo.IsError) throw userInfo.Exception;

            string authenticationScheme = CookieAuthenticationDefaults.AuthenticationScheme;

            ClaimsIdentity claimsIdentity = new(userInfo.Claims, authenticationScheme, "name", "role");

            ClaimsPrincipal claimsPrincipal = new(claimsIdentity);

            AuthenticationProperties authenticationProperties = new();

            authenticationProperties.StoreTokens(new List<AuthenticationToken>()
            {
                new AuthenticationToken{Name= OpenIdConnectParameterNames.AccessToken,Value= token.AccessToken},
                new AuthenticationToken{Name= OpenIdConnectParameterNames.RefreshToken,Value= token.RefreshToken},
                new AuthenticationToken{Name= OpenIdConnectParameterNames.ExpiresIn,Value= DateTime.Now.AddSeconds(token.ExpiresIn).ToString("o",CultureInfo.InvariantCulture)}
            });

            await _httpContextAccessor.HttpContext.SignInAsync(authenticationScheme, claimsPrincipal, authenticationProperties);

            return Response<bool>.Success(200);
        }
    }
}