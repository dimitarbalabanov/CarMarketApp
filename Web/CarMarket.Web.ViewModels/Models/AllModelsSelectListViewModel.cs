namespace CarMarket.Web.ViewModels.Models
{
    using System.Collections.Generic;

    public class AllModelsSelectListViewModel
    {
        public IEnumerable<ModelSelectListViewModel> Models { get; set; }

        public int SelectedModelId { get; set; }
    }
}
