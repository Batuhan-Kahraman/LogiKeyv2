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
	public class BildirimManager : IBildirimService
	{
		IBildirimDal _BildirimDal;
		public BildirimManager(IBildirimDal BildirimDal)
		{
			_BildirimDal = BildirimDal;
		}


		public List<Bildirim> GetAllList(Expression<Func<Bildirim, bool>> filter)
		{
			return _BildirimDal.GetAllList(filter);
		}

		public Bildirim GetByID(int id)
		{
			return _BildirimDal.GetByID(id);
		}

		public Bildirim GetByPropertyName(string propertyName, string value)
		{
			return _BildirimDal.GetByPropertyName(propertyName, value);
		}

		public List<Bildirim> List()
		{
			return _BildirimDal.GetAllList();
		}

		public void TAdd(Bildirim t)
		{
			_BildirimDal.Insert(t);
		}

		public void TDelete(Bildirim t)
		{
			_BildirimDal.Delete(t);
		}

		public void TUpdate(Bildirim t)
		{
			_BildirimDal.Update(t);
		}
	}
}
