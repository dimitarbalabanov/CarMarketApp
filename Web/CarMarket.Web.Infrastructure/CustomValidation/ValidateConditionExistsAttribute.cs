namespace CarMarket.Web.Infrastructure.CustomValidation
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    using CarMarket.Services.Data.Interfaces;

    using Microsoft.Extensions.DependencyInjection;

    [AttributeUsage(AttributeTargets.Property)]
    public class ValidateConditionExistsAttribute : ValidationAttribute
    {
        private const string InvalidConditionErrorMessage = "Please choose a valid condition from the dropdown.";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var id = (int)value;
            var conditionsService = validationContext.GetService<IConditionsService>();
            bool isValid = Task.Run(async () => await conditionsService.IsValidByIdAsync(id)).Result;
            return isValid
                ? ValidationResult.Success
                : new ValidationResult(InvalidConditionErrorMessage);
        }
    }
}
