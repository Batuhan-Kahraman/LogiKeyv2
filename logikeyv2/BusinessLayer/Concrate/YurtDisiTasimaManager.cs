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
	public class YurtDisiTasimaManager : IYurtDisiTasimaService
	{
		IYurtDisiTasimaDal _YurtDisiTasimaDal;
		public YurtDisiTasimaManager(IYurtDisiTasimaDal YurtDisiTasimaDal)
		{
			_YurtDisiTasimaDal = YurtDisiTasimaDal;
		}


		public List<YurtDisiTasima> GetAllList(Expression<Func<YurtDisiTasima, bool>> filter)
		{
			return _YurtDisiTasimaDal.GetAllList(filter);
		}

		public YurtDisiTasima GetByID(int id)
		{
			return _YurtDisiTasimaDal.GetByID(id);
		}

		public YurtDisiTasima GetByPropertyName(string propertyName, string value)
		{
			return _YurtDisiTasimaDal.GetByPropertyName(propertyName, value);
		}

		public List<YurtDisiTasima> List()
		{
			return _YurtDisiTasimaDal.GetAllList();
		}

		public void TAdd(YurtDisiTasima t)
		{
			_YurtDisiTasimaDal.Insert(t);
		}

		public void TDelete(YurtDisiTasima t)
		{
			_YurtDisiTasimaDal.Delete(t);
		}

		public void TUpdate(YurtDisiTasima t)
		{
			_YurtDisiTasimaDal.Update(t);
		}
	}
}
