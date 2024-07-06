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
	public class YurtDisiTasimaDetayUrunManager : IYurtDisiTasimaDetayUrunService
	{
		IYurtDisiTasimaDetayUrunDal _YurtDisiTasimaDetayUrunDal;
		public YurtDisiTasimaDetayUrunManager(IYurtDisiTasimaDetayUrunDal YurtDisiTasimaDetayUrunDal)
		{
			_YurtDisiTasimaDetayUrunDal = YurtDisiTasimaDetayUrunDal;
		}


		public List<YurtDisiTasimaDetayUrun> GetAllList(Expression<Func<YurtDisiTasimaDetayUrun, bool>> filter)
		{
			return _YurtDisiTasimaDetayUrunDal.GetAllList(filter);
		}

		public YurtDisiTasimaDetayUrun GetByID(int id)
		{
			return _YurtDisiTasimaDetayUrunDal.GetByID(id);
		}

		public YurtDisiTasimaDetayUrun GetByPropertyName(string propertyName, string value)
		{
			return _YurtDisiTasimaDetayUrunDal.GetByPropertyName(propertyName, value);
		}

		public List<YurtDisiTasimaDetayUrun> List()
		{
			return _YurtDisiTasimaDetayUrunDal.GetAllList();
		}

		public void TAdd(YurtDisiTasimaDetayUrun t)
		{
			_YurtDisiTasimaDetayUrunDal.Insert(t);
		}

		public void TDelete(YurtDisiTasimaDetayUrun t)
		{
			_YurtDisiTasimaDetayUrunDal.Delete(t);
		}

		public void TUpdate(YurtDisiTasimaDetayUrun t)
		{
			_YurtDisiTasimaDetayUrunDal.Update(t);
		}
	}
}
