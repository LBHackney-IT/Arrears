using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArrearsApi.V1.Infrastructure
{
    public class ArrearsContext: DbContext
    {
        public ArrearsContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ArrearsDbEntity> Arrears { get; set; }
    }
}
