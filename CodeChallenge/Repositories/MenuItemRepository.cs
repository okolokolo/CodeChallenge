using System.Collections.Generic;
using System.Linq;
using DataContext;
using DataContext.Models;

namespace CodeChallenge.Repositories
{
    public class MenuItemRepository : IMenuItemRepository
    {
        readonly MenuDbContext _menuDbContext;

        public MenuItemRepository(MenuDbContext menuDbContext)
        {
            _menuDbContext = menuDbContext;
        }

        public IQueryable<MenuItem> Get(List<int> ids)
        {
            return _menuDbContext.MenuItems.Where(x => ids.Contains(x.Id));
        }

    }
}
