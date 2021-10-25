using System.Threading.Tasks;
using DataContext.Models;

namespace CodeChallenge.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> CreateOrder(Order order);
    }
}