using Labo08_LeticiaMaldonado.DTOs;
using Labo08_LeticiaMaldonado.Models;
using Labo08_LeticiaMaldonado.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Labo08_LeticiaMaldonado.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrdersService _ordersService;

    public OrdersController(IOrdersService ordersService)
    {
        _ordersService = ordersService;
    }
    
    [HttpGet("{orderId:int}/detalles")]
    public async Task<ActionResult<IEnumerable<ProductDetailDto>>> GetProductDetailsByOrderId(int orderId)
    {
        var details = await _ordersService.GetProductDetailsByOrderIdAsync(orderId);
        return Ok(details);
    }

   
    [HttpGet("{orderId:int}/total-productos")]
    public async Task<ActionResult<int>> GetTotalProductQuantityByOrderId(int orderId)
    {
        var totalQuantity = await _ordersService.GetTotalProductQuantityByOrderIdAsync(orderId);
        return Ok(new { OrderId = orderId, TotalQuantity = totalQuantity });
    }

   
    [HttpGet("despues-fecha")]
    public async Task<ActionResult<IEnumerable<Order>>> GetOrdersAfterDate([FromQuery] DateTime date)
    {
        var orders = await _ordersService.GetOrdersAfterDateAsync(date);
        return Ok(orders);
    }

   
    [HttpGet("todos-detalles")]
    public async Task<ActionResult<IEnumerable<OrderDetailSummaryDto>>> GetAllOrdersAndDetails()
    {
        var details = await _ordersService.GetAllOrdersAndDetailsAsync();
        return Ok(details);
    }
    
    [HttpGet("con-detalles")]
    public async Task<ActionResult<IEnumerable<OrderDetailsDto>>> GetOrdersWithDetails()
    {
        var ordersWithDetails = await _ordersService.GetOrdersWithDetailsAsync();
        return Ok(ordersWithDetails);
    }
    
    [HttpGet("ventas-por-cliente")]
    public async Task<ActionResult<IEnumerable<SalesByClientDto>>> GetSalesByClient()
    {
        var sales  = await _ordersService.GetSalesByClientAsync();
        return Ok(sales);
    }
}
