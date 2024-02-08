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
	public class MasrafTipiManager : IMasrafTipiService
	{
		IMasrafTipiDal _MasrafTipiDal;
		public MasrafTipiManager(IMasrafTipiDal MasrafTipiDal)
		{
			_MasrafTipiDal = MasrafTipiDal;
		}


		public List<MasrafTipi> GetAllList(Expression<Func<MasrafTipi, bool>> filter)
		{
			return _MasrafTipiDal.GetAllList(filter);
		}

		public MasrafTipi GetByID(int id)
		{
			return _MasrafTipiDal.GetByID(id);
		}

		public MasrafTipi GetByPropertyName(string propertyName, string value)
		{
			return _MasrafTipiDal.GetByPropertyName(propertyName, value);
		}

		public List<MasrafTipi> List()
		{
			return _MasrafTipiDal.GetAllList();
		}

		public void TAdd(MasrafTipi t)
		{
			_MasrafTipiDal.Insert(t);
		}

		public void TDelete(MasrafTipi t)
		{
			_MasrafTipiDal.Delete(t);
		}

		public void TUpdate(MasrafTipi t)
		{
			_MasrafTipiDal.Update(t);
		}
	}
}
