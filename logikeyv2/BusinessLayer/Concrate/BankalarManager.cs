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
	public class BankalarManager : IBankalarService
	{
		IBankalarDal _BankalarDal;
		public BankalarManager(IBankalarDal BankalarDal)
		{
			_BankalarDal = BankalarDal;
		}


		public List<Bankalar> GetAllList(Expression<Func<Bankalar, bool>> filter)
		{
			return _BankalarDal.GetAllList(filter);
		}

		public Bankalar GetByID(int id)
		{
			return _BankalarDal.GetByID(id);
		}

		public Bankalar GetByPropertyName(string propertyName, string value)
		{
			return _BankalarDal.GetByPropertyName(propertyName, value);
		}

		public List<Bankalar> List()
		{
			return _BankalarDal.GetAllList();
		}

		public void TAdd(Bankalar t)
		{
			_BankalarDal.Insert(t);
		}

		public void TDelete(Bankalar t)
		{
			_BankalarDal.Delete(t);
		}

		public void TUpdate(Bankalar t)
		{
			_BankalarDal.Update(t);
		}
	}
}
