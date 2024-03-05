using EntityLayer.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{


    public interface IAracTurService : IGenericService<AracTur> { }
    public interface IAracTipService : IGenericService<AracTip> { }
    public interface ISahiplikService : IGenericService<Sahiplik> { }
    public interface IDurumlarService : IGenericService<Durumlar> { }
    public interface IMarkaService : IGenericService<Marka> { }
    public interface IModelService : IGenericService<Model> { }
    public interface IGrupService : IGenericService<Grup> { }
    public interface IYakitTipiService : IGenericService<YakitTipi> { }
    public interface IAkuTipiService : IGenericService<AkuTipi> { }
    public interface ILastikTipiService : IGenericService<LastikTipi> { }
    public interface IMasrafTipiService : IGenericService<MasrafTipi> { }
    public interface IAracService : IGenericService<Arac> { }
    public interface IFirmaService : IGenericService<Firma> { }
    public interface IFirmaModulService : IGenericService<FirmaModul> { }
    public interface IIzinlerService : IGenericService<Izinler> { }
    public interface IKullaniciGrubuService : IGenericService<KullaniciGrubu> { }
    public interface IKullaniciYetkilerService : IGenericService<KullaniciYetkiler> { }
    public interface IKullanicilarService : IGenericService<Kullanicilar> { }
    public interface IModullerService : IGenericService<Moduller> { }
    public interface IAdresOzellikTanimlamaService : IGenericService<AdresOzellikTanimlama> { }
    public interface ICariService : IGenericService<Cari> { }
    public interface ICariGrupService : IGenericService<CariGrup> { }
    public interface ICariUcretlendirmeService : IGenericService<CariUcretlendirme> { }

    public interface ICari_OdemeYapanService : IGenericService<Cari_OdemeYapan> { }
    public interface ISurucuCikisNedeniService : IGenericService<SurucuCikisNedeni> { }
    public interface ISurucuPozisyonService : IGenericService<SurucuPozisyon> { }
    public interface IEhliyetSinifiService : IGenericService<EhliyetSinifi> { }
    public interface ISurucuService : IGenericService<Surucu> { }
    public interface IKazaTuruService : IGenericService<KazaTuru> { }
    public interface ITasinacakUrunService : IGenericService<TasinacakUrun> { }
    public interface ITasimaTipiService : IGenericService<TasimaTipi> { }
    public interface IBankalarService : IGenericService<Bankalar> { }
    public interface IBirimlerService : IGenericService<Birimler> { }
    public interface IKazaService : IGenericService<Kaza> { }
    
    public interface IMasrafCezaService : IGenericService<MasrafCeza> { }
    public interface IUnListesiService : IGenericService<UnListesi> { }
    public interface IAracGelirService : IGenericService<AracGelir> { }
    public interface IAracGiderService : IGenericService<AracGider> { }
    public interface ITasimaService : IGenericService<Tasima> { }
    public interface IBildirimService : IGenericService<Bildirim> { }
    public interface IDuyuruService : IGenericService<Duyuru> { }
    public interface IAracResimlerService : IGenericService<AracResimler> { }

}
