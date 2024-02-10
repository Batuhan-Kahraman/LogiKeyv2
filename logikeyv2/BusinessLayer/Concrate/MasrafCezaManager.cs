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
	public class MasrafCezaManager : IMasrafCezaService
	{
		IMasrafCezaDal _MasrafCezaDal;
		public MasrafCezaManager(IMasrafCezaDal MasrafCezaDal)
		{
			_MasrafCezaDal = MasrafCezaDal;
		}


		public List<MasrafCeza> GetAllList(Expression<Func<MasrafCeza, bool>> filter)
		{
			return _MasrafCezaDal.GetAllList(filter);
		}

		public MasrafCeza GetByID(int id)
		{
			return _MasrafCezaDal.GetByID(id);
		}

		public MasrafCeza GetByPropertyName(string propertyName, string value)
		{
			return _MasrafCezaDal.GetByPropertyName(propertyName, value);
		}

		public List<MasrafCeza> List()
		{
			return _MasrafCezaDal.GetAllList();
		}

		public void TAdd(MasrafCeza t)
		{
			_MasrafCezaDal.Insert(t);
		}

		public void TDelete(MasrafCeza t)
		{
			_MasrafCezaDal.Delete(t);
		}

		public void TUpdate(MasrafCeza t)
		{
			_MasrafCezaDal.Update(t);
		}
	}
}
