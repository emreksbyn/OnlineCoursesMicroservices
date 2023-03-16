using System.ComponentModel.DataAnnotations;

namespace FreeCourse.Web.Models.OrderServiceModels
{
    public class CheckoutInfoInput
    {
        [Display(Name = "Province")]
        public string? Province { get; set; }
        
        [Display(Name = "District")]
        public string? District { get; set; }
        
        [Display(Name = "Street")]
        public string? Street { get; set; }
        
        [Display(Name = "Zip Code")]
        public string? ZipCode { get; set; }
        
        [Display(Name ="Address")]
        public string? Line { get; set; }
        
        [Display(Name ="Card Firstname Lastname")]
        public string CardName { get; set; }
        
        [Display(Name = "Card Number")]
        public string CardNumber { get; set; }
        
        [Display(Name = "Expiration (mm/YY)")]
        public string Expiration { get; set; }
        
        [Display(Name = "CVV")]
        public string CVV { get; set; }
    }
}