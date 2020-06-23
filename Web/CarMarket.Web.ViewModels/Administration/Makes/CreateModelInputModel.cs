namespace CarMarket.Web.ViewModels.Administration.Makes
{
    using System.ComponentModel.DataAnnotations;

    using CarMarket.Web.Infrastructure.CustomValidation;

    public class CreateModelInputModel
    {
        private const string NameRequiredErrorMessage = "Please enter a name.";
        private const string NameLenghtErrorMessage = "{0} should be between {2} and {1} symbols long.";
        private const int NameMaxLenght = 50;
        private const int NameMinLenght = 1;

        [Required(ErrorMessage = NameRequiredErrorMessage)]
        [StringLength(NameMaxLenght, MinimumLength = NameMinLenght, ErrorMessage = NameLenghtErrorMessage)]
        [ValidateModelName]
        public string Name { get; set; }

        public int MakeId { get; set; }
    }
}
