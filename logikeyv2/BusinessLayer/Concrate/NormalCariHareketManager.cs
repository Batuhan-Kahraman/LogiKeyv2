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
    public class NormalCariHareketManager:INormalCariHareketService
    {
        INormalCariHareketDal _CariHareketDal;
        public NormalCariHareketManager(INormalCariHareketDal CariHareketDal)
        {
            _CariHareketDal = CariHareketDal;
        }


        public List<NormalCariHareket> GetAllList(Expression<Func<NormalCariHareket, bool>> filter)
        {
            return _CariHareketDal.GetAllList(filter);
        }

        public NormalCariHareket GetByID(int id)
        {
            return _CariHareketDal.GetByID(id);
        }

        public NormalCariHareket GetByPropertyName(string propertyName, string value)
        {
            return _CariHareketDal.GetByPropertyName(propertyName, value);
        }

        public List<NormalCariHareket> List()
        {
            return _CariHareketDal.GetAllList();
        }

        public void TAdd(NormalCariHareket t)
        {
            _CariHareketDal.Insert(t);
        }

        public void TDelete(NormalCariHareket t)
        {
            _CariHareketDal.Delete(t);
        }

        public void TUpdate(NormalCariHareket t)
        {
            _CariHareketDal.Update(t);
        }
    }
}
