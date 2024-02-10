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
	public class TasinacakUrunManager : ITasinacakUrunService
	{
		ITasinacakUrunDal _TasinacakUrunDal;
		public TasinacakUrunManager(ITasinacakUrunDal TasinacakUrunDal)
		{
			_TasinacakUrunDal = TasinacakUrunDal;
		}


		public List<TasinacakUrun> GetAllList(Expression<Func<TasinacakUrun, bool>> filter)
		{
			return _TasinacakUrunDal.GetAllList(filter);
		}

		public TasinacakUrun GetByID(int id)
		{
			return _TasinacakUrunDal.GetByID(id);
		}

		public TasinacakUrun GetByPropertyName(string propertyName, string value)
		{
			return _TasinacakUrunDal.GetByPropertyName(propertyName, value);
		}

		public List<TasinacakUrun> List()
		{
			return _TasinacakUrunDal.GetAllList();
		}

		public void TAdd(TasinacakUrun t)
		{
			_TasinacakUrunDal.Insert(t);
		}

		public void TDelete(TasinacakUrun t)
		{
			_TasinacakUrunDal.Delete(t);
		}

		public void TUpdate(TasinacakUrun t)
		{
			_TasinacakUrunDal.Update(t);
		}
	}
}
