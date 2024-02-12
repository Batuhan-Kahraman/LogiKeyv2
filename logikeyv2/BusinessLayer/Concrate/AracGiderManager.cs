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
    public class AracGiderManager:IAracGiderService
    {
        IAracGiderDal _AracGiderDal;
        public AracGiderManager(IAracGiderDal AracGiderDal)
        {
            _AracGiderDal = AracGiderDal;
        }


        public List<AracGider> GetAllList(Expression<Func<AracGider, bool>> filter)
        {
            return _AracGiderDal.GetAllList(filter);
        }

        public AracGider GetByID(int id)
        {
            return _AracGiderDal.GetByID(id);
        }

        public AracGider GetByPropertyName(string propertyName, string value)
        {
            return _AracGiderDal.GetByPropertyName(propertyName, value);
        }

        public List<AracGider> List()
        {
            return _AracGiderDal.GetAllList();
        }

        public void TAdd(AracGider t)
        {
            _AracGiderDal.Insert(t);
        }

        public void TDelete(AracGider t)
        {
            _AracGiderDal.Delete(t);
        }

        public void TUpdate(AracGider t)
        {
            _AracGiderDal.Update(t);
        }
    }
}
