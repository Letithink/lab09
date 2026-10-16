using Labo08_LeticiaMaldonado.DTOs;
using Labo08_LeticiaMaldonado.Models;

namespace Labo08_LeticiaMaldonado.Services.Interfaces;

public interface IClientsService
{

    Task<IEnumerable<Client>> GetClientsByNameAsync(string name);


    Task<ClientWithOrderCountDto?> GetClientWithMostOrdersAsync();
    
    Task<IEnumerable<string>> GetClientsWhoBoughtProductAsync(int productId);
    
    Task<IEnumerable<ClientOrderDto>> GetClientsWithOrdersAsync();
    
    Task<IEnumerable<object>> GetClientsWithProductCountAsync();
    
}
