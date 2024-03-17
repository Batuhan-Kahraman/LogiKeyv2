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
    public class CariHareketManager:ICariHareketService
    {
        ICariHareketDal _CariHareketDal;
        public CariHareketManager(ICariHareketDal CariHareketDal)
        {
            _CariHareketDal = CariHareketDal;
        }


        public List<CariHareket> GetAllList(Expression<Func<CariHareket, bool>> filter)
        {
            return _CariHareketDal.GetAllList(filter);
        }

        public CariHareket GetByID(int id)
        {
            return _CariHareketDal.GetByID(id);
        }

        public CariHareket GetByPropertyName(string propertyName, string value)
        {
            return _CariHareketDal.GetByPropertyName(propertyName, value);
        }

        public List<CariHareket> List()
        {
            return _CariHareketDal.GetAllList();
        }

        public void TAdd(CariHareket t)
        {
            _CariHareketDal.Insert(t);
        }

        public void TDelete(CariHareket t)
        {
            _CariHareketDal.Delete(t);
        }

        public void TUpdate(CariHareket t)
        {
            _CariHareketDal.Update(t);
        }
    }
}
