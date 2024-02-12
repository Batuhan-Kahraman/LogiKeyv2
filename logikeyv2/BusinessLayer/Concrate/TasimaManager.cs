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
    public class TasimaManager:ITasimaService
    {
        ITasimaDal _TasimaDal;
        public TasimaManager(ITasimaDal TasimaDal)
        {
            _TasimaDal = TasimaDal;
        }


        public List<Tasima> GetAllList(Expression<Func<Tasima, bool>> filter)
        {
            return _TasimaDal.GetAllList(filter);
        }

        public Tasima GetByID(int id)
        {
            return _TasimaDal.GetByID(id);
        }

        public Tasima GetByPropertyName(string propertyName, string value)
        {
            return _TasimaDal.GetByPropertyName(propertyName, value);
        }

        public List<Tasima> List()
        {
            return _TasimaDal.GetAllList();
        }

        public void TAdd(Tasima t)
        {
            _TasimaDal.Insert(t);
        }

        public void TDelete(Tasima t)
        {
            _TasimaDal.Delete(t);
        }

        public void TUpdate(Tasima t)
        {
            _TasimaDal.Update(t);
        }
    }
}
