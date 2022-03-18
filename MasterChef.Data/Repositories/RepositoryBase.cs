using MasterChef.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterChef.Data.Repositories
{
    public class RepositoryBase
    {
        protected readonly MainDbContext _ctx;

        public RepositoryBase(MainDbContext ctx)
        {
            _ctx = ctx;
        }

    }
}
