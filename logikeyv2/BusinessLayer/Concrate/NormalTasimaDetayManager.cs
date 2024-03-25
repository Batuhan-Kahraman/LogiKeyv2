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
	public class NormalTasimaDetayManager : INormalTasimaDetayService
	{
		INormalTasimaDetayDal _NormalTasimaDetayDal;
		public NormalTasimaDetayManager(INormalTasimaDetayDal NormalTasimaDetayDal)
		{
			_NormalTasimaDetayDal = NormalTasimaDetayDal;
		}


		public List<NormalTasimaDetay> GetAllList(Expression<Func<NormalTasimaDetay, bool>> filter)
		{
			return _NormalTasimaDetayDal.GetAllList(filter);
		}

		public NormalTasimaDetay GetByID(int id)
		{
			return _NormalTasimaDetayDal.GetByID(id);
		}

		public NormalTasimaDetay GetByPropertyName(string propertyName, string value)
		{
			return _NormalTasimaDetayDal.GetByPropertyName(propertyName, value);
		}

		public List<NormalTasimaDetay> List()
		{
			return _NormalTasimaDetayDal.GetAllList();
		}

		public void TAdd(NormalTasimaDetay t)
		{
			_NormalTasimaDetayDal.Insert(t);
		}

		public void TDelete(NormalTasimaDetay t)
		{
			_NormalTasimaDetayDal.Delete(t);
		}

		public void TUpdate(NormalTasimaDetay t)
		{
			_NormalTasimaDetayDal.Update(t);
		}
	}
}
