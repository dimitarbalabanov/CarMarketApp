namespace CarMarket.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CarMarket.Data.Models;
    using CarMarket.Services.Data.Interfaces;
    using CarMarket.Web.ViewModels.Listings;
    using CarMarket.Web.ViewModels.Listings.SelectListItemsViewModels;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class ListingsController : Controller
    {
        private readonly IBodiesService bodiesService;
        private readonly IColorsService colorsService;
        private readonly IConditionsService conditionsService;
        private readonly IFuelsService fuelsService;
        private readonly IListingsService listingsService;
        private readonly IMakesService makesService;
        private readonly ITransmissionsService transmissionsService;
        private readonly IModelsService modelsService;
        private readonly IBookmarksService bookmarksService;
        private readonly UserManager<ApplicationUser> userManager;

        public ListingsController(
            IBodiesService bodiesService,
            IColorsService colorsService,
            IConditionsService conditionsService,
            IFuelsService fuelsService,
            IListingsService listingsService,
            IMakesService makesService,
            ITransmissionsService transmissionsService,
            IModelsService modelsService,
            IBookmarksService bookmarksService,
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
            this.modelsService = modelsService;
            this.bookmarksService = bookmarksService;
        }

        [Authorize]
        public async Task<IActionResult> Create()
        {
            var bodies = await this.bodiesService.GetAllAsync<BodySelectListViewModel>();
            var colors = await this.colorsService.GetAllAsync<ColorSelectListViewModel>();
            var conditions = await this.conditionsService.GetAllAsync<ConditionSelectListViewModel>();
            var fuels = await this.fuelsService.GetAllAsync<FuelSelectListViewModel>();
            var makes = await this.makesService.GetAllAsync<MakeSelectListViewModel>();
            var transmissions = await this.transmissionsService.GetAllAsync<TransmissionSelectListViewModel>();

            var viewModel = new CreateListingInputModel
            {
                Bodies = bodies.Select(x => x.BodySelectListItem),
                Colors = colors.Select(x => x.ColorSelectListItem),
                Conditions = conditions.Select(x => x.ConditionSelectListItem),
                Fuels = fuels.Select(x => x.FuelSelectListItem),
                Makes = makes.Select(x => x.MakeSelectListItem),
                Transmissions = transmissions.Select(x => x.TransmissionSelectListItem),
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            var listingViewModel = await this.listingsService
                .GetSingleByIdAsync<DetailsListingViewModel>(id);
            var user = await this.userManager.GetUserAsync(this.User);
            listingViewModel.IsBookmarkedByCurrentUser = await this.bookmarksService.IsBookmarkedAsync(user.Id, id);
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

        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var viewModel = await this.listingsService.GetSingleByIdAsync<EditListingInputModel>(id);

            var bodies = await this.bodiesService.GetAllAsync<BodySelectListViewModel>();
            var colors = await this.colorsService.GetAllAsync<ColorSelectListViewModel>();
            var conditions = await this.conditionsService.GetAllAsync<ConditionSelectListViewModel>();
            var fuels = await this.fuelsService.GetAllAsync<FuelSelectListViewModel>();
            var makes = await this.makesService.GetAllAsync<MakeSelectListViewModel>();
            var transmissions = await this.transmissionsService.GetAllAsync<TransmissionSelectListViewModel>();
            var models = await this.modelsService.GetAllByMakeIdAsync<ModelSelectListViewModel>(viewModel.MakeId);
            var make = makes.FirstOrDefault(x => x.Id == viewModel.MakeId);
            var selectMake = new SelectListItem(make.Name, make.Id.ToString(), true, true);
            var list = new List<SelectListItem>();
            list.Add(selectMake);
            viewModel.Bodies = bodies.Select(x => x.BodySelectListItem);
            viewModel.Colors = colors.Select(x => x.ColorSelectListItem);
            viewModel.Fuels = fuels.Select(x => x.FuelSelectListItem);
            viewModel.Makes = list;
            viewModel.Transmissions = transmissions.Select(x => x.TransmissionSelectListItem);
            viewModel.Conditions = conditions.Select(x => x.ConditionSelectListItem);
            viewModel.Models = models.Select(x => x.ModelSelectListItem);

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit()
        {
            //var user = await this.userManager.GetUserAsync(this.User);

            ////if (!this.ModelState.IsValid)
            ////{
            ////    return this.View(input);
            ////}

            //var listingId = await this.listingsService.CreateAsync<CreateListingInputModel>(input, user.Id, input.UploadImages);
            return null;
            //return this.RedirectToAction(nameof(this.Details), new { id = listingId });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            //var user = await this.userManager.GetUserAsync(this.User);

            //if (!this.ModelState.IsValid)
            //{
            //    return this.View(input);
            //}

            await this.listingsService.DeleteByIdAsync(id);

            return this.RedirectToAction(nameof(this.Personal));
        }

        public async Task<IActionResult> Personal()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var listings = await this.listingsService.GetAllByCreatorIdAsync<PersonalListingViewModel>(user.Id);
            var viewModel = new PersonalViewModel { Listings = listings };
            return this.View(viewModel);
        }

        public async Task<IActionResult> Bookmarks()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var bookmarkedListings = await this.bookmarksService.GetAllListingsByUserIdAsync<BookmarksListingViewModel>(user.Id);
            var viewModel = new BookmarksViewModel { Listings = bookmarkedListings };
            return this.View(viewModel);
        }
    }
}
