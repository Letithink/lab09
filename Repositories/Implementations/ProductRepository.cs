namespace Labo08_LeticiaMaldonado.Repositories.Implementations;

using Labo08_LeticiaMaldonado.Models;
using Labo08_LeticiaMaldonado.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;


public class ProductRepository : GenericRepository<Product> , IProductRepository    
{
    public ProductRepository(Lab08DbContext context) : base(context) { }

    public async Task<IEnumerable<Product>> GetWithPriceGreaterThanAsync(decimal minPrice)
    {
        return await _dbSet
            .Where(p => p.Price > minPrice)
            .ToListAsync();
    }

    public async Task<Product?> GetMostExpensiveAsync()
    {
        return await _dbSet
            .OrderByDescending(p => p.Price)
            .FirstOrDefaultAsync();
    }

    public async Task<decimal> GetAveragePriceAsync()
    {
        bool hasProducts = await _dbSet.AnyAsync();
        if (!hasProducts) return 0m;

        return await _dbSet.AverageAsync(p => p.Price);
    }

    public async Task<IEnumerable<Product>> GetWithoutDescriptionAsync()
    {
        return await _dbSet
            .Where(p => string.IsNullOrEmpty(p.Description))
            .ToListAsync();
    }
}