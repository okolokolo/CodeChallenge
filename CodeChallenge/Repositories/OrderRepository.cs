using System.Linq;
using System.Threading.Tasks;
using DataContext;
using DataContext.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeChallenge.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        readonly MenuDbContext _menuDbContext;

        public OrderRepository(MenuDbContext menuDbContext)
        {
            _menuDbContext = menuDbContext;
        }

        public async Task<Order> CreateOrder(Order order)
        {
            await _menuDbContext.Orders.AddAsync(order);
            await _menuDbContext.SaveChangesAsync();
            var menuItems = await _menuDbContext.OrderMenuItems.Include(omi => omi.MenuItem)
                .Where(x => x.OrderId == order.Id)
                .ToListAsync();
            order.MenuItems = menuItems;
            return order;
        } 

    }
}
