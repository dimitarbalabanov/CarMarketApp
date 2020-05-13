namespace CarMarket.Web.MappingConfiguration
{
    using AutoMapper;
    using CarMarket.Data.Models;
    using CarMarket.Web.ViewModels.Home;
    using CarMarket.Web.ViewModels.Listings;
    using CarMarket.Web.ViewModels.Listings.SelectListItemsViewModels;
    using CarMarket.Web.ViewModels.Models;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Linq;

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // listing input model
            this.CreateMap<CreateListingInputModel, Listing>();

            this.CreateMap<Color, ColorSelectListViewModel>();

            this.CreateMap<Body, BodySelectListViewModel>();

            this.CreateMap<Condition, ConditionSelectListViewModel>();

            this.CreateMap<Fuel, FuelSelectListViewModel>();

            this.CreateMap<Make, MakeSelectListViewModel>();

            this.CreateMap<Transmission, TransmissionSelectListViewModel>();

            this.CreateMap<Model, ModelSelectListViewModel>();

            this.CreateMap<Model, ModelResponseModel>();

            this.CreateMap<Listing, DetailsListingViewModel>()
                .ForMember(dest => dest.ImageUrls, opt => opt.MapFrom(x => x.Images.Select(y => y.ImageUrl)));

            this.CreateMap<Listing, HomeListingViewModel>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(x => x.Images.Select(y => y.ImageUrl).FirstOrDefault()));

            this.CreateMap<Listing, PersonalListingViewModel>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(x => x.Images.Select(y => y.ImageUrl).FirstOrDefault()));
        }
    }
}
