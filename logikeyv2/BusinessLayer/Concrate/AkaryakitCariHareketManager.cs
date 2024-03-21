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
    public class AkaryakitCariHareketManager:IAkaryakitCariHareketService
    {
        IAkaryakitCariHareketDal _CariHareketDal;
        public AkaryakitCariHareketManager(IAkaryakitCariHareketDal CariHareketDal)
        {
            _CariHareketDal = CariHareketDal;
        }


        public List<AkaryakitCariHareket> GetAllList(Expression<Func<AkaryakitCariHareket, bool>> filter)
        {
            return _CariHareketDal.GetAllList(filter);
        }

        public AkaryakitCariHareket GetByID(int id)
        {
            return _CariHareketDal.GetByID(id);
        }

        public AkaryakitCariHareket GetByPropertyName(string propertyName, string value)
        {
            return _CariHareketDal.GetByPropertyName(propertyName, value);
        }

        public List<AkaryakitCariHareket> List()
        {
            return _CariHareketDal.GetAllList();
        }

        public void TAdd(AkaryakitCariHareket t)
        {
            _CariHareketDal.Insert(t);
        }

        public void TDelete(AkaryakitCariHareket t)
        {
            _CariHareketDal.Delete(t);
        }

        public void TUpdate(AkaryakitCariHareket t)
        {
            _CariHareketDal.Update(t);
        }
    }
}
