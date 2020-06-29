namespace CarMarket.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;

    using CarMarket.Data.Models;
    using CarMarket.Services.Data.Dtos;
    using CarMarket.Services.Data.Interfaces;
    using CarMarket.Web.ViewModels.Listings;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class ListingsController : Controller
    {
        private readonly IListingsService listingsService;
        private readonly IBookmarksService bookmarksService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IMapper mapper;

        public ListingsController(
            IListingsService listingsService,
            IBookmarksService bookmarksService,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IMapper mapper)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.mapper = mapper;
            this.listingsService = listingsService;
            this.bookmarksService = bookmarksService;
        }

        public IActionResult Create()
        {
            var viewModel = new CreateListingInputModel();
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateListingInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var images = this.mapper.Map<IEnumerable<CreateListingInputImageDto>>(input.InputImages);
            var userId = this.userManager.GetUserId(this.User);
            var listingId = await this.listingsService
                .CreateAsync<CreateListingInputModel>(input, userId, images);

            return this.RedirectToAction(nameof(this.Details), new { id = listingId });
        }

        public async Task<IActionResult> Edit(int id)
        {
            var viewModel = await this.listingsService.GetSingleByIdAsync<EditListingInputModel>(id);
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditListingInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            input.InputImages = input.InputImages.Where(x => x.ImageFile != null).ToList();
            var newImages = this.mapper.Map<IEnumerable<EditListingInputImageDto>>(input.InputImages);

            var userId = this.userManager.GetUserId(this.User);
            var listingId = await this.listingsService.EditAsync<EditListingInputModel>(input, input.Id, userId, newImages);
            return this.RedirectToAction(nameof(this.Details), new { id = listingId });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = this.userManager.GetUserId(this.User);
            await this.listingsService.DeleteByIdAsync(id, userId);
            return this.RedirectToAction(nameof(this.Personal));
        }

        public async Task<IActionResult> Personal()
        {
            var userId = this.userManager.GetUserId(this.User);
            var listings = await this.listingsService.GetAllByCreatorIdAsync<PersonalListingViewModel>(userId);
            var viewModel = new PersonalViewModel { Listings = listings };
            return this.View(viewModel);
        }

        public async Task<IActionResult> Bookmarks()
        {
            var userId = this.userManager.GetUserId(this.User);
            var bookmarkedListings = await this.bookmarksService.GetAllListingsByUserIdAsync<BookmarksListingViewModel>(userId);
            var viewModel = new BookmarksViewModel { Listings = bookmarkedListings };
            return this.View(viewModel);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var listingViewModel = await this.listingsService
                .GetSingleByIdAsync<DetailsListingViewModel>(id);

            if (this.signInManager.IsSignedIn(this.User))
            {
                var userId = this.userManager.GetUserId(this.User);
                listingViewModel.IsBookmarkedByCurrentUser = await this.bookmarksService.IsBookmarkedAsync(userId, id);
            }

            return this.View(listingViewModel);
        }
    }
}
