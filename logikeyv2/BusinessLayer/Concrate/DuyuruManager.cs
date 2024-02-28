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
	public class DuyuruManager : IDuyuruService
	{
		IDuyuruDal _DuyuruDal;
		public DuyuruManager(IDuyuruDal DuyuruDal)
		{
			_DuyuruDal = DuyuruDal;
		}


		public List<Duyuru> GetAllList(Expression<Func<Duyuru, bool>> filter)
		{
			return _DuyuruDal.GetAllList(filter);
		}

		public Duyuru GetByID(int id)
		{
			return _DuyuruDal.GetByID(id);
		}

		public Duyuru GetByPropertyName(string propertyName, string value)
		{
			return _DuyuruDal.GetByPropertyName(propertyName, value);
		}

		public List<Duyuru> List()
		{
			return _DuyuruDal.GetAllList();
		}

		public void TAdd(Duyuru t)
		{
			_DuyuruDal.Insert(t);
		}

		public void TDelete(Duyuru t)
		{
			_DuyuruDal.Delete(t);
		}

		public void TUpdate(Duyuru t)
		{
			_DuyuruDal.Update(t);
		}
	}
}
