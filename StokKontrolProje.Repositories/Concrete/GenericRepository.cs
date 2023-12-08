using Microsoft.EntityFrameworkCore;
using StokKontrolProje.Entities.Entities;
using StokKontrolProje.Repositories.Abstract;
using StokKontrolProje.Repositories.Context;
using System.Linq.Expressions;
using System.Transactions;

namespace StokKontrolProje.Repositories.Concrete
{
	public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
	{
		private readonly StokKontrolContext context;

		public GenericRepository(StokKontrolContext context)
		{
			this.context = context;
		}

		public bool Activate(int id)
		{
			T item = GetById(id);
			item.IsActive = true;
			return Update(item);
		}

		public bool Add(T entity)
		{
			try
			{
				context.Set<T>().Add(entity);
				return Save() > 0;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public bool Add(List<T> entities)
		{
			try
			{
				using (TransactionScope ts = new TransactionScope())
				{
					foreach (T entity in entities)
					{
						context.Set<T>().Add(entity);
					}
					ts.Complete();
					return Save() > 0;
				}
			}
			catch (Exception)
			{
				return false;
			}
		}

		public bool Any(Expression<Func<T, bool>> exp) => context.Set<T>().Any(exp);

		public List<T> GetActive() => context.Set<T>().Where(x => x.IsActive).ToList();


		public IQueryable<T> GetActive(params Expression<Func<T, object>>[] includes)
		{
			var query = context.Set<T>().Where(x => x.IsActive == true);
			if (includes != null)
			{
				query = includes.Aggregate(query, (current, include) => current.Include(include));
			}
			return query;
		}

		public List<T> GetAll() => context.Set<T>().ToList();


		public IQueryable<T> GetAll(params Expression<Func<T, object>>[] includes)
		{
			var query = context.Set<T>().AsQueryable();
			if (includes != null)
			{
				query = includes.Aggregate(query, (current, include) => current.Include(include));
			}
			return query;
		}

		public IQueryable<T> GetAll(Expression<Func<T, bool>> exp, params Expression<Func<T, object>>[] includes)
		{
			var query = context.Set<T>().Where(exp);
			if (includes != null)
			{
				query = includes.Aggregate(query, (current, include) => current.Include(include));
			}
			return query;
		}

		public T GetByDefault(Expression<Func<T, bool>> exp) => context.Set<T>().FirstOrDefault(exp);

		public T GetById(int id) => context.Set<T>().Find(id);

		public IQueryable<T> GetById(int id, params Expression<Func<T, object>>[] includes)
		{
			var query = context.Set<T>().Where(x => x.Id == id);
			if (includes != null)
			{
				query = includes.Aggregate(query, (current, include) => current.Include(include));
			}
			return query;
		}

		public List<T> GetDefault(Expression<Func<T, bool>> exp) => context.Set<T>().Where(exp).ToList();

		public bool Remove(T entity)
		{
			entity.IsActive = false;
			return Update(entity);
		}

		public bool Remove(int id)
		{
			try
			{
				T item = GetById(id);
				return Remove(item);
			}
			catch (Exception)
			{
				return false;
			}
		}

		public bool RemoveAll(Expression<Func<T, bool>> exp)
		{
			try
			{
				using (TransactionScope ts = new TransactionScope())
				{
					var collection = GetDefault(exp);
					int counter = 0;
					foreach (var item in collection)
					{
						item.IsActive = false;
						bool operationResult = Update(item);

						if (operationResult) counter++;
					}
					if (collection.Count == counter) ts.Complete();
					else return false;
				}
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public int Save()
		{
			return context.SaveChanges();
		}

		public bool Update(T entity)
		{
			try
			{
				entity.ModifiedDate = DateTime.Now;
				context.Set<T>().Update(entity);
				return Save() > 0;
			}
			catch (Exception)
			{
				return false;
			}
		}
	}
}
