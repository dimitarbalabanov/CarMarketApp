namespace CarMarket.Services.Data.Interfaces
{
    using System.Collections.Generic;

    public interface IFuelService
    {
        IEnumerable<T> GetAll<T>();
    }
}
