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
	public class SurucuPozisyonManager : ISurucuPozisyonService
	{
		ISurucuPozisyonDal _SurucuPozisyonDal;
		public SurucuPozisyonManager(ISurucuPozisyonDal SurucuPozisyonDal)
		{
			_SurucuPozisyonDal = SurucuPozisyonDal;
		}


		public List<SurucuPozisyon> GetAllList(Expression<Func<SurucuPozisyon, bool>> filter)
		{
			return _SurucuPozisyonDal.GetAllList(filter);
		}

		public SurucuPozisyon GetByID(int id)
		{
			return _SurucuPozisyonDal.GetByID(id);
		}

		public SurucuPozisyon GetByPropertyName(string propertyName, string value)
		{
			return _SurucuPozisyonDal.GetByPropertyName(propertyName, value);
		}

		public List<SurucuPozisyon> List()
		{
			return _SurucuPozisyonDal.GetAllList();
		}

		public void TAdd(SurucuPozisyon t)
		{
			_SurucuPozisyonDal.Insert(t);
		}

		public void TDelete(SurucuPozisyon t)
		{
			_SurucuPozisyonDal.Delete(t);
		}

		public void TUpdate(SurucuPozisyon t)
		{
			_SurucuPozisyonDal.Update(t);
		}
	}
}
