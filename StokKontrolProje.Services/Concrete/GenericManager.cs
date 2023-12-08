using StokKontrolProje.Entities.Entities;
using StokKontrolProje.Repositories.Abstract;
using StokKontrolProje.Services.Abstract;
using System.Linq.Expressions;

namespace StokKontrolProje.Services.Concrete
{
	public class GenericManager<T> : IGenericService<T> where T : BaseEntity
	{
		private readonly IGenericRepository<T> repository;

		public GenericManager(IGenericRepository<T> repository)
		{
			this.repository = repository;
		}

		public bool Activate(int id)
		{
			if (id == 0 || GetById(id) == null) return false;
			else return repository.Activate(id);
		}

		public bool Add(T entity)
		{
			return repository.Add(entity);
		}

		public bool Add(List<T> entities)
		{
			return repository.Add(entities);
		}

		public bool Any(Expression<Func<T, bool>> exp)
		{
			return repository.Any(exp);
		}

		public List<T> GetActive()
		{
			return repository.GetActive();
		}

		public IQueryable<T> GetActive(params Expression<Func<T, object>>[] includes)
		{
			return repository.GetActive(includes);
		}

		public List<T> GetAll()
		{
			return repository.GetAll();
		}

		public IQueryable<T> GetAll(params Expression<Func<T, object>>[] includes)
		{
			return repository.GetAll(includes);
		}

		public IQueryable<T> GetAll(Expression<Func<T, bool>> exp, params Expression<Func<T, object>>[] includes)
		{
			return repository.GetAll(exp, includes);
		}

		public T GetByDefault(Expression<Func<T, bool>> exp)
		{
			return repository.GetByDefault(exp);
		}

		public T GetById(int id)
		{
			return repository.GetById(id);
		}

		public IQueryable<T> GetById(int id, params Expression<Func<T, object>>[] includes)
		{
			return repository.GetById(id, includes);
		}

		public List<T> GetDefault(Expression<Func<T, bool>> exp)
		{
			return repository.GetDefault(exp);
		}

		public bool Remove(T entity)
		{
			if (entity == null) return false;
			else return repository.Remove(entity);
		}

		public bool Remove(int id)
		{
			if (id <= 0) return false;
			else return repository.Remove(id);
		}

		public bool RemoveAll(Expression<Func<T, bool>> exp)
		{
			return repository.RemoveAll(exp);
		}

		public int Save()
		{
			return repository.Save();
		}

		public bool Update(T entity)
		{
			if(entity== null) return false;
			else return repository.Update(entity);
		}
	}
}
