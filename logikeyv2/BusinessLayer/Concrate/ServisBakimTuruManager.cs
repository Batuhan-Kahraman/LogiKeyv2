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
	public class ServisBakimTuruManager : IServisBakimTuruService
	{
		IServisBakimTuruDal _ServisBakimTuruDal;
		public ServisBakimTuruManager(IServisBakimTuruDal ServisBakimTuruDal)
		{
			_ServisBakimTuruDal = ServisBakimTuruDal;
		}


		public List<ServisBakimTuru> GetAllList(Expression<Func<ServisBakimTuru, bool>> filter)
		{
			return _ServisBakimTuruDal.GetAllList(filter);
		}

		public ServisBakimTuru GetByID(int id)
		{
			return _ServisBakimTuruDal.GetByID(id);
		}

		public ServisBakimTuru GetByPropertyName(string propertyName, string value)
		{
			return _ServisBakimTuruDal.GetByPropertyName(propertyName, value);
		}

		public List<ServisBakimTuru> List()
		{
			return _ServisBakimTuruDal.GetAllList();
		}

		public void TAdd(ServisBakimTuru t)
		{
			_ServisBakimTuruDal.Insert(t);
		}

		public void TDelete(ServisBakimTuru t)
		{
			_ServisBakimTuruDal.Delete(t);
		}

		public void TUpdate(ServisBakimTuru t)
		{
			_ServisBakimTuruDal.Update(t);
		}
	}
}
