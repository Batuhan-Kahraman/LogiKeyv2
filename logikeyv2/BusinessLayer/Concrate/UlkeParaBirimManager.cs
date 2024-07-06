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
	public class UlkeParaBirimManager : IUlkeParaBirimService
	{
		IUlkeParaBirimDal _UlkeParaBirimDal;
		public UlkeParaBirimManager(IUlkeParaBirimDal UlkeParaBirimDal)
		{
			_UlkeParaBirimDal = UlkeParaBirimDal;
		}


		public List<UlkeParaBirim> GetAllList(Expression<Func<UlkeParaBirim, bool>> filter)
		{
			return _UlkeParaBirimDal.GetAllList(filter);
		}

		public UlkeParaBirim GetByID(int id)
		{
			return _UlkeParaBirimDal.GetByID(id);
		}

		public UlkeParaBirim GetByPropertyName(string propertyName, string value)
		{
			return _UlkeParaBirimDal.GetByPropertyName(propertyName, value);
		}

		public List<UlkeParaBirim> List()
		{
			return _UlkeParaBirimDal.GetAllList();
		}

		public void TAdd(UlkeParaBirim t)
		{
			_UlkeParaBirimDal.Insert(t);
		}

		public void TDelete(UlkeParaBirim t)
		{
			_UlkeParaBirimDal.Delete(t);
		}

		public void TUpdate(UlkeParaBirim t)
		{
			_UlkeParaBirimDal.Update(t);
		}
	}
}
