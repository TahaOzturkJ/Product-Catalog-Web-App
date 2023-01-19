using System.Collections.Generic;

namespace Project.ENTITIES.Models
{
    public class Brand : BaseEntity
    {
        public string BrandName { get; set; }

        //Relational Properties

        public virtual List<Product> Products { get; set; }
    }
}
