namespace CarMarket.Web.Infrastructure.CustomValidation
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    using CarMarket.Services.Data.Interfaces;

    using Microsoft.Extensions.DependencyInjection;

    [AttributeUsage(AttributeTargets.Property)]
    public class ValidateTransmissionExistsAttribute : ValidationAttribute
    {
        private const string InvalidTransmissionErrorMessage = "Please choose a valid transmission from the dropdown.";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var id = (int)value;
            var transmissionsService = validationContext.GetService<ITransmissionsService>();
            bool isValid = Task.Run(async () => await transmissionsService.IsValidByIdAsync(id)).Result;
            return isValid
                ? ValidationResult.Success
                : new ValidationResult(InvalidTransmissionErrorMessage);
        }
    }
}
