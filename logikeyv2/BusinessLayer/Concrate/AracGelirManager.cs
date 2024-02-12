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
    public class AracGelirManager:IAracGelirService
    {
        IAracGelirDal _AracGelirDal;
        public AracGelirManager(IAracGelirDal AracGelirDal)
        {
            _AracGelirDal = AracGelirDal;
        }


        public List<AracGelir> GetAllList(Expression<Func<AracGelir, bool>> filter)
        {
            return _AracGelirDal.GetAllList(filter);
        }

        public AracGelir GetByID(int id)
        {
            return _AracGelirDal.GetByID(id);
        }

        public AracGelir GetByPropertyName(string propertyName, string value)
        {
            return _AracGelirDal.GetByPropertyName(propertyName, value);
        }

        public List<AracGelir> List()
        {
            return _AracGelirDal.GetAllList();
        }

        public void TAdd(AracGelir t)
        {
            _AracGelirDal.Insert(t);
        }

        public void TDelete(AracGelir t)
        {
            _AracGelirDal.Delete(t);
        }

        public void TUpdate(AracGelir t)
        {
            _AracGelirDal.Update(t);
        }
    }
}
