using Project.ENTITIES.Models;
using System.Collections.Generic;

namespace Project.WEBUI.VMClasses
{
    public class IndexVM
    {
        public List<AppUser> AppUsers { get; set; }
        public AppUser AppUser { get; set; }

        public List<CableType> CableTypes { get; set; }
        public CableType CableType { get; set; }

        public List<Brand> Brands { get; set; }
        public Brand Brand { get; set; }

        public List<Product> Products { get; set; }
        public Product Product { get; set; }
    }
}