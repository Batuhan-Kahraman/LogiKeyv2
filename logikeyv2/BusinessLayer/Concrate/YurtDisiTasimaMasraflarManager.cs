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
	public class YurtDisiTasimaMasraflarManager : IYurtDisiTasimaMasraflarService
	{
		IYurtDisiTasimaMasraflarDal _YurtDisiTasimaMasraflarDal;
		public YurtDisiTasimaMasraflarManager(IYurtDisiTasimaMasraflarDal YurtDisiTasimaMasraflarDal)
		{
			_YurtDisiTasimaMasraflarDal = YurtDisiTasimaMasraflarDal;
		}


		public List<YurtDisiTasimaMasraflar> GetAllList(Expression<Func<YurtDisiTasimaMasraflar, bool>> filter)
		{
			return _YurtDisiTasimaMasraflarDal.GetAllList(filter);
		}

		public YurtDisiTasimaMasraflar GetByID(int id)
		{
			return _YurtDisiTasimaMasraflarDal.GetByID(id);
		}

		public YurtDisiTasimaMasraflar GetByPropertyName(string propertyName, string value)
		{
			return _YurtDisiTasimaMasraflarDal.GetByPropertyName(propertyName, value);
		}

		public List<YurtDisiTasimaMasraflar> List()
		{
			return _YurtDisiTasimaMasraflarDal.GetAllList();
		}

		public void TAdd(YurtDisiTasimaMasraflar t)
		{
			_YurtDisiTasimaMasraflarDal.Insert(t);
		}

		public void TDelete(YurtDisiTasimaMasraflar t)
		{
			_YurtDisiTasimaMasraflarDal.Delete(t);
		}

		public void TUpdate(YurtDisiTasimaMasraflar t)
		{
			_YurtDisiTasimaMasraflarDal.Update(t);
		}
	}
}
