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
    public class UyariTipManager : IUyariTipService
    {
        IUyariTipDal _UyariTipDal;
        public UyariTipManager(IUyariTipDal UyariTipDal)
        {
            _UyariTipDal = UyariTipDal;
        }


        public List<UyariTip> GetAllList(Expression<Func<UyariTip, bool>> filter)
        {
            return _UyariTipDal.GetAllList(filter);
        }

        public UyariTip GetByID(int id)
        {
            return _UyariTipDal.GetByID(id);
        }

        public UyariTip GetByPropertyName(string propertyName, string value)
        {
            return _UyariTipDal.GetByPropertyName(propertyName, value);
        }

        public List<UyariTip> List()
        {
            return _UyariTipDal.GetAllList();
        }

        public void TAdd(UyariTip t)
        {
            _UyariTipDal.Insert(t);
        }

        public void TDelete(UyariTip t)
        {
            _UyariTipDal.Delete(t);
        }

        public void TUpdate(UyariTip t)
        {
            _UyariTipDal.Update(t);
        }
    }
}

