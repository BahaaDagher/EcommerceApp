using Ecommerce.Validations;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.ViewModel
{
    public class CreateBrandVM
    {
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [MinLength(3)]
        [MaxLength(20)]
        public string Name { get; set; }
        [MaxLength(250)]
        public string Description { get; set; }
        public bool Status { get; set; }
        [AllowedExtentions(new[] { ".png" ,".jpg" , ".jpeg" , ".gif"})]
        public IFormFile FormImg { get; set; }
    }
}
