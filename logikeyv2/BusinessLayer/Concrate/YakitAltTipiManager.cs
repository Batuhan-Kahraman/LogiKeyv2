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
	public class YakitAltTipiManager : IYakitAltTipiService
	{
		IYakitAltTipiDal _YakitAltTipiDal;
		public YakitAltTipiManager(IYakitAltTipiDal YakitAltTipiDal)
		{
			_YakitAltTipiDal = YakitAltTipiDal;
		}


		public List<YakitAltTipi> GetAllList(Expression<Func<YakitAltTipi, bool>> filter)
		{
			return _YakitAltTipiDal.GetAllList(filter);
		}

		public YakitAltTipi GetByID(int id)
		{
			return _YakitAltTipiDal.GetByID(id);
		}

		public YakitAltTipi GetByPropertyName(string propertyName, string value)
		{
			return _YakitAltTipiDal.GetByPropertyName(propertyName, value);
		}

		public List<YakitAltTipi> List()
		{
			return _YakitAltTipiDal.GetAllList();
		}

		public void TAdd(YakitAltTipi t)
		{
			_YakitAltTipiDal.Insert(t);
		}

		public void TDelete(YakitAltTipi t)
		{
			_YakitAltTipiDal.Delete(t);
		}

		public void TUpdate(YakitAltTipi t)
		{
			_YakitAltTipiDal.Update(t);
		}
	}
}
