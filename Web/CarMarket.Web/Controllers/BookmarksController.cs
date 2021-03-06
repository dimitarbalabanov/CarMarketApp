﻿namespace CarMarket.Web.Controllers
{
    using System.Threading.Tasks;

    using CarMarket.Data.Models;
    using CarMarket.Services.Data.Interfaces;
    using CarMarket.Web.ViewModels.Bookmarks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BookmarksController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IBookmarksService bookmarksService;

        public BookmarksController(
            UserManager<ApplicationUser> userManager,
            IBookmarksService bookmarksService)
        {
            this.userManager = userManager;
            this.bookmarksService = bookmarksService;
        }

        [HttpPost]
        public async Task<ActionResult> Post(BookmarkInputModel input)
        {
            var userId = this.userManager.GetUserId(this.User);
            await this.bookmarksService.AddAsync(userId, input.Id);
            return this.Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var userId = this.userManager.GetUserId(this.User);
            await this.bookmarksService.RemoveAsync(userId, id);
            return this.NoContent();
        }
    }
}
