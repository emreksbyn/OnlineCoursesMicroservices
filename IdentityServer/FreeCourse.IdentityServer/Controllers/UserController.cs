using FreeCourse.IdentityServer.Dtos;
using FreeCourse.IdentityServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace FreeCourse.IdentityServer.Controllers
{
    [Authorize(LocalApi.PolicyName)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignupDto signupDto)
        {
            ApplicationUser user = new ApplicationUser
            {
                UserName = signupDto.UserName,
                Email = signupDto.Email,
                City = signupDto.City
            };

            IdentityResult result = await _userManager.CreateAsync(user, signupDto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors.Select(e => e.Description).ToList());
                //return BadRequest(Response<NoContent>.Fail(result.Errors.Select(e => e.Description).ToList(), 400));
            }

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            Claim userIdClaim = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub);
            if (userIdClaim == null) return BadRequest();

            ApplicationUser user = await _userManager.FindByIdAsync(userIdClaim.Value);
            if (user == null) return BadRequest();

            var userDto = new
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                City = user.City
            };
            return Ok(userDto);
        }
    }
}