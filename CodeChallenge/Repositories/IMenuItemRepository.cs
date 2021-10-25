using System.Collections.Generic;
using System.Linq;
using DataContext.Models;

namespace CodeChallenge.Repositories
{
    public interface IMenuItemRepository
    {
        IQueryable<MenuItem> Get(List<int> ids);
    }
}