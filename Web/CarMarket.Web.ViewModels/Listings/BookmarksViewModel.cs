using System;
using System.Collections.Generic;
using System.Text;

namespace CarMarket.Web.ViewModels.Listings
{
    public class BookmarksViewModel
    {
        public IEnumerable<BookmarksListingViewModel> Listings { get; set; }
    }
}
