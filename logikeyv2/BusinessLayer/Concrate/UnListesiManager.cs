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
    public class UnListesiManager:IUnListesiService
    {
        IUnListesiDal _UnListesiDal;
        public UnListesiManager(IUnListesiDal UnListesiDal)
        {
            _UnListesiDal = UnListesiDal;
        }


        public List<UnListesi> GetAllList(Expression<Func<UnListesi, bool>> filter)
        {
            return _UnListesiDal.GetAllList(filter);
        }

        public UnListesi GetByID(int id)
        {
            return _UnListesiDal.GetByID(id);
        }

        public UnListesi GetByPropertyName(string propertyName, string value)
        {
            return _UnListesiDal.GetByPropertyName(propertyName, value);
        }

        public List<UnListesi> List()
        {
            return _UnListesiDal.GetAllList();
        }

        public void TAdd(UnListesi t)
        {
            _UnListesiDal.Insert(t);
        }

        public void TDelete(UnListesi t)
        {
            _UnListesiDal.Delete(t);
        }

        public void TUpdate(UnListesi t)
        {
            _UnListesiDal.Update(t);
        }
    }
}
