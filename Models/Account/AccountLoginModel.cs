using System.ComponentModel.DataAnnotations;

namespace dotnet_db.Models;


public class AccountLoginModel
{


    [Display(Name = "E-Posta")]
    [Required(ErrorMessage = "Lütfen e-posta adresinizi giriniz!")]
    [EmailAddress(ErrorMessage = "Lütfen geçerli bir e-posta adresi giriniz!")]
    public string Email { get; set; } = null!;


    [Display(Name = "Şifre")]
    [Required(ErrorMessage = "Lütfen bir şifre giriniz!")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;



    [Display(Name = "Beni Hatırla")]
    public bool RememberMe { get; set; } = true;

}