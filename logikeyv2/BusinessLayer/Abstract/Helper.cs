using DataAccessLayer.Concrate;
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
    }
}
