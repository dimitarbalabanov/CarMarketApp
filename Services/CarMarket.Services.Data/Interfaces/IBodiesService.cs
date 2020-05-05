namespace CarMarket.Services.Data.Interfaces
{
    using System.Collections.Generic;

    public interface IBodiesService
    {
        IEnumerable<T> GetAll<T>();
    }
}
