namespace CarMarket.Web.MappingConfiguration
{
    using AutoMapper;
    using CarMarket.Data.Models;
    using CarMarket.Web.ViewModels.Listings;
    using CarMarket.Web.ViewModels.Models;

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

            //CreateMap<StudentDTO, Student>()
            //    .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.CurrentCity))
            //    .ForMember(dest => dest.IsAdult, opt => opt.MapFrom(src => src.Age > 18 ? true : false));
            //CreateMap<AddressDTO, Address>();
        }
    }
}
