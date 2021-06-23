

using System.Threading.Tasks;

namespace Mx.EntityFramework.Contracts
{
	public interface IUnitOfWork
	{
		void SaveChanges();
		Task SaveChangesAsync();
	}
}
