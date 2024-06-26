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
	public class YurtDisiMasraflarManager : IYurtDisiMasraflarService
	{
		IYurtDisiMasraflarDal _YurtDisiMasraflarDal;
		public YurtDisiMasraflarManager(IYurtDisiMasraflarDal YurtDisiMasraflarDal)
		{
			_YurtDisiMasraflarDal = YurtDisiMasraflarDal;
		}


		public List<YurtDisiMasraflar> GetAllList(Expression<Func<YurtDisiMasraflar, bool>> filter)
		{
			return _YurtDisiMasraflarDal.GetAllList(filter);
		}

		public YurtDisiMasraflar GetByID(int id)
		{
			return _YurtDisiMasraflarDal.GetByID(id);
		}

		public YurtDisiMasraflar GetByPropertyName(string propertyName, string value)
		{
			return _YurtDisiMasraflarDal.GetByPropertyName(propertyName, value);
		}

		public List<YurtDisiMasraflar> List()
		{
			return _YurtDisiMasraflarDal.GetAllList();
		}

		public void TAdd(YurtDisiMasraflar t)
		{
			_YurtDisiMasraflarDal.Insert(t);
		}

		public void TDelete(YurtDisiMasraflar t)
		{
			_YurtDisiMasraflarDal.Delete(t);
		}

		public void TUpdate(YurtDisiMasraflar t)
		{
			_YurtDisiMasraflarDal.Update(t);
		}
	}
}
