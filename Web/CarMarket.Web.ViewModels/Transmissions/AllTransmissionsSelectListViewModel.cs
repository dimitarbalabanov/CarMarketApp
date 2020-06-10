namespace CarMarket.Web.ViewModels.Transmissions
{
    using System.Collections.Generic;

    public class AllTransmissionsSelectListViewModel
    {
        public IEnumerable<TransmissionSelectListViewModel> Transmissions { get; set; }

        public int SelectedTransmissionId { get; set; }
    }
}
