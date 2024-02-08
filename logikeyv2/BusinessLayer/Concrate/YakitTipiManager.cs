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
	public class YakitTipiManager : IYakitTipiService
	{
		IYakitTipiDal _YakitTipiDal;
		public YakitTipiManager(IYakitTipiDal YakitTipiDal)
		{
			_YakitTipiDal = YakitTipiDal;
		}


		public List<YakitTipi> GetAllList(Expression<Func<YakitTipi, bool>> filter)
		{
			return _YakitTipiDal.GetAllList(filter);
		}

		public YakitTipi GetByID(int id)
		{
			return _YakitTipiDal.GetByID(id);
		}

		public YakitTipi GetByPropertyName(string propertyName, string value)
		{
			return _YakitTipiDal.GetByPropertyName(propertyName, value);
		}

		public List<YakitTipi> List()
		{
			return _YakitTipiDal.GetAllList();
		}

		public void TAdd(YakitTipi t)
		{
			_YakitTipiDal.Insert(t);
		}

		public void TDelete(YakitTipi t)
		{
			_YakitTipiDal.Delete(t);
		}

		public void TUpdate(YakitTipi t)
		{
			_YakitTipiDal.Update(t);
		}
	}
}
