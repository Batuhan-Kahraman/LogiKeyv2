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
	public class KazaManager : IKazaService
	{
		IKazaDal _KazaDal;
		public KazaManager(IKazaDal KazaDal)
		{
			_KazaDal = KazaDal;
		}


		public List<Kaza> GetAllList(Expression<Func<Kaza, bool>> filter)
		{
			return _KazaDal.GetAllList(filter);
		}

		public Kaza GetByID(int id)
		{
			return _KazaDal.GetByID(id);
		}

		public Kaza GetByPropertyName(string propertyName, string value)
		{
			return _KazaDal.GetByPropertyName(propertyName, value);
		}

		public List<Kaza> List()
		{
			return _KazaDal.GetAllList();
		}

		public void TAdd(Kaza t)
		{
			_KazaDal.Insert(t);
		}

		public void TDelete(Kaza t)
		{
			_KazaDal.Delete(t);
		}

		public void TUpdate(Kaza t)
		{
			_KazaDal.Update(t);
		}
	}
}
