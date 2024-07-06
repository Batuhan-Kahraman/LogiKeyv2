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
	public class YurtDisiAracTurManager : IYurtDisiAracTurService
	{
		IYurtDisiAracTurDal _YurtDisiAracTurDal;
		public YurtDisiAracTurManager(IYurtDisiAracTurDal YurtDisiAracTurDal)
		{
			_YurtDisiAracTurDal = YurtDisiAracTurDal;
		}


		public List<YurtDisiAracTur> GetAllList(Expression<Func<YurtDisiAracTur, bool>> filter)
		{
			return _YurtDisiAracTurDal.GetAllList(filter);
		}

		public YurtDisiAracTur GetByID(int id)
		{
			return _YurtDisiAracTurDal.GetByID(id);
		}

		public YurtDisiAracTur GetByPropertyName(string propertyName, string value)
		{
			return _YurtDisiAracTurDal.GetByPropertyName(propertyName, value);
		}

		public List<YurtDisiAracTur> List()
		{
			return _YurtDisiAracTurDal.GetAllList();
		}

		public void TAdd(YurtDisiAracTur t)
		{
			_YurtDisiAracTurDal.Insert(t);
		}

		public void TDelete(YurtDisiAracTur t)
		{
			_YurtDisiAracTurDal.Delete(t);
		}

		public void TUpdate(YurtDisiAracTur t)
		{
			_YurtDisiAracTurDal.Update(t);
		}
	}
}
