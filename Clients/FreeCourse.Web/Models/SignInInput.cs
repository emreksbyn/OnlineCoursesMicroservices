using System.ComponentModel.DataAnnotations;

namespace FreeCourse.Web.Models
{
    public class SigninInput
    {
        [Display(Name ="Your email")]
        public string? Email { get; set; }
        [Display(Name = "Your password")]
        public string? Password { get; set; }
        [Display(Name = "Remember me")]
        public bool IsRemember { get; set; }
    }
}