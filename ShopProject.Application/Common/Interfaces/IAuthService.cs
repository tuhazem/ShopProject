using ShopProject.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Application.Common.Interfaces
{
    public interface IAuthService
    {
        Task<AuthModel> GetTokenAsync(LoginModel model);
        Task<AuthModel> RegisterAsync(RegisterModel model);
    }
}
