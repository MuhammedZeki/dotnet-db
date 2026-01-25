using System.ComponentModel.DataAnnotations;

namespace dotnet_db.Models;



public class SliderModel
{

    [Display(Name = "Başlık")]
    [Required(ErrorMessage = "Başlık Giriniz")]
    [StringLength(20, ErrorMessage = "{0} için {2}-{1} Karakter aralığında giriniz.", MinimumLength = 3)]
    public string Title { get; set; } = null!;



    [Display(Name = "Aktif")]
    public bool IsActive { get; set; }



    [Display(Name = "Index")]
    public int Index { get; set; }
}