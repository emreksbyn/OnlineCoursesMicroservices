using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FreeCourse.Web.Models.CatalogServiceModels
{
    public class CourseUpdateInput
    {
        public string? Id { get; set; }

        [Display(Name = "Course Name")]
        [Required]
        public string? Name { get; set; }

        [Display(Name = "Course Description")]
        [Required]
        public string? Description { get; set; }

        [Display(Name = "Course Price")]
        [Required]
        public decimal Price { get; set; }

        public string? UserId { get; set; }
        public string? Picture { get; set; }
        public FeatureViewModel? Feature { get; set; }

        [Display(Name = "Course Category")]
        [Required]
        public string? CategoryId { get; set; }

        [Display(Name = "Course Picture")]
        public IFormFile PhotoFormFile { get; set; }
    }
}