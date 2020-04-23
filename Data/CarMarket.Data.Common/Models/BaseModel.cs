namespace CarMarket.Data.Common.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public abstract class BaseModel<TKey> : ICreatedOn
    {
        [Key]
        public TKey Id { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
