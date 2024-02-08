using DataAccessLayer.Abstract;
using DataAccessLayer.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public  class GenericRepository<T>:IGenericDal<T> where T : class
    {
        Context context = new Context();

        public void Delete(T t)
        {
            context.Remove(t);
            context.SaveChanges();
        }

        public List<T> GetAllList()
        {
            return context.Set<T>().ToList();
        }

        public T GetByID(int id)
        {
            return context.Set<T>().Find(id);

        }

        public void Insert(T t)
        {
            context.Add(t);
            context.SaveChanges();
        }


        public void Update(T t)
        {
            //context.Update(t);
            context.Entry(t).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
        }

        public List<T> GetAllList(Expression<Func<T, bool>> filter)
        {
            return context.Set<T>().Where(filter).ToList();
        }
        public T GetByPropertyName(string propertyName, string value)
        {
            var propertyInfo = typeof(T).GetProperty(propertyName);

            if (propertyInfo != null)
            {
                return context.Set<T>()
                    .AsEnumerable() // Verileri yerel olarak değerlendirme
                    .FirstOrDefault(entity => propertyInfo.GetValue(entity, null).ToString() == value);
            }

            return null;

        }
    }
}
