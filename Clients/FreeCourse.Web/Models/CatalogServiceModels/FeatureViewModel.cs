using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FreeCourse.Web.Models.CatalogServiceModels
{
    public class FeatureViewModel
    {
        [Display(Name = "Course Duration")]
        public int Duration { get; set; }
    }
}