using System.ComponentModel.DataAnnotations;

namespace dotnet_db.Models;


public class SliderEditModel : SliderModel
{
    public int Id { get; set; }

    [Display(Name = "Açıklama")]
    public string? Description { get; set; }

    public IFormFile? ImageFile { get; set; }
    public string? ImageName { get; set; } = null!;

}



