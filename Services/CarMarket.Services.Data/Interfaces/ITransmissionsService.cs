﻿namespace CarMarket.Services.Data.Interfaces
{
    using System.Collections.Generic;

    public interface ITransmissionsService
    {
        IEnumerable<T> GetAll<T>();
    }
}
