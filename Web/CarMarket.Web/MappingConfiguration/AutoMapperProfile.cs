namespace CarMarket.Web.MappingConfiguration
{
    using AutoMapper;
    using CarMarket.Data.Models;
    using CarMarket.Web.ViewModels.Home;
    using CarMarket.Web.ViewModels.Listings;
    using CarMarket.Web.ViewModels.Models;
    using System.Linq;

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // listing input model
            this.CreateMap<CreateListingInputModel, Listing>();

            this.CreateMap<Color, ColorDropDownViewModel>();
            this.CreateMap<Body, BodyDropDownViewModel>();
            this.CreateMap<Condition, ConditionDropDownViewModel>();
            this.CreateMap<Fuel, FuelDropDownViewModel>();
            this.CreateMap<Make, MakeDropDownViewModel>();
            this.CreateMap<Transmission, TransmissionDropDownViewModel>();
            this.CreateMap<Model, ModelResponseModel>();

            this.CreateMap<Listing, DetailsListingViewModel>()
                .ForMember(dest => dest.ImageUrls, opt => opt.MapFrom(x => x.Images.Select(y => y.ImageUrl)));

            this.CreateMap<Listing, HomeListingViewModel>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(x => x.Images.Select(y => y.ImageUrl).FirstOrDefault()));

            this.CreateMap<Listing, PersonalListingViewModel>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(x => x.Images.Select(y => y.ImageUrl).FirstOrDefault()));

            //CreateMap<StudentDTO, Student>()
            //    .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.CurrentCity))
            //    .ForMember(dest => dest.IsAdult, opt => opt.MapFrom(src => src.Age > 18 ? true : false));
            //CreateMap<AddressDTO, Address>();
        }
    }
}
