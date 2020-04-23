namespace CarMarket.Services.Data.Interfaces
{
    using System.Collections.Generic;

    public interface IColorService
    {
        IEnumerable<T> GetAll<T>();
    }
}
