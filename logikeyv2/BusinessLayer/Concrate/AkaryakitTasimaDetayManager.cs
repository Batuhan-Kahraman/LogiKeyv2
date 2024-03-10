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
	public class AkaryakitTasimaDetayManager : IAkaryakitTasimaDetayService
	{
		IAkaryakitTasimaDetayDal _AkaryakitTasimaDetayDal;
		public AkaryakitTasimaDetayManager(IAkaryakitTasimaDetayDal AkaryakitTasimaDetayDal)
		{
			_AkaryakitTasimaDetayDal = AkaryakitTasimaDetayDal;
		}


		public List<AkaryakitTasimaDetay> GetAllList(Expression<Func<AkaryakitTasimaDetay, bool>> filter)
		{
			return _AkaryakitTasimaDetayDal.GetAllList(filter);
		}

		public AkaryakitTasimaDetay GetByID(int id)
		{
			return _AkaryakitTasimaDetayDal.GetByID(id);
		}

		public AkaryakitTasimaDetay GetByPropertyName(string propertyName, string value)
		{
			return _AkaryakitTasimaDetayDal.GetByPropertyName(propertyName, value);
		}

		public List<AkaryakitTasimaDetay> List()
		{
			return _AkaryakitTasimaDetayDal.GetAllList();
		}

		public void TAdd(AkaryakitTasimaDetay t)
		{
			_AkaryakitTasimaDetayDal.Insert(t);
		}

		public void TDelete(AkaryakitTasimaDetay t)
		{
			_AkaryakitTasimaDetayDal.Delete(t);
		}

		public void TUpdate(AkaryakitTasimaDetay t)
		{
			_AkaryakitTasimaDetayDal.Update(t);
		}
	}
}
