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
	public class AracTipManager : IAracTipService
	{
		IAracTipDal _AracTipDal;
		public AracTipManager(IAracTipDal AracTipDal)
		{
			_AracTipDal = AracTipDal;
		}


		public List<AracTip> GetAllList(Expression<Func<AracTip, bool>> filter)
		{
			return _AracTipDal.GetAllList(filter);
		}

		public AracTip GetByID(int id)
		{
			return _AracTipDal.GetByID(id);
		}

		public AracTip GetByPropertyName(string propertyName, string value)
		{
			return _AracTipDal.GetByPropertyName(propertyName, value);
		}

		public List<AracTip> List()
		{
			return _AracTipDal.GetAllList();
		}

		public void TAdd(AracTip t)
		{
			_AracTipDal.Insert(t);
		}

		public void TDelete(AracTip t)
		{
			_AracTipDal.Delete(t);
		}

		public void TUpdate(AracTip t)
		{
			_AracTipDal.Update(t);
		}
	}
}
