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
    public class OgrenciTahsilatManager : IOgrenciTahsilatService
    {
        IOgrenciTahsilatDal _ogreciTahsilatDal;

        public OgrenciTahsilatManager(IOgrenciTahsilatDal ogreciTahsilatDal)
        {
            _ogreciTahsilatDal = ogreciTahsilatDal;
        }
        public List<OgrenciTahsilat> GetAllList(Expression<Func<OgrenciTahsilat, bool>> filter)
        {
            return _ogreciTahsilatDal.GetAllList(filter);
        }
        public OgrenciTahsilat GetByPropertyName(string propertyName, string value)
        {
            return _ogreciTahsilatDal.GetByPropertyName(propertyName, value);
        }
        public OgrenciTahsilat GetByID(int id)
        {
            return _ogreciTahsilatDal.GetByID(id);
        }

        public List<OgrenciTahsilat> List()
        {
            return _ogreciTahsilatDal.GetAllList();
        }

        public void TAdd(OgrenciTahsilat t)
        {
            _ogreciTahsilatDal.Insert(t);
        }

        public void TDelete(OgrenciTahsilat t)
        {
            _ogreciTahsilatDal.Delete(t);
        }

        public void TUpdate(OgrenciTahsilat t)
        {
            _ogreciTahsilatDal.Update(t);
        }
    }
}
