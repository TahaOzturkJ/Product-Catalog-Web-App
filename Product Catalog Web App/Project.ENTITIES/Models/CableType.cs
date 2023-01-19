using System.Collections.Generic;

namespace Project.ENTITIES.Models
{
    public class CableType : BaseEntity
    {
        public string CableTypeName { get; set; }

        //Relational Properties
        public virtual List<Product> Products { get; set; }
    }
}
