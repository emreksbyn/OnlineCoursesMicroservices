using FreeCourse.Web.Models.IdentityServiceModels;
using IdentityModel.Client;
using Shared.FreeCourse.Shared.Dtos;

namespace FreeCourse.Web.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<Response<bool>> SignIn(SigninInput signinInput);

        // Refresh Token' i Cookie' den okuyacagimiz icin parametre almasina gerek yok.
        Task<TokenResponse> GetAccessTokenByRefreshToken();

        // Kullanici Log out olunca tokenlari silecek olan metot.
        Task RevokeRefreshToken();
    }
}