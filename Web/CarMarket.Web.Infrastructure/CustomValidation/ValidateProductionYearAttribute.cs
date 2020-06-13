namespace CarMarket.Web.Infrastructure.CustomValidation
{
    using System;
    using System.ComponentModel.DataAnnotations;

    [AttributeUsage(AttributeTargets.Property)]
    public class ValidateProductionYearAttribute : ValidationAttribute
    {
        private const string InvalidProductionYearErrorMessage = "Production year can be between {0} and {1}.";
        private const int ProductionYearMinValue = 1900;

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var prodYear = (int)value;
            var currentYear = DateTime.UtcNow.Year;
            var isValid = prodYear <= currentYear && prodYear >= ProductionYearMinValue;
            return isValid
                ? ValidationResult.Success
                : new ValidationResult(string.Format(InvalidProductionYearErrorMessage, ProductionYearMinValue, currentYear));
        }
    }
}
