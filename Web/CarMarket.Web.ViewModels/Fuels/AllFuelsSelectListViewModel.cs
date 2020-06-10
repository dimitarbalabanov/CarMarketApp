namespace CarMarket.Web.ViewModels.Fuels
{
    using System.Collections.Generic;

    public class AllFuelsSelectListViewModel
    {
        public IEnumerable<FuelSelectListViewModel> Fuels { get; set; }

        public int SelectedFuelId { get; set; }
    }
}
