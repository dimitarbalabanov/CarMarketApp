namespace CarMarket.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class ListingsController : Controller
    {
        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        public IActionResult Single()
        {
            return this.View();
        }

        //[HttpPost]
        //[Authorize]
        //public async Task<IActionResult> Create()
        //{
        //    return null;
        //}
    }
}
