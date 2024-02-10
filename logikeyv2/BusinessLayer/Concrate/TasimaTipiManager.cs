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
	public class TasimaTipiManager : ITasimaTipiService
	{
		ITasimaTipiDal _TasimaTipiDal;
		public TasimaTipiManager(ITasimaTipiDal TasimaTipiDal)
		{
			_TasimaTipiDal = TasimaTipiDal;
		}


		public List<TasimaTipi> GetAllList(Expression<Func<TasimaTipi, bool>> filter)
		{
			return _TasimaTipiDal.GetAllList(filter);
		}

		public TasimaTipi GetByID(int id)
		{
			return _TasimaTipiDal.GetByID(id);
		}

		public TasimaTipi GetByPropertyName(string propertyName, string value)
		{
			return _TasimaTipiDal.GetByPropertyName(propertyName, value);
		}

		public List<TasimaTipi> List()
		{
			return _TasimaTipiDal.GetAllList();
		}

		public void TAdd(TasimaTipi t)
		{
			_TasimaTipiDal.Insert(t);
		}

		public void TDelete(TasimaTipi t)
		{
			_TasimaTipiDal.Delete(t);
		}

		public void TUpdate(TasimaTipi t)
		{
			_TasimaTipiDal.Update(t);
		}
	}
}
