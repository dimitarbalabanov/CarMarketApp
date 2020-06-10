namespace CarMarket.Web.ViewModels.Bodies
{
    using System.Collections.Generic;

    public class AllBodiesSelectListViewModel
    {
        public IEnumerable<BodySelectListViewModel> Bodies { get; set; }

        public int SelectedBodyId { get; set; }
    }
}
