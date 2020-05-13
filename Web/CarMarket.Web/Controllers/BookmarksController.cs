using CarMarket.Data.Models;
using CarMarket.Services.Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarMarket.Web.Controllers
{
    public class BookmarksController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IListingsService listingsService;

        public BookmarksController(UserManager<ApplicationUser> userManager, IListingsService listingsService)
        {
            this.userManager = userManager;
            this.listingsService = listingsService;
        }

        //public async Task<IActionResult> Personal()
        //{
        //    var user = await this.userManager.GetUserAsync(this.User);
        //    var listings = await this.listingsService.GetAllByCreatorIdAsync<PersonalListingViewModel>(user.Id);
        //    var viewModel = new PersonalViewModel { Listings = listings };
        //    return this.View(viewModel);
        //}
    }
}
