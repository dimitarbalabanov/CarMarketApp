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

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var transmissions = await this.transmissionsService.GetAllAsync<TransmissionSelectListViewModel>();
            return this.View(transmissions);
        }
    }
}
