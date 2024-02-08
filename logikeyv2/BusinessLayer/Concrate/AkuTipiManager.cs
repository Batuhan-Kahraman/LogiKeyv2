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
	public class AkuTipiManager : IAkuTipiService
	{
		IAkuTipiDal _AkuTipiDal;
		public AkuTipiManager(IAkuTipiDal AkuTipiDal)
		{
			_AkuTipiDal = AkuTipiDal;
		}


		public List<AkuTipi> GetAllList(Expression<Func<AkuTipi, bool>> filter)
		{
			return _AkuTipiDal.GetAllList(filter);
		}

		public AkuTipi GetByID(int id)
		{
			return _AkuTipiDal.GetByID(id);
		}

		public AkuTipi GetByPropertyName(string propertyName, string value)
		{
			return _AkuTipiDal.GetByPropertyName(propertyName, value);
		}

		public List<AkuTipi> List()
		{
			return _AkuTipiDal.GetAllList();
		}

		public void TAdd(AkuTipi t)
		{
			_AkuTipiDal.Insert(t);
		}

		public void TDelete(AkuTipi t)
		{
			_AkuTipiDal.Delete(t);
		}

		public void TUpdate(AkuTipi t)
		{
			_AkuTipiDal.Update(t);
		}
	}
}
