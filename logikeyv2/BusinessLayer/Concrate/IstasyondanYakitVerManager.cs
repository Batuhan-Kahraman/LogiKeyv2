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
	public class IstasyondanYakitVerManager : IIstasyondanYakitVerService
	{
		IIstasyondanYakitVerDal _IstasyondanYakitVerDal;
		public IstasyondanYakitVerManager(IIstasyondanYakitVerDal IstasyondanYakitVerDal)
		{
			_IstasyondanYakitVerDal = IstasyondanYakitVerDal;
		}


		public List<IstasyondanYakitVer> GetAllList(Expression<Func<IstasyondanYakitVer, bool>> filter)
		{
			return _IstasyondanYakitVerDal.GetAllList(filter);
		}

		public IstasyondanYakitVer GetByID(int id)
		{
			return _IstasyondanYakitVerDal.GetByID(id);
		}

		public IstasyondanYakitVer GetByPropertyName(string propertyName, string value)
		{
			return _IstasyondanYakitVerDal.GetByPropertyName(propertyName, value);
		}

		public List<IstasyondanYakitVer> List()
		{
			return _IstasyondanYakitVerDal.GetAllList();
		}

		public void TAdd(IstasyondanYakitVer t)
		{
			_IstasyondanYakitVerDal.Insert(t);
		}

		public void TDelete(IstasyondanYakitVer t)
		{
			_IstasyondanYakitVerDal.Delete(t);
		}

		public void TUpdate(IstasyondanYakitVer t)
		{
			_IstasyondanYakitVerDal.Update(t);
		}
	}
}
