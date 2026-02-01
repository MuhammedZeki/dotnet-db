using System.ComponentModel.DataAnnotations;

namespace dotnet_db.Models;


public class AccountEditUserModel
{
    [Display(Name = "Kullanıcı Adı")]
    [Required(ErrorMessage = "Lütfen kullanıcı adı giriniz!")]
    public string FullName { get; set; } = null!;

    [Display(Name = "E-Posta")]
    [Required(ErrorMessage = "Lütfen e-posta adresinizi giriniz!")]
    [EmailAddress(ErrorMessage = "Lütfen geçerli bir e-posta adresi giriniz!")]
    public string Email { get; set; } = null!;


}