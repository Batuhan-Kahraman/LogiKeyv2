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
    public class IzinlerManager : IIzinlerService
    {
        IIzinlerDal _IzinlerDal;
        public IzinlerManager(IIzinlerDal IzinlerDal)
        {
            _IzinlerDal = IzinlerDal;
        }
        public List<Izinler> GetAllList(Expression<Func<Izinler, bool>> filter)
        {
            return _IzinlerDal.GetAllList(filter);
        }

        public Izinler GetByID(int id)
        {
            return _IzinlerDal.GetByID(id);
        }

        public Izinler GetByPropertyName(string propertyName, string value)
        {
            return _IzinlerDal.GetByPropertyName(propertyName, value);
        }

        public List<Izinler> List()
        {
            return _IzinlerDal.GetAllList();
        }

        public void TAdd(Izinler t)
        {
            _IzinlerDal.Insert(t);
        }

        public void TDelete(Izinler t)
        {
            _IzinlerDal.Delete(t);
        }

        public void TUpdate(Izinler t)
        {
            _IzinlerDal.Update(t);
        }
    }
}
