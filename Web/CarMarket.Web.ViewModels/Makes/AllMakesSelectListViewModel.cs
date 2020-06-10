namespace CarMarket.Web.ViewModels.Makes
{
    using System.Collections.Generic;

    public class AllMakesSelectListViewModel
    {
        public IEnumerable<MakeSelectListViewModel> Makes { get; set; }

        public int SelectedMakeId { get; set; }
    }
}
