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
	public class AracTurManager : IAracTurService
	{
		IAracTurDal _AracTurDal;
		public AracTurManager(IAracTurDal AracTurDal)
		{
			_AracTurDal = AracTurDal;
		}


		public List<AracTur> GetAllList(Expression<Func<AracTur, bool>> filter)
		{
			return _AracTurDal.GetAllList(filter);
		}

		public AracTur GetByID(int id)
		{
			return _AracTurDal.GetByID(id);
		}

		public AracTur GetByPropertyName(string propertyName, string value)
		{
			return _AracTurDal.GetByPropertyName(propertyName, value);
		}

		public List<AracTur> List()
		{
			return _AracTurDal.GetAllList();
		}

		public void TAdd(AracTur t)
		{
			_AracTurDal.Insert(t);
		}

		public void TDelete(AracTur t)
		{
			_AracTurDal.Delete(t);
		}

		public void TUpdate(AracTur t)
		{
			_AracTurDal.Update(t);
		}
	}
}
