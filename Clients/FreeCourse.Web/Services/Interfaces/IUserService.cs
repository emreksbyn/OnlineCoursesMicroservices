using FreeCourse.Web.Models.UserServiceModels;

namespace FreeCourse.Web.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserViewModel> GetUser();
    }
}