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
	public class OkulAracTurManager : IOkulAracTurService
	{
		IOkulAracTurDal _OkulAracTurDal;
		public OkulAracTurManager(IOkulAracTurDal OkulAracTurDal)
		{
			_OkulAracTurDal = OkulAracTurDal;
		}


		public List<OkulAracTur> GetAllList(Expression<Func<OkulAracTur, bool>> filter)
		{
			return _OkulAracTurDal.GetAllList(filter);
		}

		public OkulAracTur GetByID(int id)
		{
			return _OkulAracTurDal.GetByID(id);
		}

		public OkulAracTur GetByPropertyName(string propertyName, string value)
		{
			return _OkulAracTurDal.GetByPropertyName(propertyName, value);
		}

		public List<OkulAracTur> List()
		{
			return _OkulAracTurDal.GetAllList();
		}

		public void TAdd(OkulAracTur t)
		{
			_OkulAracTurDal.Insert(t);
		}

		public void TDelete(OkulAracTur t)
		{
			_OkulAracTurDal.Delete(t);
		}

		public void TUpdate(OkulAracTur t)
		{
			_OkulAracTurDal.Update(t);
		}
	}
}
