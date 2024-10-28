using Flim.Application.Records;
using Flim.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flim.Application.Interfaces
{
    /// <summary>
    /// contract of user service.
    /// </summary>
    public interface IUserService
    {
        Task<User> AuthenticateAsync(string username, string password);
        Task<bool> RegisterAsync(User user);

        Task<List<MyOrderRecord>> GetBookingsAsync(int id);
    }
}
