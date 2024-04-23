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
	public class BakimStokManager : IBakimStokService
	{
		IBakimStokDal _BakimStokDal;
		public BakimStokManager(IBakimStokDal BakimStokDal)
		{
			_BakimStokDal = BakimStokDal;
		}


		public List<BakimStok> GetAllList(Expression<Func<BakimStok, bool>> filter)
		{
			return _BakimStokDal.GetAllList(filter);
		}

		public BakimStok GetByID(int id)
		{
			return _BakimStokDal.GetByID(id);
		}

		public BakimStok GetByPropertyName(string propertyName, string value)
		{
			return _BakimStokDal.GetByPropertyName(propertyName, value);
		}

		public List<BakimStok> List()
		{
			return _BakimStokDal.GetAllList();
		}

		public void TAdd(BakimStok t)
		{
			_BakimStokDal.Insert(t);
		}

		public void TDelete(BakimStok t)
		{
			_BakimStokDal.Delete(t);
		}

		public void TUpdate(BakimStok t)
		{
			_BakimStokDal.Update(t);
		}
	}
}
