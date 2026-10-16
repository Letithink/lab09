using Labo08_LeticiaMaldonado.Models;

namespace Labo08_LeticiaMaldonado.Repositories.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetWithPriceGreaterThanAsync(decimal minPrice);

    Task<Product?> GetMostExpensiveAsync();

    Task<decimal> GetAveragePriceAsync();
    Task<IEnumerable<Product>> GetWithoutDescriptionAsync();
}