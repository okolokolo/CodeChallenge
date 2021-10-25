using System.Threading.Tasks;
using DataContext;
using DataContext.Models;

namespace CodeChallenge.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        readonly MenuDbContext _MenuDbContext;

        public OrderRepository(MenuDbContext menuDbContext)
        {
            _MenuDbContext = menuDbContext;
        }

        public async Task<Order> CreateOrder(Order order)
        {
            await _MenuDbContext.Orders.AddAsync(order);
            await _MenuDbContext.SaveChangesAsync();
            return order;
        } 

    }
}
