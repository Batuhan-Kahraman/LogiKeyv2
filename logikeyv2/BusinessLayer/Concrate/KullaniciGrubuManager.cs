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
    public class KullaniciGrubuManager : IKullaniciGrubuService
    {
        IKullaniciGrubuDal _KullaniciGrubuDal;
        public KullaniciGrubuManager(IKullaniciGrubuDal KullaniciGrubuDal)
        {
            _KullaniciGrubuDal = KullaniciGrubuDal;
        }
        public List<KullaniciGrubu> GetAllList(Expression<Func<KullaniciGrubu, bool>> filter)
        {
            return _KullaniciGrubuDal.GetAllList(filter);
        }

        public KullaniciGrubu GetByID(int id)
        {
            return _KullaniciGrubuDal.GetByID(id);
        }

        public KullaniciGrubu GetByPropertyName(string propertyName, string value)
        {
            return _KullaniciGrubuDal.GetByPropertyName(propertyName, value);
        }

        public List<KullaniciGrubu> List()
        {
            return _KullaniciGrubuDal.GetAllList();
        }

        public void TAdd(KullaniciGrubu t)
        {
            _KullaniciGrubuDal.Insert(t);
        }

        public void TDelete(KullaniciGrubu t)
        {
            _KullaniciGrubuDal.Delete(t);
        }

        public void TUpdate(KullaniciGrubu t)
        {
            _KullaniciGrubuDal.Update(t);
        }
    }
}
