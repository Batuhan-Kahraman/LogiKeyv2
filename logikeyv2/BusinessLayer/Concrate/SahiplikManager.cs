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
	public class SahiplikManager : ISahiplikService
	{
		ISahiplikDal _SahiplikDal;
		public SahiplikManager(ISahiplikDal SahiplikDal)
		{
			_SahiplikDal = SahiplikDal;
		}


		public List<Sahiplik> GetAllList(Expression<Func<Sahiplik, bool>> filter)
		{
			return _SahiplikDal.GetAllList(filter);
		}

		public Sahiplik GetByID(int id)
		{
			return _SahiplikDal.GetByID(id);
		}

		public Sahiplik GetByPropertyName(string propertyName, string value)
		{
			return _SahiplikDal.GetByPropertyName(propertyName, value);
		}

		public List<Sahiplik> List()
		{
			return _SahiplikDal.GetAllList();
		}

		public void TAdd(Sahiplik t)
		{
			_SahiplikDal.Insert(t);
		}

		public void TDelete(Sahiplik t)
		{
			_SahiplikDal.Delete(t);
		}

		public void TUpdate(Sahiplik t)
		{
			_SahiplikDal.Update(t);
		}
	}
}
