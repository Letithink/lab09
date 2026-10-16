namespace Labo08_LeticiaMaldonado.DTOs;

public class ClientWithOrderCountDto
{
    public int ClientId { get; set; }
    public string ClientName { get; set; } = string.Empty;
    public int OrderCount { get; set; }
}

public class ProductDetailDto
{
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
}

public class OrderDetailSummaryDto
{
    public int OrderId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
}
