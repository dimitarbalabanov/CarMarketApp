namespace CarMarket.Services.Data.Interfaces
{
    using System.Collections.Generic;

    public interface IConditionService
    {
        IEnumerable<T> GetAll<T>();
    }
}
