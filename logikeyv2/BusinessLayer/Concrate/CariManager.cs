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
    public class CariManager:ICariService
    {
        ICariDal _CariDal;
        public CariManager(ICariDal CariDal)
        {
            _CariDal = CariDal;
        }


        public List<Cari> GetAllList(Expression<Func<Cari, bool>> filter)
        {
            return _CariDal.GetAllList(filter);
        }

        public Cari GetByID(int id)
        {
            return _CariDal.GetByID(id);
        }

        public Cari GetByPropertyName(string propertyName, string value)
        {
            return _CariDal.GetByPropertyName(propertyName, value);
        }

        public List<Cari> List()
        {
            return _CariDal.GetAllList();
        }

        public void TAdd(Cari t)
        {
            _CariDal.Insert(t);
        }

        public void TDelete(Cari t)
        {
            _CariDal.Delete(t);
        }

        public void TUpdate(Cari t)
        {
            _CariDal.Update(t);
        }
    }
}
