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
	public class KazaTuruManager : IKazaTuruService
	{
		IKazaTuruDal _KazaTuruDal;
		public KazaTuruManager(IKazaTuruDal KazaTuruDal)
		{
			_KazaTuruDal = KazaTuruDal;
		}


		public List<KazaTuru> GetAllList(Expression<Func<KazaTuru, bool>> filter)
		{
			return _KazaTuruDal.GetAllList(filter);
		}

		public KazaTuru GetByID(int id)
		{
			return _KazaTuruDal.GetByID(id);
		}

		public KazaTuru GetByPropertyName(string propertyName, string value)
		{
			return _KazaTuruDal.GetByPropertyName(propertyName, value);
		}

		public List<KazaTuru> List()
		{
			return _KazaTuruDal.GetAllList();
		}

		public void TAdd(KazaTuru t)
		{
			_KazaTuruDal.Insert(t);
		}

		public void TDelete(KazaTuru t)
		{
			_KazaTuruDal.Delete(t);
		}

		public void TUpdate(KazaTuru t)
		{
			_KazaTuruDal.Update(t);
		}
	}
}
