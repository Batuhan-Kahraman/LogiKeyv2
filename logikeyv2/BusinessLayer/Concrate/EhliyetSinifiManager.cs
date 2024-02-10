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
	public class EhliyetSinifiManager : IEhliyetSinifiService
	{
		IEhliyetSinifiDal _EhliyetSinifiDal;
		public EhliyetSinifiManager(IEhliyetSinifiDal EhliyetSinifiDal)
		{
			_EhliyetSinifiDal = EhliyetSinifiDal;
		}


		public List<EhliyetSinifi> GetAllList(Expression<Func<EhliyetSinifi, bool>> filter)
		{
			return _EhliyetSinifiDal.GetAllList(filter);
		}

		public EhliyetSinifi GetByID(int id)
		{
			return _EhliyetSinifiDal.GetByID(id);
		}

		public EhliyetSinifi GetByPropertyName(string propertyName, string value)
		{
			return _EhliyetSinifiDal.GetByPropertyName(propertyName, value);
		}

		public List<EhliyetSinifi> List()
		{
			return _EhliyetSinifiDal.GetAllList();
		}

		public void TAdd(EhliyetSinifi t)
		{
			_EhliyetSinifiDal.Insert(t);
		}

		public void TDelete(EhliyetSinifi t)
		{
			_EhliyetSinifiDal.Delete(t);
		}

		public void TUpdate(EhliyetSinifi t)
		{
			_EhliyetSinifiDal.Update(t);
		}
	}
}
