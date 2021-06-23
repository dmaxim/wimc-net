using System;
using System.Linq;
using System.Linq.Expressions;

namespace Mx.EntityFramework.Contracts
{
	public interface IRepositoryReadOnly<TEntity> where TEntity : class
	{
		IQueryable<TEntity> GetAll();

		IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);

		void Reload(TEntity entity);


	}
}
