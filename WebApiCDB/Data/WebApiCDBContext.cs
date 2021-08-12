using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApiCDB.Models;

namespace WebApiCDB.Data
{
    public class WebApiCDBContext : DbContext
    {
        public WebApiCDBContext (DbContextOptions<WebApiCDBContext> options)
            : base(options)
        {
        }

        public DbSet<WebApiCDB.Models.FD> FD { get; set; }

        public DbSet<WebApiCDB.Models.RD> RD { get; set; }
    }
}
