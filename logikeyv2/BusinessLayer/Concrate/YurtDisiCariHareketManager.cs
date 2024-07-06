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
    public class YurtDisiCariHareketManager:IYurtDisiCariHareketService
    {
        IYurtDisiCariHareketDal _CariHareketDal;
        public YurtDisiCariHareketManager(IYurtDisiCariHareketDal CariHareketDal)
        {
            _CariHareketDal = CariHareketDal;
        }


        public List<YurtDisiCariHareket> GetAllList(Expression<Func<YurtDisiCariHareket, bool>> filter)
        {
            return _CariHareketDal.GetAllList(filter);
        }

        public YurtDisiCariHareket GetByID(int id)
        {
            return _CariHareketDal.GetByID(id);
        }

        public YurtDisiCariHareket GetByPropertyName(string propertyName, string value)
        {
            return _CariHareketDal.GetByPropertyName(propertyName, value);
        }

        public List<YurtDisiCariHareket> List()
        {
            return _CariHareketDal.GetAllList();
        }

        public void TAdd(YurtDisiCariHareket t)
        {
            _CariHareketDal.Insert(t);
        }

        public void TDelete(YurtDisiCariHareket t)
        {
            _CariHareketDal.Delete(t);
        }

        public void TUpdate(YurtDisiCariHareket t)
        {
            _CariHareketDal.Update(t);
        }
    }
}
