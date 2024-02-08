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
	public class GrupManager : IGrupService
	{
		IGrupDal _GrupDal;
		public GrupManager(IGrupDal GrupDal)
		{
			_GrupDal = GrupDal;
		}


		public List<Grup> GetAllList(Expression<Func<Grup, bool>> filter)
		{
			return _GrupDal.GetAllList(filter);
		}

		public Grup GetByID(int id)
		{
			return _GrupDal.GetByID(id);
		}

		public Grup GetByPropertyName(string propertyName, string value)
		{
			return _GrupDal.GetByPropertyName(propertyName, value);
		}

		public List<Grup> List()
		{
			return _GrupDal.GetAllList();
		}

		public void TAdd(Grup t)
		{
			_GrupDal.Insert(t);
		}

		public void TDelete(Grup t)
		{
			_GrupDal.Delete(t);
		}

		public void TUpdate(Grup t)
		{
			_GrupDal.Update(t);
		}
	}
}
