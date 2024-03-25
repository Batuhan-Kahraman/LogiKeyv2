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
	public class NormalTasimaDetayUrunManager : INormalTasimaDetayUrunService
	{
		INormalTasimaDetayUrunDal _NormalTasimaDetayUrunDal;
		public NormalTasimaDetayUrunManager(INormalTasimaDetayUrunDal NormalTasimaDetayUrunDal)
		{
			_NormalTasimaDetayUrunDal = NormalTasimaDetayUrunDal;
		}


		public List<NormalTasimaDetayUrun> GetAllList(Expression<Func<NormalTasimaDetayUrun, bool>> filter)
		{
			return _NormalTasimaDetayUrunDal.GetAllList(filter);
		}

		public NormalTasimaDetayUrun GetByID(int id)
		{
			return _NormalTasimaDetayUrunDal.GetByID(id);
		}

		public NormalTasimaDetayUrun GetByPropertyName(string propertyName, string value)
		{
			return _NormalTasimaDetayUrunDal.GetByPropertyName(propertyName, value);
		}

		public List<NormalTasimaDetayUrun> List()
		{
			return _NormalTasimaDetayUrunDal.GetAllList();
		}

		public void TAdd(NormalTasimaDetayUrun t)
		{
			_NormalTasimaDetayUrunDal.Insert(t);
		}

		public void TDelete(NormalTasimaDetayUrun t)
		{
			_NormalTasimaDetayUrunDal.Delete(t);
		}

		public void TUpdate(NormalTasimaDetayUrun t)
		{
			_NormalTasimaDetayUrunDal.Update(t);
		}
	}
}
