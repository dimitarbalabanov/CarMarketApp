namespace CarMarket.Web.Infrastructure.CustomValidation
{
    using System;
    using System.ComponentModel.DataAnnotations;

    [AttributeUsage(AttributeTargets.Property)]
    public class ValidateProductionYearAttribute : ValidationAttribute
    {
        private const string InvalidProductionYearMessage = "Car has to be already produced.";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var prodYear = (int)value;
            var currentYear = DateTime.UtcNow.Year;
            var isValid = prodYear <= currentYear;
            return isValid
                ? ValidationResult.Success
                : new ValidationResult(InvalidProductionYearMessage);
        }
    }
}
