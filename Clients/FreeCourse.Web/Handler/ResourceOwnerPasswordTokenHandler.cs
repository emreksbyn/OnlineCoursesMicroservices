using FreeCourse.Web.Exceptions;
using FreeCourse.Web.Services.Interfaces;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace FreeCourse.Web.Handler
{
    public class ResourceOwnerPasswordTokenHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IIdentityService _identityService;
        private readonly ILogger<ResourceOwnerPasswordTokenHandler> _logger;

        public ResourceOwnerPasswordTokenHandler(IHttpContextAccessor contextAccessor, IIdentityService identityService, ILogger<ResourceOwnerPasswordTokenHandler> logger)
        {
            _contextAccessor = contextAccessor;
            _identityService = identityService;
            _logger = logger;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            string accessToken = await _contextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                TokenResponse tokenResponse = await _identityService.GetAccessTokenByRefreshToken();

                if (tokenResponse != null)
                {
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenResponse.AccessToken);
                    response = await base.SendAsync(request, cancellationToken);
                }
            }

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new UnAuthorizeException();
            }

            return response;
        }
    }
}