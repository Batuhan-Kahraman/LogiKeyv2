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
	public class StokManager : IStokService
	{
		IStokDal _StokDal;
		public StokManager(IStokDal StokDal)
		{
			_StokDal = StokDal;
		}


		public List<Stok> GetAllList(Expression<Func<Stok, bool>> filter)
		{
			return _StokDal.GetAllList(filter);
		}

		public Stok GetByID(int id)
		{
			return _StokDal.GetByID(id);
		}

		public Stok GetByPropertyName(string propertyName, string value)
		{
			return _StokDal.GetByPropertyName(propertyName, value);
		}

		public List<Stok> List()
		{
			return _StokDal.GetAllList();
		}

		public void TAdd(Stok t)
		{
			_StokDal.Insert(t);
		}

		public void TDelete(Stok t)
		{
			_StokDal.Delete(t);
		}

		public void TUpdate(Stok t)
		{
			_StokDal.Update(t);
		}
	}
}
