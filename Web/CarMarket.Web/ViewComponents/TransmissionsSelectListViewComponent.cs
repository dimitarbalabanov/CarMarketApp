namespace CarMarket.Web.ViewComponents
{
    using System.Threading.Tasks;

    using CarMarket.Services.Data.Interfaces;
    using CarMarket.Web.ViewModels.Transmissions;

    using Microsoft.AspNetCore.Mvc;

    public class TransmissionsSelectListViewComponent : ViewComponent
    {
        private readonly ITransmissionsService transmissionsService;

        public TransmissionsSelectListViewComponent(ITransmissionsService transmissionsService)
        {
            this.transmissionsService = transmissionsService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var transmissions = await this.transmissionsService.GetAllAsync<TransmissionSelectListViewModel>();
            var viewModel = new AllTransmissionsSelectListViewModel
            {
                Transmissions = transmissions,
                SelectedTransmissionId = id,
            };

            return this.View(viewModel);
        }
    }
}
