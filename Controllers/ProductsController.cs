using Labo08_LeticiaMaldonado.Models;
using Labo08_LeticiaMaldonado.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Labo08_LeticiaMaldonado.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductsService _productsService;

    public ProductsController(IProductsService productsService)
    {
        _productsService = productsService;
    }


    [HttpGet("precio-mayor-que/{minPrice:decimal}")]
    public async Task<ActionResult<IEnumerable<Product>>> GetProductsWithPriceGreaterThan(decimal minPrice)
    {
        var products = await _productsService.GetProductsWithPriceGreaterThanAsync(minPrice);
        return Ok(products);
    }
    
    [HttpGet("mas-caro")]
    public async Task<ActionResult<Product>> GetMostExpensiveProduct()
    {
        var product = await _productsService.GetMostExpensiveProductAsync();
        if (product == null)
            return NotFound("No hay productos disponibles.");

        return Ok(product);
    }

    [HttpGet("promedio-precio")]
    public async Task<ActionResult<decimal>> GetAverageProductPrice()
    {
        var average = await _productsService.GetAverageProductPriceAsync();
        return Ok(new { AveragePrice = average });
    }

    
    [HttpGet("sin-descripcion")]
    public async Task<ActionResult<IEnumerable<Product>>> GetProductsWithoutDescription()
    {
        var products = await _productsService.GetProductsWithoutDescriptionAsync();
        return Ok(products);
    }

    
    [HttpGet("vender-a-cliente/{clientId:int}")]
    public async Task<ActionResult<IEnumerable<string>>> GetProductsSoldToClient(int clientId)
    {
        var products = await _productsService.GetProductsSoldToClientAsync(clientId);
        return Ok(products);
    }
}
