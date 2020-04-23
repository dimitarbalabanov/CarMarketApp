namespace CarMarket.Services.Data.Interfaces
{
    using System.Collections.Generic;

    public interface IBodyService
    {
        IEnumerable<T> GetAll<T>();
    }
}
