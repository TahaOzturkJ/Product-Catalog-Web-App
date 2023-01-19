using Project.ENTITIES.Models;

namespace Project.MAP.Options
{
    public class ProductMap : BaseMap<Product>
    {
        public ProductMap()
        {
            ToTable("Urunler");
            Property(x => x.Model).IsRequired();
            Property(x => x.Positioning).HasColumnName("Pozisyon").IsRequired();
            Property(x => x.CavoRefNo).IsRequired();
            Property(x => x.ModelYear).HasColumnName("Sene").IsRequired();
            Property(x => x.Length).HasColumnName("Uzunluk").IsRequired();
            Property(x => x.OemRefNo).IsRequired();
        }
    }
}
