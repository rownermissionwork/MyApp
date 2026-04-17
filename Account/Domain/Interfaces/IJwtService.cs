using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken<T>(T model) where T : class;
    }
}
