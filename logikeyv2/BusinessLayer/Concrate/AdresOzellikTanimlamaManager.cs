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
    public class AdresOzellikTanimlamaManager : IAdresOzellikTanimlamaService
    {
        IAdresOzellikTanimlamaDal _AdresOzellikTanimlamaDal;
        public AdresOzellikTanimlamaManager(IAdresOzellikTanimlamaDal AdresOzellikTanimlamaDal)
        {
            _AdresOzellikTanimlamaDal = AdresOzellikTanimlamaDal;
        }
        public List<AdresOzellikTanimlama> GetAllList(Expression<Func<AdresOzellikTanimlama, bool>> filter)
        {
            return _AdresOzellikTanimlamaDal.GetAllList(filter);
        }

        public AdresOzellikTanimlama GetByID(int id)
        {
            return _AdresOzellikTanimlamaDal.GetByID(id);
        }

        public AdresOzellikTanimlama GetByPropertyName(string propertyName, string value)
        {
            return _AdresOzellikTanimlamaDal.GetByPropertyName(propertyName, value);
        }

        public List<AdresOzellikTanimlama> List()
        {
            return _AdresOzellikTanimlamaDal.GetAllList();
        }

        public void TAdd(AdresOzellikTanimlama t)
        {
            _AdresOzellikTanimlamaDal.Insert(t);
        }

        public void TDelete(AdresOzellikTanimlama t)
        {
            _AdresOzellikTanimlamaDal.Delete(t);
        }

        public void TUpdate(AdresOzellikTanimlama t)
        {
            _AdresOzellikTanimlamaDal.Update(t);
        }
    }
}
