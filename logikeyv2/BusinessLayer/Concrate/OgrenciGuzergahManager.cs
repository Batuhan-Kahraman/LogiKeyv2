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
    public class OgrenciGuzergahManager:IOgrenciGuzergahService
    {
      
        IOgrenciGuzergahDal _OgrenciGuzergahDal;
        public OgrenciGuzergahManager(IOgrenciGuzergahDal OgrenciGuzergahDal)
        {
            _OgrenciGuzergahDal = OgrenciGuzergahDal;
        }
        public List<OgrenciGuzergah> GetAllList(Expression<Func<OgrenciGuzergah, bool>> filter)
        {
            return _OgrenciGuzergahDal.GetAllList(filter);
        }

        public OgrenciGuzergah GetByID(int id)
        {
            return _OgrenciGuzergahDal.GetByID(id);
        }

        public OgrenciGuzergah GetByPropertyName(string propertyName, string value)
        {
            return _OgrenciGuzergahDal.GetByPropertyName(propertyName, value);
        }

        public List<OgrenciGuzergah> List()
        {
            return _OgrenciGuzergahDal.GetAllList();
        }

        public void TAdd(OgrenciGuzergah t)
        {
            _OgrenciGuzergahDal.Insert(t);
        }

        public void TDelete(OgrenciGuzergah t)
        {
            _OgrenciGuzergahDal.Delete(t);
        }

        public void TUpdate(OgrenciGuzergah t)
        {
            _OgrenciGuzergahDal.Update(t);
        }
    }
}
