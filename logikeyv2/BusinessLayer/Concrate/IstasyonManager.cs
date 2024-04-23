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
	public class IstasyonManager : IIstasyonService
	{
		IIstasyonDal _IstasyonDal;
		public IstasyonManager(IIstasyonDal IstasyonDal)
		{
			_IstasyonDal = IstasyonDal;
		}


		public List<Istasyon> GetAllList(Expression<Func<Istasyon, bool>> filter)
		{
			return _IstasyonDal.GetAllList(filter);
		}

		public Istasyon GetByID(int id)
		{
			return _IstasyonDal.GetByID(id);
		}

		public Istasyon GetByPropertyName(string propertyName, string value)
		{
			return _IstasyonDal.GetByPropertyName(propertyName, value);
		}

		public List<Istasyon> List()
		{
			return _IstasyonDal.GetAllList();
		}

		public void TAdd(Istasyon t)
		{
			_IstasyonDal.Insert(t);
		}

		public void TDelete(Istasyon t)
		{
			_IstasyonDal.Delete(t);
		}

		public void TUpdate(Istasyon t)
		{
			_IstasyonDal.Update(t);
		}
	}
}
