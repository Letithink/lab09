using Labo08_LeticiaMaldonado.DTOs;
using Labo08_LeticiaMaldonado.Models;
using Labo08_LeticiaMaldonado.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Labo08_LeticiaMaldonado.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly IClientsService _clientsService;

    public ClientsController(IClientsService clientsService)
    {
        _clientsService = clientsService;
    }


    [HttpGet("by-name")]
    public async Task<ActionResult<IEnumerable<Client>>> GetClientsByName([FromQuery] string name)
    {
        var clients = await _clientsService.GetClientsByNameAsync(name);
        return Ok(clients);
    }

    [HttpGet("most-orders")]
    public async Task<ActionResult<ClientWithOrderCountDto>> GetClientWithMostOrders()
    {
        var result = await _clientsService.GetClientWithMostOrdersAsync();
        if (result == null)
            return NotFound("No hay pedidos registrados.");

        return Ok(result);
    }
    
    [HttpGet("comprar-producto/{productId:int}")]
    public async Task<ActionResult<IEnumerable<string>>> GetClientsWhoBoughtProduct(int productId)
    {
        var clients = await _clientsService.GetClientsWhoBoughtProductAsync(productId);
        return Ok(clients);
    }
    
    
    
    [HttpGet("con-conteo-productos")]
    public async Task<ActionResult<IEnumerable<object>>> GetClientsWithProductCount()
    {
        var result = await _clientsService.GetClientsWithProductCountAsync();
        return Ok(result);
    }
    
    [HttpGet("con-pedidos")]
    public async Task<ActionResult<IEnumerable<ClientOrderDto>>> GetClientsWithOrders()
    {
        var clients = await _clientsService.GetClientsWithOrdersAsync();
        return Ok(clients);
    }
}
