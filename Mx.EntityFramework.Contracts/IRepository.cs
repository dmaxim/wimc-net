
namespace Mx.EntityFramework.Contracts
{
	public interface IRepository<TEntity> : IUnitOfWork, IRepositoryReadOnly<TEntity> where TEntity : class
	{
		void Insert(TEntity entity);
		void Update(TEntity entity);
		void Delete(TEntity entity);

	}
}
