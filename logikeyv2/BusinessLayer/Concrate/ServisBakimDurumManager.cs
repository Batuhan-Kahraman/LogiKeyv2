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
	public class ServisBakimDurumManager : IServisBakimDurumService
	{
		IServisBakimDurumDal _ServisBakimDurumDal;
		public ServisBakimDurumManager(IServisBakimDurumDal ServisBakimDurumDal)
		{
			_ServisBakimDurumDal = ServisBakimDurumDal;
		}


		public List<ServisBakimDurum> GetAllList(Expression<Func<ServisBakimDurum, bool>> filter)
		{
			return _ServisBakimDurumDal.GetAllList(filter);
		}

		public ServisBakimDurum GetByID(int id)
		{
			return _ServisBakimDurumDal.GetByID(id);
		}

		public ServisBakimDurum GetByPropertyName(string propertyName, string value)
		{
			return _ServisBakimDurumDal.GetByPropertyName(propertyName, value);
		}

		public List<ServisBakimDurum> List()
		{
			return _ServisBakimDurumDal.GetAllList();
		}

		public void TAdd(ServisBakimDurum t)
		{
			_ServisBakimDurumDal.Insert(t);
		}

		public void TDelete(ServisBakimDurum t)
		{
			_ServisBakimDurumDal.Delete(t);
		}

		public void TUpdate(ServisBakimDurum t)
		{
			_ServisBakimDurumDal.Update(t);
		}
	}
}
