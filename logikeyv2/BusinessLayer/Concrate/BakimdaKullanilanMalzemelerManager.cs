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
	public class BakimdaKullanilanMalzemelerManager : IBakimdaKullanilanMalzemelerService
	{
		IBakimdaKullanilanMalzemelerDal _BakimdaKullanilanMalzemelerDal;
		public BakimdaKullanilanMalzemelerManager(IBakimdaKullanilanMalzemelerDal BakimdaKullanilanMalzemelerDal)
		{
			_BakimdaKullanilanMalzemelerDal = BakimdaKullanilanMalzemelerDal;
		}


		public List<BakimdaKullanilanMalzemeler> GetAllList(Expression<Func<BakimdaKullanilanMalzemeler, bool>> filter)
		{
			return _BakimdaKullanilanMalzemelerDal.GetAllList(filter);
		}

		public BakimdaKullanilanMalzemeler GetByID(int id)
		{
			return _BakimdaKullanilanMalzemelerDal.GetByID(id);
		}

		public BakimdaKullanilanMalzemeler GetByPropertyName(string propertyName, string value)
		{
			return _BakimdaKullanilanMalzemelerDal.GetByPropertyName(propertyName, value);
		}

		public List<BakimdaKullanilanMalzemeler> List()
		{
			return _BakimdaKullanilanMalzemelerDal.GetAllList();
		}

		public void TAdd(BakimdaKullanilanMalzemeler t)
		{
			_BakimdaKullanilanMalzemelerDal.Insert(t);
		}

		public void TDelete(BakimdaKullanilanMalzemeler t)
		{
			_BakimdaKullanilanMalzemelerDal.Delete(t);
		}

		public void TUpdate(BakimdaKullanilanMalzemeler t)
		{
			_BakimdaKullanilanMalzemelerDal.Update(t);
		}
	}
}
