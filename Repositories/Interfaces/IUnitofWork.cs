using Labo08_LeticiaMaldonado.Repositories.Implementations;

namespace Labo08_LeticiaMaldonado.Repositories.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;

    IProductRepository Products { get; }
    IOrderRepository Orders { get; }
    IClientRepository Clients { get; }

    Task<int> Complete();
}