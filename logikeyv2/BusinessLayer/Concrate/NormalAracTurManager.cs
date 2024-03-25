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
	public class NormalAracTurManager : INormalAracTurService
	{
		INormalAracTurDal _NormalAracTurDal;
		public NormalAracTurManager(INormalAracTurDal NormalAracTurDal)
		{
			_NormalAracTurDal = NormalAracTurDal;
		}


		public List<NormalAracTur> GetAllList(Expression<Func<NormalAracTur, bool>> filter)
		{
			return _NormalAracTurDal.GetAllList(filter);
		}

		public NormalAracTur GetByID(int id)
		{
			return _NormalAracTurDal.GetByID(id);
		}

		public NormalAracTur GetByPropertyName(string propertyName, string value)
		{
			return _NormalAracTurDal.GetByPropertyName(propertyName, value);
		}

		public List<NormalAracTur> List()
		{
			return _NormalAracTurDal.GetAllList();
		}

		public void TAdd(NormalAracTur t)
		{
			_NormalAracTurDal.Insert(t);
		}

		public void TDelete(NormalAracTur t)
		{
			_NormalAracTurDal.Delete(t);
		}

		public void TUpdate(NormalAracTur t)
		{
			_NormalAracTurDal.Update(t);
		}
	}
}
