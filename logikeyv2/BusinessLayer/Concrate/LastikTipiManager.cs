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
	public class LastikTipiManager : ILastikTipiService
	{
		ILastikTipiDal _LastikTipiDal;
		public LastikTipiManager(ILastikTipiDal LastikTipiDal)
		{
			_LastikTipiDal = LastikTipiDal;
		}


		public List<LastikTipi> GetAllList(Expression<Func<LastikTipi, bool>> filter)
		{
			return _LastikTipiDal.GetAllList(filter);
		}

		public LastikTipi GetByID(int id)
		{
			return _LastikTipiDal.GetByID(id);
		}

		public LastikTipi GetByPropertyName(string propertyName, string value)
		{
			return _LastikTipiDal.GetByPropertyName(propertyName, value);
		}

		public List<LastikTipi> List()
		{
			return _LastikTipiDal.GetAllList();
		}

		public void TAdd(LastikTipi t)
		{
			_LastikTipiDal.Insert(t);
		}

		public void TDelete(LastikTipi t)
		{
			_LastikTipiDal.Delete(t);
		}

		public void TUpdate(LastikTipi t)
		{
			_LastikTipiDal.Update(t);
		}
	}
}
