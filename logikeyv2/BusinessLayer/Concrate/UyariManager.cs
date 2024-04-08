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
    public class UyariManager : IUyariService
    {
        IUyariDal _UyariDal;
        public UyariManager(IUyariDal UyariDal)
        {
            _UyariDal = UyariDal;
        }


        public List<Uyari> GetAllList(Expression<Func<Uyari, bool>> filter)
        {
            return _UyariDal.GetAllList(filter);
        }

        public Uyari GetByID(int id)
        {
            return _UyariDal.GetByID(id);
        }

        public Uyari GetByPropertyName(string propertyName, string value)
        {
            return _UyariDal.GetByPropertyName(propertyName, value);
        }

        public List<Uyari> List()
        {
            return _UyariDal.GetAllList();
        }

        public void TAdd(Uyari t)
        {
            _UyariDal.Insert(t);
        }

        public void TDelete(Uyari t)
        {
            _UyariDal.Delete(t);
        }

        public void TUpdate(Uyari t)
        {
            _UyariDal.Update(t);
        }
    }
}

