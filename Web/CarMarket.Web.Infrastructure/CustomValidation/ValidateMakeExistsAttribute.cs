namespace CarMarket.Web.Infrastructure.CustomValidation
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    using CarMarket.Services.Data.Interfaces;

    using Microsoft.Extensions.DependencyInjection;

    [AttributeUsage(AttributeTargets.Property)]
    public class ValidateMakeExistsAttribute : ValidationAttribute
    {
        private const string InvalidMakeErrorMessage = "Please choose a valid make from the dropdown.";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var id = (int)value;
            var makesService = validationContext.GetService<IMakesService>();
            bool isValid = Task.Run(async () => await makesService.IsValidByIdAsync(id)).Result;
            return isValid
                ? ValidationResult.Success
                : new ValidationResult(InvalidMakeErrorMessage);
        }
    }
}
