using Labo08_LeticiaMaldonado.DTOs;
using Labo08_LeticiaMaldonado.Models;

namespace Labo08_LeticiaMaldonado.Services.Interfaces;

public interface IOrdersService
{

    Task<IEnumerable<ProductDetailDto>> GetProductDetailsByOrderIdAsync(int orderId);

    Task<int> GetTotalProductQuantityByOrderIdAsync(int orderId);


    Task<IEnumerable<Order>> GetOrdersAfterDateAsync(DateTime date);

    Task<IEnumerable<OrderDetailSummaryDto>> GetAllOrdersAndDetailsAsync();
    
    Task<IEnumerable<OrderDetailsDto>> GetOrdersWithDetailsAsync();
    Task<IEnumerable<SalesByClientDto>> GetSalesByClientAsync();
}
