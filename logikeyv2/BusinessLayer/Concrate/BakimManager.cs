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
	public class BakimManager : IBakimService
	{
		IBakimDal _BakimDal;
		public BakimManager(IBakimDal BakimDal)
		{
			_BakimDal = BakimDal;
		}


		public List<Bakim> GetAllList(Expression<Func<Bakim, bool>> filter)
		{
			return _BakimDal.GetAllList(filter);
		}

		public Bakim GetByID(int id)
		{
			return _BakimDal.GetByID(id);
		}

		public Bakim GetByPropertyName(string propertyName, string value)
		{
			return _BakimDal.GetByPropertyName(propertyName, value);
		}

		public List<Bakim> List()
		{
			return _BakimDal.GetAllList();
		}

		public void TAdd(Bakim t)
		{
			_BakimDal.Insert(t);
		}

		public void TDelete(Bakim t)
		{
			_BakimDal.Delete(t);
		}

		public void TUpdate(Bakim t)
		{
			_BakimDal.Update(t);
		}
	}
}
