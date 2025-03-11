using Wholesale.Models;
using Wholesale.Server.Data;
using Wholesale.Server.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Wholesale.Server.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly ApplicationDbContext _context;

        public LoginRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User?> AuthenticateUserAsync(string identifier, string password)
        {
            var user = await _context.UserWholesale
                .Where(u => u.UserName == identifier || u.UserEmail == identifier
                || u.SlpCode == identifier ||  u.U_CodigoPOS == identifier || u.UserPhone == identifier)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                Console.WriteLine("Usuario no encontrado");
                return null;
            }

            if (!BCrypt.Net.BCrypt.Verify(password, user.UserPassword))
            {
                Console.WriteLine("Contraseña incorrecta");
                return null;
            }

            Console.WriteLine("Usuario autenticado correctamente");
            return user;
        }


    }
}
