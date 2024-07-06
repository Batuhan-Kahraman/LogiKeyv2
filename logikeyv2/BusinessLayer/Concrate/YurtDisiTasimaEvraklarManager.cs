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
	public class YurtDisiTasimaEvraklarManager : IYurtDisiTasimaEvraklarService
	{
		IYurtDisiTasimaEvraklarDal _YurtDisiTasimaEvraklarDal;
		public YurtDisiTasimaEvraklarManager(IYurtDisiTasimaEvraklarDal YurtDisiTasimaEvraklarDal)
		{
			_YurtDisiTasimaEvraklarDal = YurtDisiTasimaEvraklarDal;
		}


		public List<YurtDisiTasimaEvraklar> GetAllList(Expression<Func<YurtDisiTasimaEvraklar, bool>> filter)
		{
			return _YurtDisiTasimaEvraklarDal.GetAllList(filter);
		}

		public YurtDisiTasimaEvraklar GetByID(int id)
		{
			return _YurtDisiTasimaEvraklarDal.GetByID(id);
		}

		public YurtDisiTasimaEvraklar GetByPropertyName(string propertyName, string value)
		{
			return _YurtDisiTasimaEvraklarDal.GetByPropertyName(propertyName, value);
		}

		public List<YurtDisiTasimaEvraklar> List()
		{
			return _YurtDisiTasimaEvraklarDal.GetAllList();
		}

		public void TAdd(YurtDisiTasimaEvraklar t)
		{
			_YurtDisiTasimaEvraklarDal.Insert(t);
		}

		public void TDelete(YurtDisiTasimaEvraklar t)
		{
			_YurtDisiTasimaEvraklarDal.Delete(t);
		}

		public void TUpdate(YurtDisiTasimaEvraklar t)
		{
			_YurtDisiTasimaEvraklarDal.Update(t);
		}
	}
}
