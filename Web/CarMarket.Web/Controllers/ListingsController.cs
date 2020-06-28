namespace CarMarket.Web.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using CarMarket.Data.Models;
    using CarMarket.Services.Data.Dtos;
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
        private readonly IMapper mapper;

        public ListingsController(IListingsService listingsService, IBookmarksService bookmarksService, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            this.userManager = userManager;
            this.mapper = mapper;
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

            var images = this.mapper.Map<IEnumerable<CreateListingImageDto>>(input.InputImages);
            var userId = this.userManager.GetUserId(this.User);
            var listingId = await this.listingsService
                .CreateAsync<CreateListingInputModel>(input, userId, images);

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
        public async Task<IActionResult> Edit(EditListingInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var newImages = new List<EditImageDto>();

            if (input.MainImage.Image != null)
            {
                var mainImg = new EditImageDto
                {
                    Id = input.MainImage.Id,
                    Image = input.MainImage.Image,
                    IsMain = true,
                };

                newImages.Add(mainImg);
            }

            if (input.SecondaryImageA.Image != null)
            {
                var secImgA = new EditImageDto
                {
                    Id = input.SecondaryImageA.Id,
                    Image = input.SecondaryImageA.Image,
                    IsMain = false,
                };

                newImages.Add(secImgA);
            }

            if (input.SecondaryImageB.Image != null)
            {
                var secImgB = new EditImageDto
                {
                    Id = input.SecondaryImageB.Id,
                    Image = input.SecondaryImageB.Image,
                    IsMain = false,
                };

                newImages.Add(secImgB);
            }

            var userId = this.userManager.GetUserId(this.User);
            var listingId = await this.listingsService.EditAsync<EditListingInputModel>(input, input.Id, userId, newImages);
            return this.RedirectToAction(nameof(this.Details), new { id = listingId });
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
