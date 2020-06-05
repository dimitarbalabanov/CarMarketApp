namespace CarMarket.Web.ViewModels.Listings
{
    using CarMarket.Web.Infrastructure.CustomValidation;
    using Microsoft.AspNetCore.Http;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class CreateListingInputModel
    {
        private const string PriceErrorMessage = "{0} can't be below {1} BGN!";
        private const string PriceMinValue = "1";
        private const string PriceMaxValue = "79228162514264337593543950335";

        private const string ProductionYearErrorMessage = "{0} should be between {1} and {2}!";
        private const int ProductionYearMinValue = 1900;
        private const int ProductionYearMaxValue = 2020;

        private const string MileageErrorMessage = "{0} should be between {1} and {2}!";
        private const int MileageMinValue = 0;
        private const int MileageMaxValue = 1000000;

        private const string HorsepowerErrorMessage = "{0} should be between {1} and {2}!";
        private const int HorsepowerMinValue = 1;
        private const int HorsepowerMaxValue = 5000;

        private const string DescriptionErrorMessage = "{0} should be at most {1} characters long!";
        private const int DescriptionMaxLenght = 1500;

        [Required(ErrorMessage = "Please choose a make")]
        [DisplayName("Make")]
        [ValidateMakeExists]
        public int MakeId { get; set; }

        [Required]
        [DisplayName("Model")]
        [ValidateMakeModelComboExists("MakeId")]
        public int ModelId { get; set; }

        [Required]
        [DisplayName("Body")]
        public int BodyId { get; set; }

        [Required]
        [DisplayName("Transmission")]
        public int TransmissionId { get; set; }

        [Required]
        [DisplayName("Fuel")]
        public int FuelId { get; set; }

        [Required]
        [DisplayName("Condition")]
        public int ConditionId { get; set; }

        [Required]
        [DisplayName("Color")]
        [ValidateValueExists("Color")]
        public int ColorId { get; set; }

        [Required]
        [DisplayName("Production year")]
        [Range(ProductionYearMinValue, ProductionYearMaxValue, ErrorMessage = ProductionYearErrorMessage)]
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

        [DataType(DataType.Upload)]
        public IEnumerable<IFormFile> UploadImages { get; set; }
    }
}
