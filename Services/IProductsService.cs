using Labo08_LeticiaMaldonado.Models;

namespace Labo08_LeticiaMaldonado.Services.Interfaces;

public interface IProductsService
{

    Task<IEnumerable<Product>> GetProductsWithPriceGreaterThanAsync(decimal minPrice);


    Task<Product?> GetMostExpensiveProductAsync();


    Task<decimal> GetAverageProductPriceAsync();

   
    Task<IEnumerable<Product>> GetProductsWithoutDescriptionAsync();

    Task<IEnumerable<string>> GetProductsSoldToClientAsync(int clientId);
}
