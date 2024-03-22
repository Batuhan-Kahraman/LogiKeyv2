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
	public class NormalFaturaManager : INormalFaturaService
	{
		INormalFaturaDal _NormalFaturaDal;
		public NormalFaturaManager(INormalFaturaDal NormalFaturaDal)
		{
			_NormalFaturaDal = NormalFaturaDal;
		}


		public List<NormalFatura> GetAllList(Expression<Func<NormalFatura, bool>> filter)
		{
			return _NormalFaturaDal.GetAllList(filter);
		}

		public NormalFatura GetByID(int id)
		{
			return _NormalFaturaDal.GetByID(id);
		}

		public NormalFatura GetByPropertyName(string propertyName, string value)
		{
			return _NormalFaturaDal.GetByPropertyName(propertyName, value);
		}

		public List<NormalFatura> List()
		{
			return _NormalFaturaDal.GetAllList();
		}

		public void TAdd(NormalFatura t)
		{
			_NormalFaturaDal.Insert(t);
		}

		public void TDelete(NormalFatura t)
		{
			_NormalFaturaDal.Delete(t);
		}

		public void TUpdate(NormalFatura t)
		{
			_NormalFaturaDal.Update(t);
		}
	}
}
