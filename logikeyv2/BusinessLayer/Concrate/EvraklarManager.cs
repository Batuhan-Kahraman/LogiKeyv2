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
	public class EvraklarManager : IEvraklarService
	{
		IEvraklarDal _EvraklarDal;
		public EvraklarManager(IEvraklarDal EvraklarDal)
		{
			_EvraklarDal = EvraklarDal;
		}


		public List<Evraklar> GetAllList(Expression<Func<Evraklar, bool>> filter)
		{
			return _EvraklarDal.GetAllList(filter);
		}

		public Evraklar GetByID(int id)
		{
			return _EvraklarDal.GetByID(id);
		}

		public Evraklar GetByPropertyName(string propertyName, string value)
		{
			return _EvraklarDal.GetByPropertyName(propertyName, value);
		}

		public List<Evraklar> List()
		{
			return _EvraklarDal.GetAllList();
		}

		public void TAdd(Evraklar t)
		{
			_EvraklarDal.Insert(t);
		}

		public void TDelete(Evraklar t)
		{
			_EvraklarDal.Delete(t);
		}

		public void TUpdate(Evraklar t)
		{
			_EvraklarDal.Update(t);
		}
	}
}
