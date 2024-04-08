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
    public class MasrafTuruManager : IMasrafTuruService
    {
        IMasrafTuruDal _MasrafTuruDal;

        
        public MasrafTuruManager(IMasrafTuruDal masrafTuruDal)
        {
            _MasrafTuruDal = masrafTuruDal;
        }
        public List<MasrafTuru> GetAllList(Expression<Func<MasrafTuru, bool>> filter)
        {
            return _MasrafTuruDal.GetAllList(filter);
        }
        public MasrafTuru GetByPropertyName(string propertyName, string value)
        {
            return _MasrafTuruDal.GetByPropertyName(propertyName, value);
        }
        public MasrafTuru GetByID(int id)
        {
            return _MasrafTuruDal.GetByID(id);
        }

        public List<MasrafTuru> List()
        {
            return _MasrafTuruDal.GetAllList();
        }

        public void TAdd(MasrafTuru t)
        {
            _MasrafTuruDal.Insert(t);
        }

        public void TDelete(MasrafTuru t)
        {
            _MasrafTuruDal.Delete(t);
        }

        public void TUpdate(MasrafTuru t)
        {
            _MasrafTuruDal.Update(t);
        }
    }
}
