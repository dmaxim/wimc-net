using Microsoft.EntityFrameworkCore;

namespace Mx.EntityFramework.Contracts
{
	public interface IEntityContext
	{
		DbContext Context { get; }
		int SaveChanges();
	}
}
