using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CacheProxyMockServer.Repositories
{
	public abstract class GenericRepository<T> where T : class
	{
		protected readonly DbContext dc;

		private DbSet<T> ds { get => dc.Set<T>(); }

		public GenericRepository(DbContext dc)
		{
			this.dc = dc;
		}

		public void Add(T entity) //where T : class
		{
			ds.Add(entity);
		}

		public void Update(T entity) //where T : class
		{
			ds.Attach(entity);
		}

		public void Remove(T entity) //where T : class
		{
			ds.Remove(entity);
		}

		public T? GetById(int Id)
		{
			return ds.Find(Id);
		}

		protected IQueryable<T> GetAll() //where T : class
		{
			return ds;
		}

		protected IQueryable<T> FindBy(Expression<Func<T, bool>> predicate) //where T : class
		{
			return ds.Where(predicate);
		}

		/// <summary>
		/// page index starts from 1
		/// </summary>
		protected IQueryable<T> FindBy(Expression<Func<T, bool>> predicate, int pageIndex, int pageSize) //where T : class
		{
			return FindBy(predicate).Skip((pageIndex - 1) * pageSize).Take(pageSize);
		}

		protected T FindSingle(Expression<Func<T, bool>> predicate) //where T : class
		{
			return FindBy(predicate).SingleOrDefault();
		}
	}
}
