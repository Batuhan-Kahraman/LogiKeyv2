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
	public class VersiyonManager : IVersiyonService
	{
		IVersiyonDal _VersiyonDal;
		public VersiyonManager(IVersiyonDal VersiyonDal)
		{
			_VersiyonDal = VersiyonDal;
		}


		public List<Versiyon> GetAllList(Expression<Func<Versiyon, bool>> filter)
		{
			return _VersiyonDal.GetAllList(filter);
		}

		public Versiyon GetByID(int id)
		{
			return _VersiyonDal.GetByID(id);
		}

		public Versiyon GetByPropertyName(string propertyName, string value)
		{
			return _VersiyonDal.GetByPropertyName(propertyName, value);
		}

		public List<Versiyon> List()
		{
			return _VersiyonDal.GetAllList();
		}

		public void TAdd(Versiyon t)
		{
			_VersiyonDal.Insert(t);
		}

		public void TDelete(Versiyon t)
		{
			_VersiyonDal.Delete(t);
		}

		public void TUpdate(Versiyon t)
		{
			_VersiyonDal.Update(t);
		}
	}
}
