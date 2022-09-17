using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TddExample.Application.Configuration;
using TddExample.Application.Model;

namespace TddExample.Application.Interface
{
    public interface IAuthBusiness
    {
        Task<AuthModel?> LoginAsync(LoginModel model);
        Task<AuthModel?> RegisterAsync(RegisterModel model);
    }
}
