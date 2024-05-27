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
	public class StokKategoriManager : IStokKategoriService
	{
		IStokKategoriDal _StokKategoriDal;
		public StokKategoriManager(IStokKategoriDal StokKategoriDal)
		{
			_StokKategoriDal = StokKategoriDal;
		}


		public List<StokKategori> GetAllList(Expression<Func<StokKategori, bool>> filter)
		{
			return _StokKategoriDal.GetAllList(filter);
		}

		public StokKategori GetByID(int id)
		{
			return _StokKategoriDal.GetByID(id);
		}

		public StokKategori GetByPropertyName(string propertyName, string value)
		{
			return _StokKategoriDal.GetByPropertyName(propertyName, value);
		}

		public List<StokKategori> List()
		{
			return _StokKategoriDal.GetAllList();
		}

		public void TAdd(StokKategori t)
		{
			_StokKategoriDal.Insert(t);
		}

		public void TDelete(StokKategori t)
		{
			_StokKategoriDal.Delete(t);
		}

		public void TUpdate(StokKategori t)
		{
			_StokKategoriDal.Update(t);
		}
	}
}
