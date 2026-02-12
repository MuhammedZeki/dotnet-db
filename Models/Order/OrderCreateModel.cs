using System.ComponentModel.DataAnnotations;
namespace dotnet_db.Models;



public class OrderCreateModel
{
    [Display(Name = "Ad Soyad")]
    [Required(ErrorMessage = "Lütfen adınızı ve soyadınızı giriniz.")]
    public string Fullname { get; set; } = null!;

    [Display(Name = "Telefon Numarası")]
    [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz.")]
    public string Phone { get; set; } = null!;

    [Display(Name = "Toplam Tutar")]
    public double TotalPrice { get; set; }

    [Display(Name = "Şehir")]
    [Required(ErrorMessage = "Lütfen şehir seçiniz.")]
    public string City { get; set; } = null!;

    [Display(Name = "Açık Adres")]
    [Required(ErrorMessage = "Lütfen tam adresinizi yazınız.")]
    public string Address { get; set; } = null!;

    [Display(Name = "Sipariş Notu")]
    public string? OrderNote { get; set; }

    [Display(Name = "Posta Kodu")]
    [Required(ErrorMessage = "Posta kodunu giriniz.")]
    public string PostalCode { get; set; } = null!;


    public string CartName { get; set; } = null!;
    public string CartNumber { get; set; } = null!;
    public string CartExpirationYear { get; set; } = null!;
    public string CartExpirationMonth { get; set; } = null!;
    public string CartCVV { get; set; } = null!;


}