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
	public class AracResimlerManager : IAracResimlerService
	{
		IAracResimlerDal _AracResimlerDal;
		public AracResimlerManager(IAracResimlerDal AracResimlerDal)
		{
			_AracResimlerDal = AracResimlerDal;
		}


		public List<AracResimler> GetAllList(Expression<Func<AracResimler, bool>> filter)
		{
			return _AracResimlerDal.GetAllList(filter);
		}

		public AracResimler GetByID(int id)
		{
			return _AracResimlerDal.GetByID(id);
		}

		public AracResimler GetByPropertyName(string propertyName, string value)
		{
			return _AracResimlerDal.GetByPropertyName(propertyName, value);
		}

		public List<AracResimler> List()
		{
			return _AracResimlerDal.GetAllList();
		}

		public void TAdd(AracResimler t)
		{
			_AracResimlerDal.Insert(t);
		}

		public void TDelete(AracResimler t)
		{
			_AracResimlerDal.Delete(t);
		}

		public void TUpdate(AracResimler t)
		{
			_AracResimlerDal.Update(t);
		}
	}
}
