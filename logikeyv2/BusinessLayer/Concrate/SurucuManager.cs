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
	public class SurucuManager : ISurucuService
	{
		ISurucuDal _SurucuDal;
		public SurucuManager(ISurucuDal SurucuDal)
		{
			_SurucuDal = SurucuDal;
		}


		public List<Surucu> GetAllList(Expression<Func<Surucu, bool>> filter)
		{
			return _SurucuDal.GetAllList(filter);
		}

		public Surucu GetByID(int id)
		{
			return _SurucuDal.GetByID(id);
		}

		public Surucu GetByPropertyName(string propertyName, string value)
		{
			return _SurucuDal.GetByPropertyName(propertyName, value);
		}

		public List<Surucu> List()
		{
			return _SurucuDal.GetAllList();
		}

		public void TAdd(Surucu t)
		{
			_SurucuDal.Insert(t);
		}

		public void TDelete(Surucu t)
		{
			_SurucuDal.Delete(t);
		}

		public void TUpdate(Surucu t)
		{
			_SurucuDal.Update(t);
		}
	}
}
