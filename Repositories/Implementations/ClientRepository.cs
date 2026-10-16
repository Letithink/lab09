using Labo08_LeticiaMaldonado.DTOs;
using Labo08_LeticiaMaldonado.Models;
using Labo08_LeticiaMaldonado.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Labo08_LeticiaMaldonado.Repositories.Implementations;

public class ClientRepository : GenericRepository<Client> , IClientRepository
{
    public ClientRepository(Lab08DbContext context) : base(context) { }

    public async Task<IEnumerable<Client>> GetByNameAsync(string name)
    {
        return await _dbSet
            .Where(c => c.Name.ToLower().Contains(name.ToLower()))
            .ToListAsync();
    }

    public async Task<IEnumerable<string>> GetClientsWhoBoughtProductAsync(int productId)
    {
        return await _context.Orderdetails
            .Where(od => od.Productid == productId)
            .Select(od => od.Order.Client.Name)
            .Distinct()
            .ToListAsync();
    }
    
    public async Task<IEnumerable<ClientOrderDto>> GetClientsWithOrdersAsync()
    {
        var clientOrders = await _context.Clients
            .AsNoTracking() // Mejor rendimiento sin hacer seguimiento de cambios
            .Select(client => new ClientOrderDto
            {
                ClientName = client.Name,
                Orders = client.Orders
                    .Select(order => new OrderDto
                    {
                        OrderId = order.Orderid,
                        OrderDate = order.Orderdate
                    }).ToList()
            }).ToListAsync();

        return clientOrders;
    }
    
    public async Task<IEnumerable<object>> GetClientsWithProductCountAsync()
    {
        var clientsWithProductCount = await _context.Clients
            .AsNoTracking()
            .Select(client => new
            {
                ClientName = client.Name,
                TotalProducts = client.Orders
                    .Sum(order => order.Orderdetails
                        .Sum(detail => detail.Quantity))
            })
            .ToListAsync();

        return clientsWithProductCount;
    }

    
}