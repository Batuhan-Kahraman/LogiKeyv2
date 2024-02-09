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
    public class CariGrupManager:ICariGrupService
    {
        ICariGrupDal _CariGrupDal;
        public CariGrupManager(ICariGrupDal CariGrupDal)
        {
            _CariGrupDal = CariGrupDal;
        }


        public List<CariGrup> GetAllList(Expression<Func<CariGrup, bool>> filter)
        {
            return _CariGrupDal.GetAllList(filter);
        }

        public CariGrup GetByID(int id)
        {
            return _CariGrupDal.GetByID(id);
        }

        public CariGrup GetByPropertyName(string propertyName, string value)
        {
            return _CariGrupDal.GetByPropertyName(propertyName, value);
        }

        public List<CariGrup> List()
        {
            return _CariGrupDal.GetAllList();
        }

        public void TAdd(CariGrup t)
        {
            _CariGrupDal.Insert(t);
        }

        public void TDelete(CariGrup t)
        {
            _CariGrupDal.Delete(t);
        }

        public void TUpdate(CariGrup t)
        {
            _CariGrupDal.Update(t);
        }
    }
}
