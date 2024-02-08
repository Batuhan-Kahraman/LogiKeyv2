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
	public class ModelManager : IModelService
	{
		IModelDal _ModelDal;
		public ModelManager(IModelDal ModelDal)
		{
			_ModelDal = ModelDal;
		}


		public List<Model> GetAllList(Expression<Func<Model, bool>> filter)
		{
			return _ModelDal.GetAllList(filter);
		}

		public Model GetByID(int id)
		{
			return _ModelDal.GetByID(id);
		}

		public Model GetByPropertyName(string propertyName, string value)
		{
			return _ModelDal.GetByPropertyName(propertyName, value);
		}

		public List<Model> List()
		{
			return _ModelDal.GetAllList();
		}

		public void TAdd(Model t)
		{
			_ModelDal.Insert(t);
		}

		public void TDelete(Model t)
		{
			_ModelDal.Delete(t);
		}

		public void TUpdate(Model t)
		{
			_ModelDal.Update(t);
		}
	}
}
