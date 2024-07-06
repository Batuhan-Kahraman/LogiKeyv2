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
	public class YurtDisiEvraklarManager : IYurtDisiEvraklarService
	{
		IYurtDisiEvraklarDal _YurtDisiEvraklarDal;
		public YurtDisiEvraklarManager(IYurtDisiEvraklarDal YurtDisiEvraklarDal)
		{
			_YurtDisiEvraklarDal = YurtDisiEvraklarDal;
		}


		public List<YurtDisiEvraklar> GetAllList(Expression<Func<YurtDisiEvraklar, bool>> filter)
		{
			return _YurtDisiEvraklarDal.GetAllList(filter);
		}

		public YurtDisiEvraklar GetByID(int id)
		{
			return _YurtDisiEvraklarDal.GetByID(id);
		}

		public YurtDisiEvraklar GetByPropertyName(string propertyName, string value)
		{
			return _YurtDisiEvraklarDal.GetByPropertyName(propertyName, value);
		}

		public List<YurtDisiEvraklar> List()
		{
			return _YurtDisiEvraklarDal.GetAllList();
		}

		public void TAdd(YurtDisiEvraklar t)
		{
			_YurtDisiEvraklarDal.Insert(t);
		}

		public void TDelete(YurtDisiEvraklar t)
		{
			_YurtDisiEvraklarDal.Delete(t);
		}

		public void TUpdate(YurtDisiEvraklar t)
		{
			_YurtDisiEvraklarDal.Update(t);
		}
	}
}
