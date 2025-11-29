using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderManagementApi.Models;
using OrderManagementApi.Services;
using System.Security.Claims;

namespace OrderManagementApi.Controller;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly OrderService _service;
    public OrdersController(OrderService service) => _service = service;

    // GET api/orders
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var list = await _service.GetUserOrdersAsync(userId!);
        return Ok(list);
    }

    // GET api/orders/5
    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var order = await _service.GetOrderAsync(id);
        if (order == null || order.UserId != userId) return NotFound();
        return Ok(order);
    }

    // POST api/orders
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] OrderDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var userName = User.FindFirstValue(ClaimTypes.Name)!;
        var created = await _service.CreateOrderAsync(userId, userName, dto);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    // PUT api/orders/5
    [Authorize]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] OrderDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var ok = await _service.UpdateOrderAsync(id, userId, dto);
        if (!ok) return NotFound();
        return NoContent();
    }

    // DELETE api/orders/5
    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var ok = await _service.DeleteOrderAsync(id, userId);
        if (!ok) return NotFound();
        return NoContent();
    }
}
