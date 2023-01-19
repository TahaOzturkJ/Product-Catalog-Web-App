using Project.ENTITIES.Models;
using System.Data.Entity.ModelConfiguration;

namespace Project.MAP.Options
{
    public abstract class BaseMap<T> : EntityTypeConfiguration<T> where T : BaseEntity
    {
        public BaseMap()
        {
            Property(x => x.CreatedDate).HasColumnName("Veri Yaratma Tarihi").HasColumnType("datetime2").IsRequired();
            Property(x => x.ModifiedDate).HasColumnName("Veri Güncellenme Tarihi").HasColumnType("datetime2");
            Property(x => x.DeletedDate).HasColumnName("Veri Silinme Tarihi").HasColumnType("datetime2");
            Property(x => x.Status).HasColumnName("Veri Durumu").IsRequired();
        }
    }
}
