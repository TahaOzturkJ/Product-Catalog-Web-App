namespace Project.ENTITIES.Models
{
    public class Product : BaseEntity
    {
        public string Model { get; set; }
        public string Positioning { get; set; }
        public string CavoRefNo { get; set; }
        public string ModelYear { get; set; }
        public string Length { get; set; }
        public string OemRefNo { get; set; }
        public int BrandID { get; set; }
        public int CableTypeID { get; set; }

        //Relational Properties

        public virtual Brand Brand { get; set; }
        public virtual CableType CableType { get; set; }


    }
}
