using OrderManagementApi.Models;
using OrderManagementApi.Repositories;

namespace OrderManagementApi.Services;
public class OrderService
{
    private readonly IOrderRepository _repo;
    public OrderService(IOrderRepository repo) => _repo = repo;

    public Task<List<Order>> GetUserOrdersAsync(string userId) => _repo.GetByUserIdAsync(userId);

    public Task<Order?> GetOrderAsync(int id) => _repo.GetByIdAsync(id);

    public async Task<Order> CreateOrderAsync(string userId, string userName, OrderDto dto)
    {
        var order = new Order
        {
            ItemName = dto.ItemName,
            Quantity = dto.Quantity,
            UnitPrice = dto.UnitPrice,
            TotalAmount = dto.Quantity * dto.UnitPrice,
            UserId = userId,
            UserName = userName
        };
        return await _repo.AddAsync(order);
    }

    public async Task<bool> UpdateOrderAsync(int id, string userId, OrderDto dto)
    {
        var order = await _repo.GetByIdAsync(id);
        if (order == null || order.UserId != userId) return false;
        order.ItemName = dto.ItemName;
        order.Quantity = dto.Quantity;
        order.UnitPrice = dto.UnitPrice;
        order.TotalAmount = dto.Quantity * dto.UnitPrice;
        await _repo.UpdateAsync(order);
        return true;
    }

    public async Task<bool> DeleteOrderAsync(int id, string userId)
    {
        var order = await _repo.GetByIdAsync(id);
        if (order == null || order.UserId != userId) return false;
        await _repo.DeleteAsync(order);
        return true;
    }
}
