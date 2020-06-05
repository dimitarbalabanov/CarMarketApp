namespace CarMarket.Web.Infrastructure.CustomValidation
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    using CarMarket.Services.Data.Interfaces;

    using Microsoft.Extensions.DependencyInjection;

    [AttributeUsage(AttributeTargets.Property)]
    public class ValidateColorExistsAttribute : ValidationAttribute
    {
        private const string InvalidColorErrorMessage = "Please choose a valid color from the dropdown.";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var id = (int)value;
            var colorsService = validationContext.GetService<IColorsService>();
            bool isValid = Task.Run(async () => await colorsService.IsValidByIdAsync(id)).Result;
            return isValid
                ? ValidationResult.Success
                : new ValidationResult(InvalidColorErrorMessage);
        }
    }
}
