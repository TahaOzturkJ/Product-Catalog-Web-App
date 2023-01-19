using Project.ENTITIES.Models;

namespace Project.MAP.Options
{
    public class AppUserMap : BaseMap<AppUser>
    {
        public AppUserMap()
        {
            ToTable("Kullanicilar");
            Property(x => x.UserName).HasColumnName("KullaniciAdi").IsRequired();
            Property(x => x.Password).HasColumnName("Sifre").IsRequired();
            Property(x => x.Role).HasColumnName("KullaniciRolü").IsRequired();
        }
    }
}
