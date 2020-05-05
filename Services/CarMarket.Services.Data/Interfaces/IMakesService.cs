namespace CarMarket.Services.Data.Interfaces
{
    using System.Collections.Generic;

    public interface IMakesService
    {
        IEnumerable<T> GetAll<T>();
    }
}
