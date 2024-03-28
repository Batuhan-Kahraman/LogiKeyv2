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
	public class KDVOraniManager : IKDVOraniService
	{
		IKDVOraniDal _KDVOraniDal;
		public KDVOraniManager(IKDVOraniDal KDVOraniDal)
		{
			_KDVOraniDal = KDVOraniDal;
		}


		public List<KDVOrani> GetAllList(Expression<Func<KDVOrani, bool>> filter)
		{
			return _KDVOraniDal.GetAllList(filter);
		}

		public KDVOrani GetByID(int id)
		{
			return _KDVOraniDal.GetByID(id);
		}

		public KDVOrani GetByPropertyName(string propertyName, string value)
		{
			return _KDVOraniDal.GetByPropertyName(propertyName, value);
		}

		public List<KDVOrani> List()
		{
			return _KDVOraniDal.GetAllList();
		}

		public void TAdd(KDVOrani t)
		{
			_KDVOraniDal.Insert(t);
		}

		public void TDelete(KDVOrani t)
		{
			_KDVOraniDal.Delete(t);
		}

		public void TUpdate(KDVOrani t)
		{
			_KDVOraniDal.Update(t);
		}
	}
}
