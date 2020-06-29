namespace CarMarket.Web.Infrastructure.CustomValidation
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using CarMarket.Services.Data.Interfaces;

    using Microsoft.Extensions.DependencyInjection;

    [AttributeUsage(AttributeTargets.Property)]
    public class ValidateModelNameAttribute : ValidationAttribute
    {
        private const string InvalidModelNameErrorMessage = "Model with the name {0} already exists";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var modelName = (string)value;
            var modelService = validationContext.GetService<IModelsService>();
            bool exists = modelService.ExistsByNameAsync(modelName).GetAwaiter().GetResult();
            return exists
                ? new ValidationResult(string.Format(InvalidModelNameErrorMessage, modelName))
                : ValidationResult.Success;
        }
    }
}
