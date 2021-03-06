﻿namespace CarMarket.Web.ViewModels.Administration.Users
{
    using System;

    public class UserViewModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public DateTime CreatedOn { get; set; }

        public int ListingsCount { get; set; }
    }
}
