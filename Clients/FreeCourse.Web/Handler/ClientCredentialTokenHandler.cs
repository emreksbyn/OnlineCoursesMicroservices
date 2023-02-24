using FreeCourse.Web.Exceptions;
using FreeCourse.Web.Services.Interfaces;
using System.Net;
using System.Net.Http.Headers;

namespace FreeCourse.Web.Handler
{
    public class ClientCredentialTokenHandler : DelegatingHandler
    {
        private readonly IClientCredentialTokenService _clientCredentialTokenService;
        public ClientCredentialTokenHandler(IClientCredentialTokenService clientCredentialTokenService)
        {
            _clientCredentialTokenService = clientCredentialTokenService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            string parameter =  await _clientCredentialTokenService.GetToken();
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", parameter);

            var response = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode == HttpStatusCode.Unauthorized) throw new UnAuthorizeException();

            return response;
        }
    }
}