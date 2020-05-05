namespace CarMarket.Services.Data.Interfaces
{
    using System.Collections.Generic;

    public interface IFuelsService
    {
        IEnumerable<T> GetAll<T>();
    }
}
