using Labo08_LeticiaMaldonado.DTOs;
using Labo08_LeticiaMaldonado.Models;
using Labo08_LeticiaMaldonado.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Labo08_LeticiaMaldonado.Repositories.Implementations;

public class OrderRepository : GenericRepository<Order> , IOrderRepository
{
    public OrderRepository(Lab08DbContext context) : base(context) { }

    public async Task<IEnumerable<string>> GetProductNamesSoldToClientAsync(int clientId)
    {
        return await _dbSet
            .Where(o => o.Clientid == clientId)
            .SelectMany(o => o.Orderdetails)
            .Select(od => od.Product.Name)
            .Distinct()
            .ToListAsync();
    }

    public async Task<IEnumerable<Order>> GetAfterDateAsync(DateTime date)
    {
        return await _dbSet
            .Where(o => o.Orderdate > date)
            .ToListAsync();
    }

    public async Task<ClientWithOrderCountDto?> GetClientWithMostOrdersAsync()
    {
        return await _dbSet
            .GroupBy(o => o.Clientid)
            .OrderByDescending(g => g.Count())
            .Select(g => new ClientWithOrderCountDto
            {
                ClientId = g.Key,
                ClientName = g.First().Client.Name,
                OrderCount = g.Count()
            })
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<ProductDetailDto>> GetProductDetailsByOrderIdAsync(int orderId)
    {
        return await _context.Orderdetails
            .Where(od => od.Orderid == orderId)
            .Select(od => new ProductDetailDto
            {
                ProductName = od.Product.Name,
                Quantity = od.Quantity
            })
            .ToListAsync();
    }

    public async Task<int> GetTotalQuantityByOrderIdAsync(int orderId)
    {
        return await _context.Orderdetails
            .Where(od => od.Orderid == orderId)
            .Select(od => od.Quantity)
            .SumAsync();
    }

    public async Task<IEnumerable<OrderDetailSummaryDto>> GetAllSummariesAsync()
    {
        return await _context.Orderdetails
            .Select(od => new OrderDetailSummaryDto
            {
                OrderId = od.Orderid,
                ProductName = od.Product.Name,
                Quantity = od.Quantity
            })
            .ToListAsync();
    }
    
    public async Task<IEnumerable<OrderDetailsDto>> GetOrdersWithDetailsAsync()
    {
        var ordersWithDetails = await _context.Orders
            .Include(order => order.Orderdetails)
            .ThenInclude(orderDetail => orderDetail.Product)
            .AsNoTracking()
            .Select(order => new OrderDetailsDto
            {
                OrderId = order.Orderid,
                OrderDate = order.Orderdate,
                Products = order.Orderdetails
                    .Select(od => new ProductDto
                    {
                        ProductName = od.Product.Name,
                        Quantity = od.Quantity,
                        Price = od.Product.Price
                    }).ToList()
            }).ToListAsync();

        return ordersWithDetails;
    }
    
    public async Task<IEnumerable<SalesByClientDto>> SalesbyClientsAsync()
    {
        var SalesbyClient = await _context.Orders
            .Include(order => order.Orderdetails)
            .ThenInclude(orderDetail => orderDetail.Product)
            .AsNoTracking()
            .GroupBy(order => order.Clientid)
            .Select(group => new SalesByClientDto
            {
                ClientName = _context.Clients
                    .FirstOrDefault(c => c.Clientid == group.Key).Name,
                TotalSales = group.Sum(order => order.Orderdetails
                    .Sum(detail => detail.Quantity * detail.Product.Price))
            })
            .OrderByDescending(s => s.TotalSales)
            .ToListAsync();
        return SalesbyClient;
    }
}