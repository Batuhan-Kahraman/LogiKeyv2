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
	public class GiderAltTipManager : IGiderAltTipService
	{
		IGiderAltTipDal _GiderAltTipDal;
		public GiderAltTipManager(IGiderAltTipDal GiderAltTipDal)
		{
			_GiderAltTipDal = GiderAltTipDal;
		}


		public List<GiderAltTip> GetAllList(Expression<Func<GiderAltTip, bool>> filter)
		{
			return _GiderAltTipDal.GetAllList(filter);
		}

		public GiderAltTip GetByID(int id)
		{
			return _GiderAltTipDal.GetByID(id);
		}

		public GiderAltTip GetByPropertyName(string propertyName, string value)
		{
			return _GiderAltTipDal.GetByPropertyName(propertyName, value);
		}

		public List<GiderAltTip> List()
		{
			return _GiderAltTipDal.GetAllList();
		}

		public void TAdd(GiderAltTip t)
		{
			_GiderAltTipDal.Insert(t);
		}

		public void TDelete(GiderAltTip t)
		{
			_GiderAltTipDal.Delete(t);
		}

		public void TUpdate(GiderAltTip t)
		{
			_GiderAltTipDal.Update(t);
		}
	}
}
