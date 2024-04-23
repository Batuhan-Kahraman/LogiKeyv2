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
	public class BakimYeriManager : IBakimYeriService
	{
		IBakimYeriDal _BakimYeriDal;
		public BakimYeriManager(IBakimYeriDal BakimYeriDal)
		{
			_BakimYeriDal = BakimYeriDal;
		}


		public List<BakimYeri> GetAllList(Expression<Func<BakimYeri, bool>> filter)
		{
			return _BakimYeriDal.GetAllList(filter);
		}

		public BakimYeri GetByID(int id)
		{
			return _BakimYeriDal.GetByID(id);
		}

		public BakimYeri GetByPropertyName(string propertyName, string value)
		{
			return _BakimYeriDal.GetByPropertyName(propertyName, value);
		}

		public List<BakimYeri> List()
		{
			return _BakimYeriDal.GetAllList();
		}

		public void TAdd(BakimYeri t)
		{
			_BakimYeriDal.Insert(t);
		}

		public void TDelete(BakimYeri t)
		{
			_BakimYeriDal.Delete(t);
		}

		public void TUpdate(BakimYeri t)
		{
			_BakimYeriDal.Update(t);
		}
	}
}
