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
    public class FirmaManager : IFirmaService
    {
        IFirmaDal _FirmaDal;
        public FirmaManager(IFirmaDal FirmaDal)
        {
            _FirmaDal = FirmaDal;
        }
        public List<Firma> GetAllList(Expression<Func<Firma, bool>> filter)
        {
            return _FirmaDal.GetAllList(filter);
        }

        public Firma GetByID(int id)
        {
            return _FirmaDal.GetByID(id);
        }

        public Firma GetByPropertyName(string propertyName, string value)
        {
            return _FirmaDal.GetByPropertyName(propertyName, value);
        }

        public List<Firma> List()
        {
            return _FirmaDal.GetAllList();
        }

        public void TAdd(Firma t)
        {
            _FirmaDal.Insert(t);
        }

        public void TDelete(Firma t)
        {
            _FirmaDal.Delete(t);
        }

        public void TUpdate(Firma t)
        {
            _FirmaDal.Update(t);
        }
    }
}
