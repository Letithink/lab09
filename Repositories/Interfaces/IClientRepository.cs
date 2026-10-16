using Labo08_LeticiaMaldonado.DTOs;
using Labo08_LeticiaMaldonado.Models;

namespace Labo08_LeticiaMaldonado.Repositories.Interfaces;

public interface IClientRepository
{
    Task<IEnumerable<Client>> GetByNameAsync(string name);
    Task<IEnumerable<string>> GetClientsWhoBoughtProductAsync(int productId);
    Task<IEnumerable<ClientOrderDto>> GetClientsWithOrdersAsync();
    Task<IEnumerable<object>> GetClientsWithProductCountAsync();
}