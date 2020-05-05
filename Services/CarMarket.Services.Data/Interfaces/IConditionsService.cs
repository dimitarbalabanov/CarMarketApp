namespace CarMarket.Services.Data.Interfaces
{
    using System.Collections.Generic;

    public interface IConditionsService
    {
        IEnumerable<T> GetAll<T>();
    }
}
