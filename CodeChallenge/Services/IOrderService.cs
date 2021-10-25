using System.Threading.Tasks;
using CodeChallenge.ViewModels;

namespace CodeChallenge.Services
{
    public interface IOrderService
    {
        Task<OrderViewModel> Create(CreateOrderViewModel vm);
    }
}