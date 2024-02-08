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
    public class FirmaModulManager:IFirmaModulService
    {
        IFirmaModulDal _FirmaModulDal;
        public FirmaModulManager(IFirmaModulDal FirmaModulDal)
        {
            _FirmaModulDal = FirmaModulDal;
        }
        public List<FirmaModul> GetAllList(Expression<Func<FirmaModul, bool>> filter)
        {
            return _FirmaModulDal.GetAllList(filter);
        }

        public FirmaModul GetByID(int id)
        {
            return _FirmaModulDal.GetByID(id);
        }

        public FirmaModul GetByPropertyName(string propertyName, string value)
        {
            return _FirmaModulDal.GetByPropertyName(propertyName, value);
        }

        public List<FirmaModul> List()
        {
            return _FirmaModulDal.GetAllList();
        }

        public void TAdd(FirmaModul t)
        {
            _FirmaModulDal.Insert(t);
        }

        public void TDelete(FirmaModul t)
        {
            _FirmaModulDal.Delete(t);
        }

        public void TUpdate(FirmaModul t)
        {
            _FirmaModulDal.Update(t);
        }
    }
}
