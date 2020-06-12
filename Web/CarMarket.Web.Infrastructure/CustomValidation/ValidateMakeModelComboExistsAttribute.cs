﻿namespace CarMarket.Web.Infrastructure.CustomValidation
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    using CarMarket.Services.Data.Interfaces;

    using Microsoft.Extensions.DependencyInjection;

    [AttributeUsage(AttributeTargets.Property)]
    public class ValidateMakeModelComboExistsAttribute : ValidationAttribute
    {
        private const string InvalidErrorMessage = "Please choose a valid make and model combination from the dropdowns.";

        private readonly string makeId;

        public ValidateMakeModelComboExistsAttribute(string makeId)
        {
            this.makeId = makeId;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var modelId = (int)value;
            var property = validationContext.ObjectType.GetProperty(makeId);
            var makeIdAsInt = (int)property.GetValue(validationContext.ObjectInstance);
            var modelsService = validationContext.GetService<IModelsService>();
            bool isValid = Task.Run(async () => await modelsService.IsValidByMakeIdAndIdAsync(makeIdAsInt, modelId)).Result;
            return isValid
                ? ValidationResult.Success
                : new ValidationResult(InvalidErrorMessage);
        }
    }
}