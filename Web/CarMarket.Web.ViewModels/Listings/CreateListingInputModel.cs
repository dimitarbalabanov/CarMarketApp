namespace CarMarket.Web.ViewModels.Listings
{
    using System.ComponentModel.DataAnnotations;

    using CarMarket.Data.Models;
    using CarMarket.Web.Infrastructure.CustomValidation;
    using Microsoft.AspNetCore.Http;

    public class CreateListingInputModel
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

        private const string MakeRequiredErrorMessage = "Please choose a make.";
        private const string ModelRequiredErrorMessage = "Please choose a model.";
        private const string BodyRequiredErrorMessage = "Please choose a body type.";
        private const string TransmissionRequiredErrorMessage = "Please choose a transmission.";
        private const string FuelRequiredErrorMessage = "Please choose a fuel type.";
        private const string ConditionRequiredErrorMessage = "Please choose a condition.";
        private const string ColorRequiredErrorMessage = "Please choose a color.";
        private const string MainImageRequiredErrorMessage = "Please choose a main image.";
        private const string SecondaryImageRequiredErrorMessage = "Please choose a secondary image.";

        [Required(ErrorMessage = MakeRequiredErrorMessage)]
        [ValidateMakeExists]
        public int MakeId { get; set; }

        [Required(ErrorMessage = ModelRequiredErrorMessage)]
        [ValidateMakeModelComboExists(nameof(MakeId))]
        public int ModelId { get; set; }

        [Required(ErrorMessage = BodyRequiredErrorMessage)]
        [ValidateBodyExists]
        public int BodyId { get; set; }

        [Required(ErrorMessage = TransmissionRequiredErrorMessage)]
        [ValidateTransmissionExists]
        public int TransmissionId { get; set; }

        [Required(ErrorMessage = FuelRequiredErrorMessage)]
        [ValidateFuelExists]
        public int FuelId { get; set; }

        [Required(ErrorMessage = ConditionRequiredErrorMessage)]
        [ValidateConditionExists]
        public int ConditionId { get; set; }

        [Required(ErrorMessage = ColorRequiredErrorMessage)]
        [ValidateColorExists]
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

        [Required(ErrorMessage = MainImageRequiredErrorMessage)]
        [ValidateFileTypeAndSize]
        public IFormFile MainImage { get; set; }

        [Required(ErrorMessage = SecondaryImageRequiredErrorMessage)]
        [ValidateFileTypeAndSize]
        public IFormFile SecondaryImageA { get; set; }

        [Required(ErrorMessage = SecondaryImageRequiredErrorMessage)]
        [ValidateFileTypeAndSize]
        public IFormFile SecondaryImageB { get; set; }
    }
}
