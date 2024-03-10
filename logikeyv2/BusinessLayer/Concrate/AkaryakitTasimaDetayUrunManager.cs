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
	public class AkaryakitTasimaDetayUrunManager : IAkaryakitTasimaDetayUrunService
	{
		IAkaryakitTasimaDetayUrunDal _AkaryakitTasimaDetayUrunDal;
		public AkaryakitTasimaDetayUrunManager(IAkaryakitTasimaDetayUrunDal AkaryakitTasimaDetayUrunDal)
		{
			_AkaryakitTasimaDetayUrunDal = AkaryakitTasimaDetayUrunDal;
		}


		public List<AkaryakitTasimaDetayUrun> GetAllList(Expression<Func<AkaryakitTasimaDetayUrun, bool>> filter)
		{
			return _AkaryakitTasimaDetayUrunDal.GetAllList(filter);
		}

		public AkaryakitTasimaDetayUrun GetByID(int id)
		{
			return _AkaryakitTasimaDetayUrunDal.GetByID(id);
		}

		public AkaryakitTasimaDetayUrun GetByPropertyName(string propertyName, string value)
		{
			return _AkaryakitTasimaDetayUrunDal.GetByPropertyName(propertyName, value);
		}

		public List<AkaryakitTasimaDetayUrun> List()
		{
			return _AkaryakitTasimaDetayUrunDal.GetAllList();
		}

		public void TAdd(AkaryakitTasimaDetayUrun t)
		{
			_AkaryakitTasimaDetayUrunDal.Insert(t);
		}

		public void TDelete(AkaryakitTasimaDetayUrun t)
		{
			_AkaryakitTasimaDetayUrunDal.Delete(t);
		}

		public void TUpdate(AkaryakitTasimaDetayUrun t)
		{
			_AkaryakitTasimaDetayUrunDal.Update(t);
		}
	}
}
