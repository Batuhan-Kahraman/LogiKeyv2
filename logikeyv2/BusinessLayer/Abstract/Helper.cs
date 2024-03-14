using DataAccessLayer.Concrate;
using EntityLayer.Concrate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrate
{
    public class Helper:Context
    {
        public static string CariUnvan(int id)
        {
            using (var context = new Context())
            {
                var entity = context.Cari.FirstOrDefault(e => e.Cari_ID == id);
                if (entity != null)
                {
                    return entity.Cari_Unvan;
                }
                else
                {
                    return "";
                }
            }
        }
        public static string UrunAdi(int id)
        {
            using (var context = new Context())
            {
                var entity = context.TasinacakUrun.FirstOrDefault(e => e.ID == id);
                if (entity != null)
                {
                    return entity.Adi;
                }
                else
                {
                    return "";
                }
            }
        }

        public static List<AkaryakitTasimaDetay> AkaryakitTasimaDetayList(int ID)
        {
            using (var context = new Context())
            {
                List<AkaryakitTasimaDetay> liste = context.AkaryakitTasimaDetay.Where(e => e.AkaryakitTasimaID == ID).ToList();
                if (liste != null)
                {
                    return liste;
                }
                else
                {
                    return null;
                }
            }
        }
        public static List<AkaryakitTasimaDetayUrun> AkaryakitTasimaDetayUrunList(int tasimaID,int detayID)
        {
            using (var context = new Context())
            {
                List<AkaryakitTasimaDetayUrun> liste = context.AkaryakitTasimaDetayUrun.Where(e => e.AkaryakitTasimaID == tasimaID && e.AkaryakitTasimaDetayID==detayID).ToList();
                if (liste != null)
                {
                    return liste;
                }
                else
                {
                    return null;
                }
            }
        }
        public static string AracPlaka(int id)
        {
            using (var context = new Context())
            {
                var entity = context.Arac.FirstOrDefault(e => e.ID == id);
                if (entity != null)
                {
                    return entity.Plaka;
                }
                else
                {
                    return "";
                }
            }
        }
        public static string TasimaTipi(int id)
        {
            using (var context = new Context())
            {
                var entity = context.TasimaTipi.FirstOrDefault(e => e.ID == id);
                if (entity != null)
                {
                    return entity.Adi;
                }
                else
                {
                    return "";
                }
            }
        }
    }
}
