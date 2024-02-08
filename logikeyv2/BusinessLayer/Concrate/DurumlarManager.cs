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
	public class DurumlarManager : IDurumlarService
	{
		IDurumlarDal _DurumlarDal;
		public DurumlarManager(IDurumlarDal DurumlarDal)
		{
			_DurumlarDal = DurumlarDal;
		}


		public List<Durumlar> GetAllList(Expression<Func<Durumlar, bool>> filter)
		{
			return _DurumlarDal.GetAllList(filter);
		}

		public Durumlar GetByID(int id)
		{
			return _DurumlarDal.GetByID(id);
		}

		public Durumlar GetByPropertyName(string propertyName, string value)
		{
			return _DurumlarDal.GetByPropertyName(propertyName, value);
		}

		public List<Durumlar> List()
		{
			return _DurumlarDal.GetAllList();
		}

		public void TAdd(Durumlar t)
		{
            _DurumlarDal.Insert(t);
		}

		public void TDelete(Durumlar t)
		{
            _DurumlarDal.Delete(t);
		}

		public void TUpdate(Durumlar t)
		{
            _DurumlarDal.Update(t);
		}
	}
}
