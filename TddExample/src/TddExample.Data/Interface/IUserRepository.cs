using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TddExample.Domain;

namespace TddExample.Data.Interface
{
    public interface IUserRepository
    {
        Task<User> AuthenticateAsync(string email, string password);
        Task<User> AddAsync(string name, string email, string password);
    }
}
