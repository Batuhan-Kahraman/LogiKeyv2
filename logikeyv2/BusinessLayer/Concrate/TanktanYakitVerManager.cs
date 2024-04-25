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
	public class TanktanYakitVerManager : ITanktanYakitVerService
	{
		ITanktanYakitVerDal _TanktanYakitVerDal;
		public TanktanYakitVerManager(ITanktanYakitVerDal TanktanYakitVerDal)
		{
			_TanktanYakitVerDal = TanktanYakitVerDal;
		}


		public List<TanktanYakitVer> GetAllList(Expression<Func<TanktanYakitVer, bool>> filter)
		{
			return _TanktanYakitVerDal.GetAllList(filter);
		}

		public TanktanYakitVer GetByID(int id)
		{
			return _TanktanYakitVerDal.GetByID(id);
		}

		public TanktanYakitVer GetByPropertyName(string propertyName, string value)
		{
			return _TanktanYakitVerDal.GetByPropertyName(propertyName, value);
		}

		public List<TanktanYakitVer> List()
		{
			return _TanktanYakitVerDal.GetAllList();
		}

		public void TAdd(TanktanYakitVer t)
		{
			_TanktanYakitVerDal.Insert(t);
		}

		public void TDelete(TanktanYakitVer t)
		{
			_TanktanYakitVerDal.Delete(t);
		}

		public void TUpdate(TanktanYakitVer t)
		{
			_TanktanYakitVerDal.Update(t);
		}
	}
}
