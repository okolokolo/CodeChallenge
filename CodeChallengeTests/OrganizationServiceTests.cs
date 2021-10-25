using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using CodeChallenge.MappingProfiles;
using CodeChallenge.Repositories;
using CodeChallenge.Services;
using CodeChallenge.ViewModels;
using DataContext;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeChallengeTests
{
    [TestClass]
    public class OrganizationServiceTests
    {
        private IOrderRepository _repository;
        private IMenuItemRepository _menuItemRepository;
        private IMapper _mapper;
        private IOrderService _service;
        private MenuDbContext _context;
        private CreateOrderViewModel vm;

        [TestInitialize]
        public void Setup()
        {
            vm = new CreateOrderViewModel();
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .Build();

            string connectionString = configuration.GetConnectionString("DefaultConnection");
            _context = new MenuDbContext(connectionString);
            _context.SeedData();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new OrganizationMappingProfile());
            });
            _mapper = config.CreateMapper();

            _repository = new OrderRepository(_context);
            _menuItemRepository = new MenuItemRepository(_context);
            _service = new OrderService(_mapper, _repository, _menuItemRepository);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task OrganizationService_CreateOrder_ShouldThrowArgumentNullExceptionForNullMenuItemIds()
        {
            await _service.Create(vm);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task OrganizationService_CreateOrder_ShouldThrowArgumentExceptionForValidMenuItemIds()
        {
            vm.MenuItemIds = new List<int>() {76, 23, 34};
            await _service.Create(vm);
        }

        [TestMethod]
        public async Task OrganizationService_CreateOrder_ShouldReturnWithNoTip()
        {
            vm.MenuItemIds = new List<int>() {1, 2};
            var order = await _service.Create(vm);
            
            Assert.AreEqual(1.50m,order.Cost);
            Assert.AreEqual(0.00m,order.Tip);
            Assert.AreEqual(1.50m, order.Total);
            Assert.AreEqual(DateTime.Today.Date, order.DatePurchased.Date);
            Assert.IsTrue(order.MenuItems.Contains("Cola"));
            Assert.IsTrue(order.MenuItems.Contains("Coffee"));

        }


        [TestMethod]
        public async Task OrganizationService_CreateOrder_ShouldReturnWitTip()
        {
            vm.MenuItemIds = new List<int>() { 2, 3 };
            var order = await _service.Create(vm);

            Assert.AreEqual(3.00m, order.Cost);
            Assert.AreEqual(0.30m, order.Tip);
            Assert.AreEqual(3.30m, order.Total);
            Assert.AreEqual(DateTime.Today.Date, order.DatePurchased.Date);
            Assert.IsTrue(order.MenuItems.Contains("Coffee"));
            Assert.IsTrue(order.MenuItems.Contains("Cheese Sandwich"));
        }

    }
}