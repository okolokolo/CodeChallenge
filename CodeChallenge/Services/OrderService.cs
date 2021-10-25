using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CodeChallenge.Repositories;
using CodeChallenge.ViewModels;
using DataContext.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeChallenge.Services
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _repository;
        private readonly IMenuItemRepository _menuItemRepository;

        public OrderService(
            IMapper mapper,
            IOrderRepository repository,
            IMenuItemRepository menuItemRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _menuItemRepository = menuItemRepository;
        }

        public async Task<OrderViewModel> Create(CreateOrderViewModel vm)
        {
            if (vm.MenuItemIds == null)
                throw new ArgumentNullException($"{nameof(vm.MenuItemIds)} is required!");

            var menuItems = _menuItemRepository.Get(vm.MenuItemIds);
            var hasItems = await menuItems.AnyAsync();

            if (!hasItems) throw new ArgumentException("No valid menu items were passed!");

            var sum = menuItems.Sum(i => i.Price);
            var cost = Math.Round(sum, 2);
            var includeTip = menuItems.Any(i => i.IsAFoodItem);
            var tip = (includeTip) ? Math.Round(cost * 0.10,2) : 0.0;
            var orderMenuItems = menuItems.Select(i => new OrderMenuItem()
            {
                MenuItemId = i.Id
            }).ToList();
            var order = new Order()
            {
                Cost = Convert.ToDecimal(cost),
                Tip = Convert.ToDecimal(tip),
                MenuItems = orderMenuItems
            };
            var createdOrder = await _repository.CreateOrder(order);
            var orderViewModel = _mapper.Map<OrderViewModel>(createdOrder);
            return orderViewModel;
        }
    }
}
