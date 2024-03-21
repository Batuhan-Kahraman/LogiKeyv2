
using EntityLayer.Concrate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccessLayer.Concrate
{
    public class Context : DbContext

    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=46.45.144.46;Database=LOGIKEY_Lojistikv2;User Id=batuhan.tmt;Password=KaleW356!;Encrypt=True;TrustServerCertificate=True;");
            //optionsBuilder.UseSqlServer("Server=LAPTOP-C1ITV0VV\\SQLEXPRESS;Database=logikey;Trusted_Connection=True;");

        }
        public DbSet<AdresOzellikTanimlama> AdresOzellikTanimlama { get; set; }

        public DbSet<AracTur> AracTur { get; set; }
        public DbSet<AracTip> AracTip { get; set; }
        public DbSet<Sahiplik> Sahiplik { get; set; }
        public DbSet<Durumlar> Durumlar { get; set; }
        public DbSet<Marka> Marka { get; set; }
        public DbSet<YakitTipi> YakitTipi { get; set; }
        public DbSet<AkuTipi> AkuTipi { get; set; }
        public DbSet<Grup> Grup { get; set; }
        public DbSet<Model> Model { get;set; }
        public DbSet<LastikTipi> LastikTipi { get; set; }
        public DbSet<MasrafTipi> MasrafTipi { get; set; }
        public DbSet<Arac> Arac { get; set; }
        public DbSet<Firma> Firma { get; set; }
        public DbSet<FirmaModul> FirmaModul { get; set; }
        public DbSet<Izinler> Izinler { get; set; }
        public DbSet<KullaniciGrubu> KullaniciGrubu { get; set; }
        public DbSet<KullaniciYetkiler> KullaniciYetkiler { get; set; }
        public DbSet<Moduller> Moduller { get; set; }
        public DbSet<Kullanicilar> Kullanicilar { get; set; }
        public DbSet<Cari> Cari { get; set; }

        public DbSet<CariGrup> CariGrup { get; set; }
        public DbSet<CariUcretlendirme> CariUcretlendirme { get; set; }
        public DbSet<Cari_OdemeYapan> Cari_OdemeYapan { get; set; }
        public DbSet<EhliyetSinifi> EhliyetSinifi { get; set; }
        public DbSet<SurucuPozisyon> SurucuPozisyon { get; set; }
        public DbSet<SurucuCikisNedeni> SurucuCikisNedeni { get; set; }
        public DbSet<KazaTuru> KazaTuru { get; set; }
        public DbSet<TasinacakUrun> TasinacakUrun { get; set; }
        public DbSet<TasimaTipi> TasimaTipi { get; set; }
        public DbSet<Bankalar> Bankalar { get; set; }
        public DbSet<Birimler> Birimler { get; set; }
        public DbSet<Kaza> Kaza { get; set; }
        public DbSet<MasrafCeza> MasrafCeza { get; set; }
        public DbSet<UnListesi> UnListesi { get; set; }
        public DbSet<AracGelir> AracGelir { get; set; }
        public DbSet<AracGider> AracGider { get; set; }
        public DbSet<Tasima> Tasima { get; set; }

        public DbSet<Bildirim> Bildirim { get; set; }
        public DbSet<Duyuru> Duyuru { get; set; }
        public DbSet<AracResimler> AracResimler { get; set; }
        public DbSet<OgrenciModulu> OgrenciModulu { get; set; }


        public DbSet<AkaryakitTasima> AkaryakitTasima { get; set; }
        public DbSet<AkaryakitTasimaDetay> AkaryakitTasimaDetay { get; set; }
        public DbSet<AkaryakitTasimaDetayUrun> AkaryakitTasimaDetayUrun { get; set; }
        public DbSet<AkaryakitFatura> AkaryakitFatura { get; set; }
        public DbSet<AkaryakitAracTur> AkaryakitAracTur { get; set; }

        public DbSet<Guzergah> Guzergah { get; set; }
        public DbSet<Okul_Guzergah> Okul_Guzergah { get; set; }
        public DbSet<OgrenciGuzergah> OgrenciGuzergah { get; set; }
        public DbSet<FaturaOkul> FaturaOkul { get; set; }
        public DbSet<AkaryakitCariHareket> AkaryakitCariHareket { get; set; }

    }
}
