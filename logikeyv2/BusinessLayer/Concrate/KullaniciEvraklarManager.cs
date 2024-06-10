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
	public class KullaniciEvraklarManager : IKullaniciEvraklarService
	{
		IKullaniciEvraklarDal _KullaniciEvraklarDal;
		public KullaniciEvraklarManager(IKullaniciEvraklarDal KullaniciEvraklarDal)
		{
			_KullaniciEvraklarDal = KullaniciEvraklarDal;
		}


		public List<KullaniciEvraklar> GetAllList(Expression<Func<KullaniciEvraklar, bool>> filter)
		{
			return _KullaniciEvraklarDal.GetAllList(filter);
		}

		public KullaniciEvraklar GetByID(int id)
		{
			return _KullaniciEvraklarDal.GetByID(id);
		}

		public KullaniciEvraklar GetByPropertyName(string propertyName, string value)
		{
			return _KullaniciEvraklarDal.GetByPropertyName(propertyName, value);
		}

		public List<KullaniciEvraklar> List()
		{
			return _KullaniciEvraklarDal.GetAllList();
		}

		public void TAdd(KullaniciEvraklar t)
		{
			_KullaniciEvraklarDal.Insert(t);
		}

		public void TDelete(KullaniciEvraklar t)
		{
			_KullaniciEvraklarDal.Delete(t);
		}

		public void TUpdate(KullaniciEvraklar t)
		{
			_KullaniciEvraklarDal.Update(t);
		}
	}
}
