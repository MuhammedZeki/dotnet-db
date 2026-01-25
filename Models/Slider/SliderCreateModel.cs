using System.ComponentModel.DataAnnotations;

namespace dotnet_db.Models;


public class SliderCreateModel : SliderModel
{
    [Display(Name = "Açıklama")]
    public string? Description { get; set; }


    [Display(Name = "Resim")]
    [Required(ErrorMessage = "Resim Seçiniz")]
    public IFormFile Image { get; set; } = null!;
}


