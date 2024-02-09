using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrate
{
    public class Cari_OdemeYapanManager:ICari_OdemeYapanService
    {
        ICari_OdemeYapanDal _Cari_OdemeYapanDal;
        public Cari_OdemeYapanManager(ICari_OdemeYapanDal Cari_OdemeYapanDal)
        {
            _Cari_OdemeYapanDal = Cari_OdemeYapanDal;
        }


        public List<Cari_OdemeYapan> GetAllList(Expression<Func<Cari_OdemeYapan, bool>> filter)
        {
            return _Cari_OdemeYapanDal.GetAllList(filter);
        }

        public Cari_OdemeYapan GetByID(int id)
        {
            return _Cari_OdemeYapanDal.GetByID(id);
        }

        public Cari_OdemeYapan GetByPropertyName(string propertyName, string value)
        {
            return _Cari_OdemeYapanDal.GetByPropertyName(propertyName, value);
        }

        public List<Cari_OdemeYapan> List()
        {
            return _Cari_OdemeYapanDal.GetAllList();
        }

        public void TAdd(Cari_OdemeYapan t)
        {
            _Cari_OdemeYapanDal.Insert(t);
        }

        public void TDelete(Cari_OdemeYapan t)
        {
            _Cari_OdemeYapanDal.Delete(t);
        }

        public void TUpdate(Cari_OdemeYapan t)
        {
            _Cari_OdemeYapanDal.Update(t);
        }
    }
}
