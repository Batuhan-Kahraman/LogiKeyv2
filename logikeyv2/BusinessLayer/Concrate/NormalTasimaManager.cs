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
	public class NormalTasimaManager : INormalTasimaService
	{
		INormalTasimaDal _NormalTasimaDal;
		public NormalTasimaManager(INormalTasimaDal NormalTasimaDal)
		{
			_NormalTasimaDal = NormalTasimaDal;
		}


		public List<NormalTasima> GetAllList(Expression<Func<NormalTasima, bool>> filter)
		{
			return _NormalTasimaDal.GetAllList(filter);
		}

		public NormalTasima GetByID(int id)
		{
			return _NormalTasimaDal.GetByID(id);
		}

		public NormalTasima GetByPropertyName(string propertyName, string value)
		{
			return _NormalTasimaDal.GetByPropertyName(propertyName, value);
		}

		public List<NormalTasima> List()
		{
			return _NormalTasimaDal.GetAllList();
		}

		public void TAdd(NormalTasima t)
		{
			_NormalTasimaDal.Insert(t);
		}

		public void TDelete(NormalTasima t)
		{
			_NormalTasimaDal.Delete(t);
		}

		public void TUpdate(NormalTasima t)
		{
			_NormalTasimaDal.Update(t);
		}
	}
}
