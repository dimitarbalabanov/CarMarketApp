namespace CarMarket.Web.Infrastructure.CustomValidation
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    using CarMarket.Services.Data.Interfaces;

    using Microsoft.Extensions.DependencyInjection;

    [AttributeUsage(AttributeTargets.Property)]
    public class ValidateFuelExistsAttribute : ValidationAttribute
    {
        private const string InvalidFuelErrorMessage = "Please choose a valid fuel from the dropdown.";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var id = (int)value;
            var fuelsService = validationContext.GetService<IFuelsService>();
            bool isValid = Task.Run(async () => await fuelsService.IsValidByIdAsync(id)).Result;
            return isValid
                ? ValidationResult.Success
                : new ValidationResult(InvalidFuelErrorMessage);
        }
    }
}
