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
	public class GiderTipManager : IGiderTipService
	{
		IGiderTipDal _GiderTipDal;
		public GiderTipManager(IGiderTipDal GiderTipDal)
		{
			_GiderTipDal = GiderTipDal;
		}


		public List<GiderTip> GetAllList(Expression<Func<GiderTip, bool>> filter)
		{
			return _GiderTipDal.GetAllList(filter);
		}

		public GiderTip GetByID(int id)
		{
			return _GiderTipDal.GetByID(id);
		}

		public GiderTip GetByPropertyName(string propertyName, string value)
		{
			return _GiderTipDal.GetByPropertyName(propertyName, value);
		}

		public List<GiderTip> List()
		{
			return _GiderTipDal.GetAllList();
		}

		public void TAdd(GiderTip t)
		{
			_GiderTipDal.Insert(t);
		}

		public void TDelete(GiderTip t)
		{
			_GiderTipDal.Delete(t);
		}

		public void TUpdate(GiderTip t)
		{
			_GiderTipDal.Update(t);
		}
	}
}
