﻿using CarMarket.Data.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CarMarket.Web.ViewModels.Listings
{
    public class DetailsListingViewModel
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public string SellerId { get; set; }

        public string SellerFirstName { get; set; }

        public string SellerLastName { get; set; }

        public string SellerEmail { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public int Mileage { get; set; }

        public int Horsepower { get; set; }

        public int ProductionYear { get; set; }

        public string ConditionType { get; set; }

        public string ModelName { get; set; }

        public string MakeName { get; set; }

        public string BodyType { get; set; }

        public string ColorName { get; set; }

        public string TransmissionType { get; set; }

        public string FuelType { get; set; }

        public string[] ImageUrls { get; set; }

        public bool IsBookmarkedByCurrentUser { get; set; }

        // description
        // mileage
        // horsepower
        // prod year
        // condition -> type
        // model -> name
        // body -> type
        // color -> name
        // transmission -> type
        // fuel -> type
        // make -> name
        // images -> urls
    }
}
