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
	public class MarkaManager : IMarkaService
	{
		IMarkaDal _MarkaDal;
		public MarkaManager(IMarkaDal MarkaDal)
		{
			_MarkaDal = MarkaDal;
		}


		public List<Marka> GetAllList(Expression<Func<Marka, bool>> filter)
		{
			return _MarkaDal.GetAllList(filter);
		}

		public Marka GetByID(int id)
		{
			return _MarkaDal.GetByID(id);
		}

		public Marka GetByPropertyName(string propertyName, string value)
		{
			return _MarkaDal.GetByPropertyName(propertyName, value);
		}

		public List<Marka> List()
		{
			return _MarkaDal.GetAllList();
		}

		public void TAdd(Marka t)
		{
			_MarkaDal.Insert(t);
		}

		public void TDelete(Marka t)
		{
			_MarkaDal.Delete(t);
		}

		public void TUpdate(Marka t)
		{
			_MarkaDal.Update(t);
		}
	}
}
