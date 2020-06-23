namespace CarMarket.Web.ViewModels.Administration.Makes
{
    using System;
    using System.Collections.Generic;

    public class DetailsMakeViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ModelsCount { get; set; }

        public DateTime CreatedOn { get; set; }

        public IEnumerable<DetailsMakeModelViewModel> Models { get; set; }
    }
}
