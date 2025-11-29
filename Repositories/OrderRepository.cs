using Microsoft.EntityFrameworkCore;
using OrderManagementApi.Data;
using OrderManagementApi.Models;

namespace OrderManagementApi.Repositories;
public class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _db;
    public OrderRepository(ApplicationDbContext db) => _db = db;

    public async Task<Order> AddAsync(Order order)
    {
        _db.Orders.Add(order);
        await _db.SaveChangesAsync();
        return order;
    }

    public Task<Order?> GetByIdAsync(int id) => _db.Orders.FirstOrDefaultAsync(o => o.Id == id);

    public Task<List<Order>> GetByUserIdAsync(string userId) =>
        _db.Orders.Where(o => o.UserId == userId).ToListAsync();

    public async Task UpdateAsync(Order order)
    {
        _db.Orders.Update(order);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(Order order)
    {
        _db.Orders.Remove(order);
        await _db.SaveChangesAsync();
    }
}
