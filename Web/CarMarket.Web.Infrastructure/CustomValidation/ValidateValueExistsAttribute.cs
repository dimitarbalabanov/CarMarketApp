namespace CarMarket.Web.Infrastructure.CustomValidation
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using CarMarket.Services.Data.Interfaces;

    [AttributeUsage(AttributeTargets.Property)]
    public class ValidateValueExistsAttribute : ValidationAttribute
    {
        private const string InvalidErrorMessage = "Please choose a valid {0} from the dropdown.";
        private const string ServiceNameFormat = "I{0}Service";
        private const string AssemblyQualifiedNameFormat = "CarMarket.Services.Data.Interfaces.{0}";

        private readonly string entityName;

        public ValidateValueExistsAttribute(string entityName)
        {
            this.entityName = entityName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var name = this.entityName;
            var entityPlural = name.EndsWith('y') ? name.Replace("y", "ies") : name + "s";
            var serviceName = string.Format(ServiceNameFormat, entityPlural);

            var serviceAssembly = typeof(IHaveValidValue).Assembly;
            var serviceAssemblyQualifiedName = string.Format(AssemblyQualifiedNameFormat, serviceName);
            var serviceType = serviceAssembly.GetType(serviceAssemblyQualifiedName);
            if (serviceType == null)
            {
                throw new ArgumentException();
            }

            var service = (IHaveValidValue)validationContext.GetService(serviceType);
            var id = (int)value;
            bool isValid = service.IsValidByIdAsync(id).GetAwaiter().GetResult();

            if (!isValid)
            {
                var errorMsg = string.Format(InvalidErrorMessage, name.ToLower());
                return new ValidationResult(errorMsg);
            }

            return ValidationResult.Success;
        }
    }
}
