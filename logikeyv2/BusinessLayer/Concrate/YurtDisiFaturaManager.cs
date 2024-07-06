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
	public class YurtDisiFaturaManager : IYurtDisiFaturaService
	{
		IYurtDisiFaturaDal _YurtDisiFaturaDal;
		public YurtDisiFaturaManager(IYurtDisiFaturaDal YurtDisiFaturaDal)
		{
			_YurtDisiFaturaDal = YurtDisiFaturaDal;
		}


		public List<YurtDisiFatura> GetAllList(Expression<Func<YurtDisiFatura, bool>> filter)
		{
			return _YurtDisiFaturaDal.GetAllList(filter);
		}

		public YurtDisiFatura GetByID(int id)
		{
			return _YurtDisiFaturaDal.GetByID(id);
		}

		public YurtDisiFatura GetByPropertyName(string propertyName, string value)
		{
			return _YurtDisiFaturaDal.GetByPropertyName(propertyName, value);
		}

		public List<YurtDisiFatura> List()
		{
			return _YurtDisiFaturaDal.GetAllList();
		}

		public void TAdd(YurtDisiFatura t)
		{
			_YurtDisiFaturaDal.Insert(t);
		}

		public void TDelete(YurtDisiFatura t)
		{
			_YurtDisiFaturaDal.Delete(t);
		}

		public void TUpdate(YurtDisiFatura t)
		{
			_YurtDisiFaturaDal.Update(t);
		}
	}
}
