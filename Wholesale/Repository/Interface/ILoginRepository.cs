using Wholesale.Server.Data;
using Wholesale.Models;
using System.Threading.Tasks;

namespace Wholesale.Server.Repository
{
    public interface ILoginRepository
    {
        Task<User?> AuthenticateUserAsync(string userName, string password);
    }
}
