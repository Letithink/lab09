using System.Collections;
using Labo08_LeticiaMaldonado.Models;
using Labo08_LeticiaMaldonado.Repositories.Interfaces;

namespace Labo08_LeticiaMaldonado.Repositories.Implementations;

public class UnitOfWork : IUnitOfWork
{
    private Hashtable? _repositories;
    private readonly Lab08DbContext _context;

    public IProductRepository Product { get; }
    public IOrderRepository Ordenes { get;  }
    public IClientRepository Client { get; }

    public UnitOfWork(Lab08DbContext context)
    {
        _context = context;
        _repositories = new Hashtable();
    }


    public IOrderRepository Orders { get; }
    public IClientRepository Clients { get; }

    public Task<int> Complete()
    {
        return _context.SaveChangesAsync();
    }

    public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
    {
        var type = typeof(TEntity).Name;

        if (_repositories!.ContainsKey(type))
        {
            return (IGenericRepository<TEntity>)_repositories[type]!;
        }

        var repositoryType = typeof(GenericRepository<>);
        var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);

        if (repositoryInstance != null)
        {
            _repositories.Add(type, repositoryInstance);
            return (IGenericRepository<TEntity>)repositoryInstance;
        }

        throw new Exception($"Could not create repository instance for type {type}");
    }

    public IProductRepository Products { get; }


    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}