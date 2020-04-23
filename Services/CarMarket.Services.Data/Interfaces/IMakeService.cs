namespace CarMarket.Services.Data.Interfaces
{
    using System.Collections.Generic;

    public interface IMakeService
    {
        IEnumerable<T> GetAll<T>();
    }
}
