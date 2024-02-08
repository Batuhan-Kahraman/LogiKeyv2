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
    public class KullanicilarManager:IKullanicilarService
    {
        IKullanicilarDal _KullanicilarDal;
        public KullanicilarManager(IKullanicilarDal KullanicilarDal)
        {
            _KullanicilarDal = KullanicilarDal;
        }
        public List<Kullanicilar> GetAllList(Expression<Func<Kullanicilar, bool>> filter)
        {
            return _KullanicilarDal.GetAllList(filter);
        }

        public Kullanicilar GetByID(int id)
        {
            return _KullanicilarDal.GetByID(id);
        }

        public Kullanicilar GetByPropertyName(string propertyName, string value)
        {
            return _KullanicilarDal.GetByPropertyName(propertyName, value);
        }

        public List<Kullanicilar> List()
        {
            return _KullanicilarDal.GetAllList();
        }

        public void TAdd(Kullanicilar t)
        {
            _KullanicilarDal.Insert(t);
        }

        public void TDelete(Kullanicilar t)
        {
            _KullanicilarDal.Delete(t);
        }

        public void TUpdate(Kullanicilar t)
        {
            _KullanicilarDal.Update(t);
        }
    }
}
