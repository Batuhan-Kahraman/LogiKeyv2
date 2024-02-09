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
    public class CariUcretlendirmeManager:ICariUcretlendirmeService
    {
        ICariUcretlendirmeDal _CariUcretlendirmeDal;
        public CariUcretlendirmeManager(ICariUcretlendirmeDal CariUcretlendirmeDal)
        {
            _CariUcretlendirmeDal = CariUcretlendirmeDal;
        }


        public List<CariUcretlendirme> GetAllList(Expression<Func<CariUcretlendirme, bool>> filter)
        {
            return _CariUcretlendirmeDal.GetAllList(filter);
        }

        public CariUcretlendirme GetByID(int id)
        {
            return _CariUcretlendirmeDal.GetByID(id);
        }

        public CariUcretlendirme GetByPropertyName(string propertyName, string value)
        {
            return _CariUcretlendirmeDal.GetByPropertyName(propertyName, value);
        }

        public List<CariUcretlendirme> List()
        {
            return _CariUcretlendirmeDal.GetAllList();
        }

        public void TAdd(CariUcretlendirme t)
        {
            _CariUcretlendirmeDal.Insert(t);
        }

        public void TDelete(CariUcretlendirme t)
        {
            _CariUcretlendirmeDal.Delete(t);
        }

        public void TUpdate(CariUcretlendirme t)
        {
            _CariUcretlendirmeDal.Update(t);
        }
    }
}
