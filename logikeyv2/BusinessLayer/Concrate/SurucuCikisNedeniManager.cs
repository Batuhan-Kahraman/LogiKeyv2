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
	public class SurucuCikisNedeniManager : ISurucuCikisNedeniService
	{
		ISurucuCikisNedeniDal _SurucuCikisNedeniDal;
		public SurucuCikisNedeniManager(ISurucuCikisNedeniDal SurucuCikisNedeniDal)
		{
			_SurucuCikisNedeniDal = SurucuCikisNedeniDal;
		}


		public List<SurucuCikisNedeni> GetAllList(Expression<Func<SurucuCikisNedeni, bool>> filter)
		{
			return _SurucuCikisNedeniDal.GetAllList(filter);
		}

		public SurucuCikisNedeni GetByID(int id)
		{
			return _SurucuCikisNedeniDal.GetByID(id);
		}

		public SurucuCikisNedeni GetByPropertyName(string propertyName, string value)
		{
			return _SurucuCikisNedeniDal.GetByPropertyName(propertyName, value);
		}

		public List<SurucuCikisNedeni> List()
		{
			return _SurucuCikisNedeniDal.GetAllList();
		}

		public void TAdd(SurucuCikisNedeni t)
		{
			_SurucuCikisNedeniDal.Insert(t);
		}

		public void TDelete(SurucuCikisNedeni t)
		{
			_SurucuCikisNedeniDal.Delete(t);
		}

		public void TUpdate(SurucuCikisNedeni t)
		{
			_SurucuCikisNedeniDal.Update(t);
		}
	}
}
