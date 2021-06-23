using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Mx.EntityFramework.Contracts;

namespace Mx.EntityFramework.Repositories
{
	public abstract class Repository<TEntity> : RepositoryReadOnly<TEntity>, IRepository<TEntity> where TEntity : class
	{
		private readonly IEntityContext _entityContext;

		protected Repository(IEntityContext entityContext) : base(entityContext)
		{
			_entityContext = entityContext;
		}

		public virtual void Insert(TEntity entity)
		{
			Context.Set<TEntity>().Add(entity);

		}


		public virtual void Update(TEntity entity)
		{
			if (!Exists(entity))
			{
				Context.Set<TEntity>().Attach(entity);
			}
			Context.Entry(entity).State = EntityState.Modified;

		}

		public virtual void Delete(TEntity entity)
		{
			if (!Exists(entity))
			{
				Context.Set<TEntity>().Attach(entity);
			}

			Context.Set<TEntity>().Remove(entity);

		}


		public virtual void SaveChanges()
		{
			
				Context.SaveChanges(); 
		}

		private bool Exists(TEntity entity) 
		{
			return Context.Set<TEntity>().Local.Any(attachedEntity => attachedEntity == entity);
		}
	}
}
