using FreeCourse.IdentityServer.Models;
using IdentityModel;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreeCourse.IdentityServer.Services
{
    public class IdentityResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public IdentityResourceOwnerPasswordValidator(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            // We send email in UserName property.
            ApplicationUser existUser = await _userManager.FindByEmailAsync(context.UserName);
            if (existUser == null)
            {
                Dictionary<string, object> errors = new Dictionary<string, object>();
                errors.Add("errors", new List<string> { "Wrong email or password" });
                context.Result.CustomResponse = errors;
                return;
            }

            bool passwordCheck = await _userManager.CheckPasswordAsync(existUser, context.Password);
            if (passwordCheck == false)
            {
                Dictionary<string, object> errors = new Dictionary<string, object>();
                errors.Add("errors", new List<string> { "Wrong email or password" });
                context.Result.CustomResponse = errors;
                return;
            }

            context.Result = new GrantValidationResult(existUser.Id.ToString(), OidcConstants.AuthenticationMethods.Password);
        }
    }
}