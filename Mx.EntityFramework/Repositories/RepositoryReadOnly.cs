using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Mx.EntityFramework.Contracts;

namespace Mx.EntityFramework.Repositories
{
	public abstract class RepositoryReadOnly<TEntity> : IRepositoryReadOnly<TEntity> where TEntity : class
	{
		private readonly IEntityContext _entityContext;

		protected RepositoryReadOnly(IEntityContext entityContext)
		{
			_entityContext = entityContext;
		}



		protected DbContext Context
		{
			get { return _entityContext.Context; }
		}

		public virtual IQueryable<TEntity> GetAll()
		{
			return Context.Set<TEntity>();
		}


		public virtual void Reload(TEntity entity)
		{
			Context.Entry<TEntity>(entity).Reload();
		}

		public virtual IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
		{
			return Context.Set<TEntity>().Where(predicate);
		}
	
	}
}
