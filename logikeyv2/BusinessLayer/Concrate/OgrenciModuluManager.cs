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
    public class OgrenciModuluManager : IOgrenciModuluService
    {
        IOgrenciModuluDal _OgrenciModuluDal;
        public OgrenciModuluManager(IOgrenciModuluDal OgrenciModuluDal)
        {
            _OgrenciModuluDal = OgrenciModuluDal;
        }


        public List<OgrenciModulu> GetAllList(Expression<Func<OgrenciModulu, bool>> filter)
        {
            return _OgrenciModuluDal.GetAllList(filter);
        }

        public OgrenciModulu GetByID(int id)
        {
            return _OgrenciModuluDal.GetByID(id);
        }

        public OgrenciModulu GetByPropertyName(string propertyName, string value)
        {
            return _OgrenciModuluDal.GetByPropertyName(propertyName, value);
        }

        public List<OgrenciModulu> List()
        {
            return _OgrenciModuluDal.GetAllList();
        }

        public void TAdd(OgrenciModulu t)
        {
            _OgrenciModuluDal.Insert(t);
        }

        public void TDelete(OgrenciModulu t)
        {
            _OgrenciModuluDal.Delete(t);
        }

        public void TUpdate(OgrenciModulu t)
        {
            _OgrenciModuluDal.Update(t);
        }
    }
}
