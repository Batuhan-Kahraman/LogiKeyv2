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
    public class Okul_GuzergahManager:IOkul_GuzergahService
    {
        IOkul_GuzergahDal _Okul_GuzergahDal;
        public Okul_GuzergahManager(IOkul_GuzergahDal Okul_GuzergahDal)
        {
            _Okul_GuzergahDal = Okul_GuzergahDal;
        }


        public List<Okul_Guzergah> GetAllList(Expression<Func<Okul_Guzergah, bool>> filter)
        {
            return _Okul_GuzergahDal.GetAllList(filter);
        }

        public Okul_Guzergah GetByID(int id)
        {
            return _Okul_GuzergahDal.GetByID(id);
        }

        public Okul_Guzergah GetByPropertyName(string propertyName, string value)
        {
            return _Okul_GuzergahDal.GetByPropertyName(propertyName, value);
        }

        public List<Okul_Guzergah> List()
        {
            return _Okul_GuzergahDal.GetAllList();
        }

        public void TAdd(Okul_Guzergah t)
        {
            _Okul_GuzergahDal.Insert(t);
        }

        public void TDelete(Okul_Guzergah t)
        {
            _Okul_GuzergahDal.Delete(t);
        }

        public void TUpdate(Okul_Guzergah t)
        {
            _Okul_GuzergahDal.Update(t);
        }
    }
}
