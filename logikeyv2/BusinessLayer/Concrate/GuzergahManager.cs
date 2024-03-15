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
    public class GuzergahManager:IGuzergahService
    {
        IGuzergahDal _GuzergahDal;
        public GuzergahManager(IGuzergahDal GuzergahDal)
        {
            _GuzergahDal = GuzergahDal;
        }


        public List<Guzergah> GetAllList(Expression<Func<Guzergah, bool>> filter)
        {
            return _GuzergahDal.GetAllList(filter);
        }

        public Guzergah GetByID(int id)
        {
            return _GuzergahDal.GetByID(id);
        }

        public Guzergah GetByPropertyName(string propertyName, string value)
        {
            return _GuzergahDal.GetByPropertyName(propertyName, value);
        }

        public List<Guzergah> List()
        {
            return _GuzergahDal.GetAllList();
        }

        public void TAdd(Guzergah t)
        {
            _GuzergahDal.Insert(t);
        }

        public void TDelete(Guzergah t)
        {
            _GuzergahDal.Delete(t);
        }

        public void TUpdate(Guzergah t)
        {
            _GuzergahDal.Update(t);
        }
    }
}
