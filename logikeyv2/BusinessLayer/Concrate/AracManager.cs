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
	public class AracManager : IAracService
	{
		IAracDal _AracDal;
		public AracManager(IAracDal AracDal)
		{
			_AracDal = AracDal;
		}


		public List<Arac> GetAllList(Expression<Func<Arac, bool>> filter)
		{
			return _AracDal.GetAllList(filter);
		}

		public Arac GetByID(int id)
		{
			return _AracDal.GetByID(id);
		}

		public Arac GetByPropertyName(string propertyName, string value)
		{
			return _AracDal.GetByPropertyName(propertyName, value);
		}

		public List<Arac> List()
		{
			return _AracDal.GetAllList();
		}

		public void TAdd(Arac t)
		{
			_AracDal.Insert(t);
		}

		public void TDelete(Arac t)
		{
			_AracDal.Delete(t);
		}

		public void TUpdate(Arac t)
		{
			_AracDal.Update(t);
		}
	}
}
