using System.ComponentModel.DataAnnotations;

namespace AppPay.Models
{
    public class FeaturesModel
    {
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Title { get; set; }
        [Required, MaxLength(100)]
        public string Description { get; set; }
        public string Icon { get; set; }
    }
}
