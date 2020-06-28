﻿namespace CarMarket.Web.MappingConfiguration
{
    using System.Linq;

    using AutoMapper;

    using CarMarket.Data.Models;
    using CarMarket.Services.Data.Dtos;
    using CarMarket.Services.Data.SearchServiceHelpers.Dtos;
    using CarMarket.Web.ViewModels.Administration.Makes;
    using CarMarket.Web.ViewModels.Administration.Users;
    using CarMarket.Web.ViewModels.Bodies;
    using CarMarket.Web.ViewModels.Colors;
    using CarMarket.Web.ViewModels.Conditions;
    using CarMarket.Web.ViewModels.Fuels;
    using CarMarket.Web.ViewModels.Home;
    using CarMarket.Web.ViewModels.Images;
    using CarMarket.Web.ViewModels.Listings;
    using CarMarket.Web.ViewModels.Makes;
    using CarMarket.Web.ViewModels.Models;
    using CarMarket.Web.ViewModels.Search;
    using CarMarket.Web.ViewModels.Transmissions;

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            this.CreateMap<CreateListingInputModel, Listing>();

            this.CreateMap<Listing, EditListingInputModel>()
                .ForMember(dest => dest.InputImages, opt => opt.MapFrom(l => l.Images));

            this.CreateMap<Image, EditListingImageInputModel>();

            this.CreateMap<CreateListingImageInputModel, CreateListingInputImageDto>();

            this.CreateMap<EditListingImageInputModel, EditListingInputImageDto>();

            this.CreateMap<EditListingInputModel, Listing>();

            this.CreateMap<Color, ColorSelectListViewModel>();

            this.CreateMap<Body, BodySelectListViewModel>();

            this.CreateMap<Condition, ConditionSelectListViewModel>();

            this.CreateMap<Fuel, FuelSelectListViewModel>();

            this.CreateMap<Make, MakeSelectListViewModel>();

            this.CreateMap<Make, MakeViewModel>();

            this.CreateMap<Make, MakeAdminViewModel>();

            this.CreateMap<Make, DetailsMakeViewModel>();

            this.CreateMap<CreateMakeInputModel, Make>();

            this.CreateMap<Transmission, TransmissionSelectListViewModel>();

            this.CreateMap<Model, ModelSelectListViewModel>();

            this.CreateMap<Model, ModelResponseModel>();

            this.CreateMap<Model, DetailsMakeModelViewModel>();

            this.CreateMap<CreateModelInputModel, Model>();

            this.CreateMap<Listing, DetailsListingViewModel>()
                .ForMember(dest => dest.ImageUrls, opt => opt.MapFrom(x => x.Images.Select(y => y.ImageUrl)));

            this.CreateMap<Listing, HomeListingViewModel>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(x => x.Images.Select(y => y.ImageUrl).FirstOrDefault()));

            this.CreateMap<Listing, PersonalListingViewModel>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(x => x.Images.Select(y => y.ImageUrl).FirstOrDefault()));

            this.CreateMap<Listing, BookmarksListingViewModel>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(x => x.Images.Select(y => y.ImageUrl).FirstOrDefault()));

            this.CreateMap<Listing, SearchResultListingViewModel>()
               .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(x => x.Images.Select(y => y.ImageUrl).FirstOrDefault()));

            this.CreateMap<SearchInputModel, SearchModelDto>();

            this.CreateMap<ApplicationUser, UserViewModel>();

            this.CreateMap<ApplicationUser, UserDetailsViewModel>();

            this.CreateMap<Listing, UserDetailsListingViewModel>();
        }
    }
}
