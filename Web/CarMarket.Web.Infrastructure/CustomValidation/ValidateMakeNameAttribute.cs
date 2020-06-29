namespace CarMarket.Web.Infrastructure.CustomValidation
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using CarMarket.Services.Data.Interfaces;

    using Microsoft.Extensions.DependencyInjection;

    [AttributeUsage(AttributeTargets.Property)]
    public class ValidateMakeNameAttribute : ValidationAttribute
    {
        private const string InvalidMakeNameErrorMessage = "Make with the name {0} already exists";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var makeName = (string)value;
            var makesService = validationContext.GetService<IMakesService>();
            bool exists = makesService.ExistsByNameAsync(makeName).GetAwaiter().GetResult();
            return exists
                ? new ValidationResult(string.Format(InvalidMakeNameErrorMessage, makeName))
                : ValidationResult.Success;
        }
    }
}
