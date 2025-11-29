using OrderManagementApi.Models;

namespace OrderManagementApi.Repositories;
public interface IOrderRepository
{
    Task<Order> AddAsync(Order order);
    Task<Order?> GetByIdAsync(int id);
    Task<List<Order>> GetByUserIdAsync(string userId);
    Task UpdateAsync(Order order);
    Task DeleteAsync(Order order);
}
