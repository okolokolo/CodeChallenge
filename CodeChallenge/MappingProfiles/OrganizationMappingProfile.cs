using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CodeChallenge.ViewModels;
using DataContext.Models;

namespace CodeChallenge.MappingProfiles
{
    public class OrganizationMappingProfile : Profile
    {
        public OrganizationMappingProfile()
        {
            CreateMap<Order, OrderViewModel>()
                .ForMember(dest => dest.MenuItems,
                    opt => opt.MapFrom(src => GetMenuItemNames(src)));
        }

        private static List<string> GetMenuItemNames(Order src)
        {
            return src.MenuItems.Select(i => i.MenuItem.Name).ToList();
        }
    }
}
