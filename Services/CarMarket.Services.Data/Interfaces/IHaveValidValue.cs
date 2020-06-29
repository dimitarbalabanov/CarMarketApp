namespace CarMarket.Services.Data.Interfaces
{
    using System.Threading.Tasks;

    public interface IHaveValidValue
    {
        Task<bool> IsValidByIdAsync(int id);
    }
}
