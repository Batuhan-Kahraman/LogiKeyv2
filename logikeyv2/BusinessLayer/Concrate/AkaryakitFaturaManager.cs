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
	public class AkaryakitFaturaManager : IAkaryakitFaturaService
	{
		IAkaryakitFaturaDal _AkaryakitFaturaDal;
		public AkaryakitFaturaManager(IAkaryakitFaturaDal AkaryakitFaturaDal)
		{
			_AkaryakitFaturaDal = AkaryakitFaturaDal;
		}


		public List<AkaryakitFatura> GetAllList(Expression<Func<AkaryakitFatura, bool>> filter)
		{
			return _AkaryakitFaturaDal.GetAllList(filter);
		}

		public AkaryakitFatura GetByID(int id)
		{
			return _AkaryakitFaturaDal.GetByID(id);
		}

		public AkaryakitFatura GetByPropertyName(string propertyName, string value)
		{
			return _AkaryakitFaturaDal.GetByPropertyName(propertyName, value);
		}

		public List<AkaryakitFatura> List()
		{
			return _AkaryakitFaturaDal.GetAllList();
		}

		public void TAdd(AkaryakitFatura t)
		{
			_AkaryakitFaturaDal.Insert(t);
		}

		public void TDelete(AkaryakitFatura t)
		{
			_AkaryakitFaturaDal.Delete(t);
		}

		public void TUpdate(AkaryakitFatura t)
		{
			_AkaryakitFaturaDal.Update(t);
		}
	}
}
