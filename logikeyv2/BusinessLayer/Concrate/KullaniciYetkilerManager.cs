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
    public class KullaniciYetkilerManager:IKullaniciYetkilerService
    {
        IKullaniciYetkileriDal _KullaniciYetkileriDal;
        public KullaniciYetkilerManager(IKullaniciYetkileriDal KullaniciYetkileriDal)
        {
            _KullaniciYetkileriDal = KullaniciYetkileriDal;
        }
        public List<KullaniciYetkiler> GetAllList(Expression<Func<KullaniciYetkiler, bool>> filter)
        {
            return _KullaniciYetkileriDal.GetAllList(filter);
        }

        public KullaniciYetkiler GetByID(int id)
        {
            return _KullaniciYetkileriDal.GetByID(id);
        }

        public KullaniciYetkiler GetByPropertyName(string propertyName, string value)
        {
            return _KullaniciYetkileriDal.GetByPropertyName(propertyName, value);
        }

        public List<KullaniciYetkiler> List()
        {
            return _KullaniciYetkileriDal.GetAllList();
        }

        public void TAdd(KullaniciYetkiler t)
        {
            _KullaniciYetkileriDal.Insert(t);
        }

        public void TDelete(KullaniciYetkiler t)
        {
            _KullaniciYetkileriDal.Delete(t);
        }

        public void TUpdate(KullaniciYetkiler t)
        {
            _KullaniciYetkileriDal.Update(t);
        }
    }
}
