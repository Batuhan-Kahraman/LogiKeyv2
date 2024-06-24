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
	public class YurtDisiTasimaDetayManager : IYurtDisiTasimaDetayService
	{
		IYurtDisiTasimaDetayDal _YurtDisiTasimaDetayDal;
		public YurtDisiTasimaDetayManager(IYurtDisiTasimaDetayDal YurtDisiTasimaDetayDal)
		{
			_YurtDisiTasimaDetayDal = YurtDisiTasimaDetayDal;
		}


		public List<YurtDisiTasimaDetay> GetAllList(Expression<Func<YurtDisiTasimaDetay, bool>> filter)
		{
			return _YurtDisiTasimaDetayDal.GetAllList(filter);
		}

		public YurtDisiTasimaDetay GetByID(int id)
		{
			return _YurtDisiTasimaDetayDal.GetByID(id);
		}

		public YurtDisiTasimaDetay GetByPropertyName(string propertyName, string value)
		{
			return _YurtDisiTasimaDetayDal.GetByPropertyName(propertyName, value);
		}

		public List<YurtDisiTasimaDetay> List()
		{
			return _YurtDisiTasimaDetayDal.GetAllList();
		}

		public void TAdd(YurtDisiTasimaDetay t)
		{
			_YurtDisiTasimaDetayDal.Insert(t);
		}

		public void TDelete(YurtDisiTasimaDetay t)
		{
			_YurtDisiTasimaDetayDal.Delete(t);
		}

		public void TUpdate(YurtDisiTasimaDetay t)
		{
			_YurtDisiTasimaDetayDal.Update(t);
		}
	}
}
