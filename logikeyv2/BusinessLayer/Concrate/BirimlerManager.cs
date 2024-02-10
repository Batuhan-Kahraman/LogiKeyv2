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
	public class BirimlerManager : IBirimlerService
	{
		IBirimlerDal _BirimlerDal;
		public BirimlerManager(IBirimlerDal BirimlerDal)
		{
			_BirimlerDal = BirimlerDal;
		}


		public List<Birimler> GetAllList(Expression<Func<Birimler, bool>> filter)
		{
			return _BirimlerDal.GetAllList(filter);
		}

		public Birimler GetByID(int id)
		{
			return _BirimlerDal.GetByID(id);
		}

		public Birimler GetByPropertyName(string propertyName, string value)
		{
			return _BirimlerDal.GetByPropertyName(propertyName, value);
		}

		public List<Birimler> List()
		{
			return _BirimlerDal.GetAllList();
		}

		public void TAdd(Birimler t)
		{
			_BirimlerDal.Insert(t);
		}

		public void TDelete(Birimler t)
		{
			_BirimlerDal.Delete(t);
		}

		public void TUpdate(Birimler t)
		{
			_BirimlerDal.Update(t);
		}
	}
}
