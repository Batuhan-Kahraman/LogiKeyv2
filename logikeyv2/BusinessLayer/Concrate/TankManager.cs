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
	public class TankManager : ITankService
	{
		ITankDal _TankDal;
		public TankManager(ITankDal TankDal)
		{
			_TankDal = TankDal;
		}


		public List<Tank> GetAllList(Expression<Func<Tank, bool>> filter)
		{
			return _TankDal.GetAllList(filter);
		}

		public Tank GetByID(int id)
		{
			return _TankDal.GetByID(id);
		}

		public Tank GetByPropertyName(string propertyName, string value)
		{
			return _TankDal.GetByPropertyName(propertyName, value);
		}

		public List<Tank> List()
		{
			return _TankDal.GetAllList();
		}

		public void TAdd(Tank t)
		{
			_TankDal.Insert(t);
		}

		public void TDelete(Tank t)
		{
			_TankDal.Delete(t);
		}

		public void TUpdate(Tank t)
		{
			_TankDal.Update(t);
		}
	}
}
