using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Helpers;

namespace Utilities.DataAccess.Labor
{
     public class DbFactory : Disposable, IDbFactory
    {
         LaborDbContext dbContext;

        public LaborDbContext Init()
        {
            return dbContext ?? (dbContext = new LaborDbContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
            base.DisposeCore();
        }

    }
}
