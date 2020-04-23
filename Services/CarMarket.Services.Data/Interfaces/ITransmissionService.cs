namespace CarMarket.Services.Data.Interfaces
{
    using System.Collections.Generic;

    public interface ITransmissionService
    {
        IEnumerable<T> GetAll<T>();
    }
}
