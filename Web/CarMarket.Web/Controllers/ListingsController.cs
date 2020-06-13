namespace CarMarket.Web.Controllers
{
    using System.Threading.Tasks;

    using CarMarket.Data.Models;
    using CarMarket.Services.Data.Interfaces;
    using CarMarket.Web.ViewModels.Listings;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class ListingsController : Controller
    {
        private readonly IListingsService listingsService;
        private readonly IBookmarksService bookmarksService;
        private readonly UserManager<ApplicationUser> userManager;

        public ListingsController(IListingsService listingsService, IBookmarksService bookmarksService, UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
            this.listingsService = listingsService;
            this.bookmarksService = bookmarksService;
        }

        [Authorize]
        public IActionResult Create()
        {
            var viewModel = new CreateListingInputModel();
            return this.View(viewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            var listingViewModel = await this.listingsService
                .GetSingleByIdAsync<DetailsListingViewModel>(id);
            var userId = this.userManager.GetUserId(this.User);
            listingViewModel.IsBookmarkedByCurrentUser = await this.bookmarksService.IsBookmarkedAsync(userId, id);
            return this.View(listingViewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateListingInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var userId = this.userManager.GetUserId(this.User);
            var listingId = await this.listingsService
                .CreateAsync<CreateListingInputModel>(input, userId, input.MainImage, input.SecondaryImageA, input.SecondaryImageB);

            return this.RedirectToAction(nameof(this.Details), new { id = listingId });
        }

        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var viewModel = await this.listingsService.GetSingleByIdAsync<EditListingInputModel>(id);
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

        [Authorize]
        public async Task<IActionResult> Personal()
        {
            var userId = this.userManager.GetUserId(this.User);
            var listings = await this.listingsService.GetAllByCreatorIdAsync<PersonalListingViewModel>(userId);
            var viewModel = new PersonalViewModel { Listings = listings };
            return this.View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> Bookmarks()
        {
            var userId = this.userManager.GetUserId(this.User);
            var bookmarkedListings = await this.bookmarksService.GetAllListingsByUserIdAsync<BookmarksListingViewModel>(userId);
            var viewModel = new BookmarksViewModel { Listings = bookmarkedListings };
            return this.View(viewModel);
        }
    }
}
