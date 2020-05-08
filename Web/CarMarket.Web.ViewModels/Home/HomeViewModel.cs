using System;
using System.Collections.Generic;
using System.Text;

namespace CarMarket.Web.ViewModels.Home
{
    public class HomeViewModel
    {
        public IEnumerable<HomeListingViewModel> Listings { get; set; }
    }
}
