namespace CarMarket.Web.ViewModels.Listings
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CarMarket.Data.Models;
    using CarMarket.Web.Infrastructure.CustomValidation;
    using CarMarket.Web.ViewModels.Images;

    public class EditListingInputModel
    {
        private const string PriceErrorMessage = "{0} cannot be below {1} BGN.";
        private const string PriceMinValue = "1";
        private const string PriceMaxValue = "79228162514264337593543950335";

        private const string MileageErrorMessage = "{0} can be between {1} and {2}.";
        private const int MileageMinValue = 0;
        private const int MileageMaxValue = 1000000;

        private const string HorsepowerErrorMessage = "{0} can be between {1} and {2}.";
        private const int HorsepowerMinValue = 1;
        private const int HorsepowerMaxValue = 5000;

        private const string DescriptionErrorMessage = "{0} should be at most {1} symbols long.";
        private const int DescriptionMaxLenght = 1500;

        private const string ModelRequiredErrorMessage = "Please choose a model.";
        private const string BodyRequiredErrorMessage = "Please choose a body type.";
        private const string TransmissionRequiredErrorMessage = "Please choose a transmission.";
        private const string FuelRequiredErrorMessage = "Please choose a fuel type.";
        private const string ConditionRequiredErrorMessage = "Please choose a condition.";
        private const string ColorRequiredErrorMessage = "Please choose a color.";

        public int Id { get; set; }

        [ValidateValueExists(nameof(Make))]
        public int MakeId { get; set; }

        public string MakeName { get; set; }

        [Required(ErrorMessage = ModelRequiredErrorMessage)]
        [ValidateMakeModelComboExists(nameof(MakeId))]
        public int ModelId { get; set; }

        [Required(ErrorMessage = BodyRequiredErrorMessage)]
        [ValidateValueExists(nameof(Body))]
        public int BodyId { get; set; }

        [Required(ErrorMessage = TransmissionRequiredErrorMessage)]
        [ValidateValueExists(nameof(Transmission))]
        public int TransmissionId { get; set; }

        [Required(ErrorMessage = FuelRequiredErrorMessage)]
        [ValidateValueExists(nameof(Fuel))]
        public int FuelId { get; set; }

        [Required(ErrorMessage = ConditionRequiredErrorMessage)]
        [ValidateValueExists(nameof(Condition))]
        public int ConditionId { get; set; }

        [Required(ErrorMessage = ColorRequiredErrorMessage)]
        [ValidateValueExists(nameof(Color))]
        public int ColorId { get; set; }

        [Required]
        [ValidateProductionYear]
        public int ProductionYear { get; set; }

        [Required]
        [Range(MileageMinValue, MileageMaxValue, ErrorMessage = MileageErrorMessage)]
        public int Mileage { get; set; }

        [Required]
        [Range(HorsepowerMinValue, HorsepowerMaxValue, ErrorMessage = HorsepowerErrorMessage)]
        public int Horsepower { get; set; }

        [Required]
        [Range(typeof(decimal), PriceMinValue, PriceMaxValue, ErrorMessage = PriceErrorMessage)]
        public decimal Price { get; set; }

        [MaxLength(DescriptionMaxLenght, ErrorMessage = DescriptionErrorMessage)]
        public string Description { get; set; }

        public IList<EditListingImageInputModel> InputImages { get; set; }
    }
}
