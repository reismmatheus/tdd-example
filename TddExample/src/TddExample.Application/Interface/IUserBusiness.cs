using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TddExample.Application.Model;

namespace TddExample.Application.Interface
{
    public interface IUserBusiness
    {
        Task<UserModel> GetProfileAsync(string userId);
    }
}
