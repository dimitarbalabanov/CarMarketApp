namespace CarMarket.Services.Data.Tests.Common
{
    using AutoMapper;
    using CarMarket.Web.MappingConfiguration;

    public static class MapperFactory
    {
        public static IMapper Create()
        {
            var config = new MapperConfiguration(c => c.AddProfile(new AutoMapperProfile()));
            return config.CreateMapper();
        }
    }
}
