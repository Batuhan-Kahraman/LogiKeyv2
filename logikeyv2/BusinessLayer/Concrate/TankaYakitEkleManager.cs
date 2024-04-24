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
	public class TankaYakitEkleManager : ITankaYakitEkleService
	{
		ITankaYakitEkleDal _TankaYakitEkleDal;
		public TankaYakitEkleManager(ITankaYakitEkleDal TankaYakitEkleDal)
		{
			_TankaYakitEkleDal = TankaYakitEkleDal;
		}


		public List<TankaYakitEkle> GetAllList(Expression<Func<TankaYakitEkle, bool>> filter)
		{
			return _TankaYakitEkleDal.GetAllList(filter);
		}

		public TankaYakitEkle GetByID(int id)
		{
			return _TankaYakitEkleDal.GetByID(id);
		}

		public TankaYakitEkle GetByPropertyName(string propertyName, string value)
		{
			return _TankaYakitEkleDal.GetByPropertyName(propertyName, value);
		}

		public List<TankaYakitEkle> List()
		{
			return _TankaYakitEkleDal.GetAllList();
		}

		public void TAdd(TankaYakitEkle t)
		{
			_TankaYakitEkleDal.Insert(t);
		}

		public void TDelete(TankaYakitEkle t)
		{
			_TankaYakitEkleDal.Delete(t);
		}

		public void TUpdate(TankaYakitEkle t)
		{
			_TankaYakitEkleDal.Update(t);
		}
	}
}
