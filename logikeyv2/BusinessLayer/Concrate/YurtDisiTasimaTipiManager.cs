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
	public class YurtDisiTasimaTipiManager : IYurtDisiTasimaTipiService
	{
		IYurtDisiTasimaTipiDal _YurtDisiTasimaTipiDal;
		public YurtDisiTasimaTipiManager(IYurtDisiTasimaTipiDal YurtDisiTasimaTipiDal)
		{
			_YurtDisiTasimaTipiDal = YurtDisiTasimaTipiDal;
		}


		public List<YurtDisiTasimaTipi> GetAllList(Expression<Func<YurtDisiTasimaTipi, bool>> filter)
		{
			return _YurtDisiTasimaTipiDal.GetAllList(filter);
		}

		public YurtDisiTasimaTipi GetByID(int id)
		{
			return _YurtDisiTasimaTipiDal.GetByID(id);
		}

		public YurtDisiTasimaTipi GetByPropertyName(string propertyName, string value)
		{
			return _YurtDisiTasimaTipiDal.GetByPropertyName(propertyName, value);
		}

		public List<YurtDisiTasimaTipi> List()
		{
			return _YurtDisiTasimaTipiDal.GetAllList();
		}

		public void TAdd(YurtDisiTasimaTipi t)
		{
			_YurtDisiTasimaTipiDal.Insert(t);
		}

		public void TDelete(YurtDisiTasimaTipi t)
		{
			_YurtDisiTasimaTipiDal.Delete(t);
		}

		public void TUpdate(YurtDisiTasimaTipi t)
		{
			_YurtDisiTasimaTipiDal.Update(t);
		}
	}
}
