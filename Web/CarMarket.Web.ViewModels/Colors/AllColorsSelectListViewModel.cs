namespace CarMarket.Web.ViewModels.Colors
{
    using System.Collections.Generic;

    public class AllColorsSelectListViewModel
    {
        public IEnumerable<ColorSelectListViewModel> Colors { get; set; }

        public int SelectedColorId { get; set; }
    }
}
