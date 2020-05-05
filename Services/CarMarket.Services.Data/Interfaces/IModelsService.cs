namespace CarMarket.Services.Data.Interfaces
{
    using System.Collections.Generic;

    public interface IModelsService
    {
        IEnumerable<T> GetAllByMakeId<T>(int id);
    }
}
