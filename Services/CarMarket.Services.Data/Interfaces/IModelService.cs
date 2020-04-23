namespace CarMarket.Services.Data.Interfaces
{
    using System.Collections.Generic;

    public interface IModelService
    {
        IEnumerable<T> GetAllByMakeId<T>(int id);
    }
}
