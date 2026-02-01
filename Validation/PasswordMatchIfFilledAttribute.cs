using System.ComponentModel.DataAnnotations;
using dotnet_db.Models;

namespace dotnet_db.Validation;

public class PasswordMatchIfFilledAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var model = (UserEditModel)validationContext.ObjectInstance;

        if (!string.IsNullOrEmpty(model.Password))
        {
            if (model.Password != model.ConfirmPassword)
            {
                return new ValidationResult("Şifreler birbiriyle uyuşmuyor!");
            }
        }

        return ValidationResult.Success;
    }
}