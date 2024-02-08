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
    public class ModullerManager : IModullerService
    {
        IModullerDal _ModullerDal;
        public ModullerManager(IModullerDal ModullerDal)
        {
            _ModullerDal = ModullerDal;
        }
        public List<Moduller> GetAllList(Expression<Func<Moduller, bool>> filter)
        {
            return _ModullerDal.GetAllList(filter);
        }

        public Moduller GetByID(int id)
        {
            return _ModullerDal.GetByID(id);
        }

        public Moduller GetByPropertyName(string propertyName, string value)
        {
            return _ModullerDal.GetByPropertyName(propertyName, value);
        }

        public List<Moduller> List()
        {
            return _ModullerDal.GetAllList();
        }

        public void TAdd(Moduller t)
        {
            _ModullerDal.Insert(t);
        }

        public void TDelete(Moduller t)
        {
            _ModullerDal.Delete(t);
        }

        public void TUpdate(Moduller t)
        {
            _ModullerDal.Update(t);
        }
    }
}
