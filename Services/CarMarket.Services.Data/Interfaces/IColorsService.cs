namespace CarMarket.Services.Data.Interfaces
{
    using System.Collections.Generic;

    public interface IColorsService
    {
        IEnumerable<T> GetAll<T>();
    }
}
