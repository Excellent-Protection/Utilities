using System;
using Utilities.DataAccess.Labor;

namespace Utilities.Helpers
{
    public interface IDbFactory : IDisposable
    {
        LaborDbContext Init();
    }
}
