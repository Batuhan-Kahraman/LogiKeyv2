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
    public class FaturaOkulManager:IFaturaOkulService
    {
        IFaturaOkulDal _FaturaOkulDal;
        public FaturaOkulManager(IFaturaOkulDal FaturaOkulDal)
        {
            _FaturaOkulDal = FaturaOkulDal;
        }


        public List<FaturaOkul> GetAllList(Expression<Func<FaturaOkul, bool>> filter)
        {
            return _FaturaOkulDal.GetAllList(filter);
        }

        public FaturaOkul GetByID(int id)
        {
            return _FaturaOkulDal.GetByID(id);
        }

        public FaturaOkul GetByPropertyName(string propertyName, string value)
        {
            return _FaturaOkulDal.GetByPropertyName(propertyName, value);
        }

        public List<FaturaOkul> List()
        {
            return _FaturaOkulDal.GetAllList();
        }

        public void TAdd(FaturaOkul t)
        {
            _FaturaOkulDal.Insert(t);
        }

        public void TDelete(FaturaOkul t)
        {
            _FaturaOkulDal.Delete(t);
        }

        public void TUpdate(FaturaOkul t)
        {
            _FaturaOkulDal.Update(t);
        }
    }
}
