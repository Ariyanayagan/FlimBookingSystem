using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flim.Infrastructures.Interfaces
{
    public interface IJwtService
    {
        string GenerateJwtToken(int userId, string role);
    }
}
