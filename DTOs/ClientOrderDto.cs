namespace Labo08_LeticiaMaldonado.DTOs;
public class ClientOrderDto
{
    public string ClientName { get; set; }
    public List<OrderDto> Orders { get; set; }
}