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
	public class AkaryakitAracTurManager : IAkaryakitAracTurService
	{
		IAkaryakitAracTurDal _AkaryakitAracTurDal;
		public AkaryakitAracTurManager(IAkaryakitAracTurDal AkaryakitAracTurDal)
		{
			_AkaryakitAracTurDal = AkaryakitAracTurDal;
		}


		public List<AkaryakitAracTur> GetAllList(Expression<Func<AkaryakitAracTur, bool>> filter)
		{
			return _AkaryakitAracTurDal.GetAllList(filter);
		}

		public AkaryakitAracTur GetByID(int id)
		{
			return _AkaryakitAracTurDal.GetByID(id);
		}

		public AkaryakitAracTur GetByPropertyName(string propertyName, string value)
		{
			return _AkaryakitAracTurDal.GetByPropertyName(propertyName, value);
		}

		public List<AkaryakitAracTur> List()
		{
			return _AkaryakitAracTurDal.GetAllList();
		}

		public void TAdd(AkaryakitAracTur t)
		{
			_AkaryakitAracTurDal.Insert(t);
		}

		public void TDelete(AkaryakitAracTur t)
		{
			_AkaryakitAracTurDal.Delete(t);
		}

		public void TUpdate(AkaryakitAracTur t)
		{
			_AkaryakitAracTurDal.Update(t);
		}
	}
}
