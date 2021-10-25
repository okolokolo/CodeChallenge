using System.Collections.Generic;
using System.Linq;
using DataContext;
using DataContext.Models;

namespace CodeChallenge.Repositories
{
    public class MenuItemRepository : IMenuItemRepository
    {
        readonly MenuDbContext _MenuDbContext;

        public MenuItemRepository(MenuDbContext menuDbContext)
        {
            _MenuDbContext = menuDbContext;
        }

        public IQueryable<MenuItem> Get(List<int> ids)
        {
            return _MenuDbContext.MenuItems.Where(x => ids.Contains(x.Id));
        }

    }
}
