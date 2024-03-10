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
	public class AkaryakitTasimaManager : IAkaryakitTasimaService
	{
		IAkaryakitTasimaDal _AkaryakitTasimaDal;
		public AkaryakitTasimaManager(IAkaryakitTasimaDal AkaryakitTasimaDal)
		{
			_AkaryakitTasimaDal = AkaryakitTasimaDal;
		}


		public List<AkaryakitTasima> GetAllList(Expression<Func<AkaryakitTasima, bool>> filter)
		{
			return _AkaryakitTasimaDal.GetAllList(filter);
		}

		public AkaryakitTasima GetByID(int id)
		{
			return _AkaryakitTasimaDal.GetByID(id);
		}

		public AkaryakitTasima GetByPropertyName(string propertyName, string value)
		{
			return _AkaryakitTasimaDal.GetByPropertyName(propertyName, value);
		}

		public List<AkaryakitTasima> List()
		{
			return _AkaryakitTasimaDal.GetAllList();
		}

		public void TAdd(AkaryakitTasima t)
		{
			_AkaryakitTasimaDal.Insert(t);
		}

		public void TDelete(AkaryakitTasima t)
		{
			_AkaryakitTasimaDal.Delete(t);
		}

		public void TUpdate(AkaryakitTasima t)
		{
			_AkaryakitTasimaDal.Update(t);
		}
	}
}
