namespace CarMarket.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using CarMarket.Data.Models;
    using CarMarket.Services.Data.Interfaces;
    using CarMarket.Web.ViewModels.Listings;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class ListingsController : Controller
    {
        private readonly IBodiesService bodiesService;
        private readonly IColorsService colorsService;
        private readonly IConditionsService conditionsService;
        private readonly IFuelsService fuelsService;
        private readonly IListingsService listingsService;
        private readonly IMakesService makesService;
        private readonly ITransmissionsService transmissionsService;
        private readonly UserManager<ApplicationUser> userManager;

        public ListingsController(
            IBodiesService bodiesService,
            IColorsService colorsService,
            IConditionsService conditionsService,
            IFuelsService fuelsService,
            IListingsService listingsService,
            IMakesService makesService,
            ITransmissionsService transmissionsService,
            UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
            this.bodiesService = bodiesService;
            this.colorsService = colorsService;
            this.conditionsService = conditionsService;
            this.fuelsService = fuelsService;
            this.listingsService = listingsService;
            this.makesService = makesService;
            this.transmissionsService = transmissionsService;
        }

        [Authorize]
        public async Task<IActionResult> Create()
        {
            var bodies = await this.bodiesService.GetAllAsync<BodyDropDownViewModel>();
            var colors = await this.colorsService.GetAllAsync<ColorDropDownViewModel>();
            var conditions = await this.conditionsService.GetAllAsync<ConditionDropDownViewModel>();
            var fuels = await this.fuelsService.GetAllAsync<FuelDropDownViewModel>();
            var makes = await this.makesService.GetAllAsync<MakeDropDownViewModel>();
            var transmissions = await this.transmissionsService.GetAllAsync<TransmissionDropDownViewModel>();

            var viewModel = new CreateListingInputModel
            {
                Bodies = bodies,
                Colors = colors,
                Conditions = conditions,
                Fuels = fuels,
                Makes = makes,
                Transmissions = transmissions,
            };
            return this.View(viewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            var listingViewModel = await this.listingsService.GetSingleByIdAsync<DetailsListingViewModel>(id);
            return this.View(listingViewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateListingInputModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            //if (!this.ModelState.IsValid)
            //{
            //    return this.View(input);
            //}

            var listingId = await this.listingsService.CreateAsync<CreateListingInputModel>(input, user.Id, input.UploadImages);

            return this.RedirectToAction(nameof(this.Details), new { id = listingId });
        }

        public async Task<IActionResult> Personal()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var listings = await this.listingsService.GetAllByCreatorIdAsync<PersonalListingViewModel>(user.Id);
            var viewModel = new PersonalViewModel { Listings = listings };
            return this.View(viewModel);
        }
    }
}
