namespace CarMarket.Services.Data.Interfaces
{
    using System.Threading.Tasks;

    public interface IValidValue
    {
        Task<bool> IsValidByIdAsync(int id);
    }
}
