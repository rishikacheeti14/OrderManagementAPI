namespace OrderManagementApi.Models;
public class Order
{
    public int Id { get; set; }
    public string ItemName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalAmount { get; set; }
    public string? UserId { get; set; }  
    public string? UserName { get; set; } 
}
