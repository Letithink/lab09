using Labo08_LeticiaMaldonado.DTOs;
using Labo08_LeticiaMaldonado.Models;
using Labo08_LeticiaMaldonado.Repositories.Interfaces;
using Labo08_LeticiaMaldonado.Services.Interfaces;

namespace Labo08_LeticiaMaldonado.Services.Implementations;

public class ClientsService : IClientsService
{
    private readonly IUnitOfWork _unitOfWork;

    public ClientsService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Client>> GetClientsByNameAsync(string name)
    {
        return await _unitOfWork.Clients.GetByNameAsync(name);
    }

    public async Task<ClientWithOrderCountDto?> GetClientWithMostOrdersAsync()
    {
        return await _unitOfWork.Orders.GetClientWithMostOrdersAsync();
    }

    public async Task<IEnumerable<string>> GetClientsWhoBoughtProductAsync(int productId)
    {
        return await _unitOfWork.Clients.GetClientsWhoBoughtProductAsync(productId);
    }
    
    public async Task<IEnumerable<ClientOrderDto>> GetClientsWithOrdersAsync()
    {
        return await _unitOfWork.Clients.GetClientsWithOrdersAsync();
    }

    public async Task<IEnumerable<object>> GetClientsWithProductCountAsync()
    {
        return await _unitOfWork.Clients.GetClientsWithProductCountAsync();
    }
    
}
