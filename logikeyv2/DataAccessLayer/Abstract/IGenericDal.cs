using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IGenericDal<T>
    {

        void Insert(T t);
        void Delete(T t);
        void Update(T t);
        List<T> GetAllList();
        T GetByID(int id);
        T GetByPropertyName(string propertyName, string value);
        List<T> GetAllList(Expression<Func<T, bool>> filter);
    }
}
