namespace CarMarket.Web.Infrastructure.CustomValidation
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    using CarMarket.Services.Data.Interfaces;

    using Microsoft.Extensions.DependencyInjection;

    [AttributeUsage(AttributeTargets.Property)]
    public class ValidateBodyExistsAttribute : ValidationAttribute
    {
        private const string InvalidBodyErrorMessage = "Please choose a valid body from the dropdown.";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var id = (int)value;
            var bodiesService = validationContext.GetService<IBodiesService>();
            bool isValid = Task.Run(async () => await bodiesService.IsValidByIdAsync(id)).Result;
            return isValid
                ? ValidationResult.Success
                : new ValidationResult(InvalidBodyErrorMessage);
        }
    }
}
