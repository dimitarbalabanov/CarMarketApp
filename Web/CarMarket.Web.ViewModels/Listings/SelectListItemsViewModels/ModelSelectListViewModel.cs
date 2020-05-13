using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarMarket.Web.ViewModels.Listings.SelectListItemsViewModels
{
    public class ModelSelectListViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public SelectListItem ModelSelectListItem => new SelectListItem(this.Name, this.Id.ToString());
    }
}
