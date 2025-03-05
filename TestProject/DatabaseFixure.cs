using Microsoft.EntityFrameworkCore;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Tests
{
    public class DatabaseFixure
    {
        public ApiDbToCodeContext Context { get; private set; }
        public DatabaseFixure()
        {
            var options = new DbContextOptionsBuilder<ApiDbToCodeContext>()
                .UseSqlServer("Server=DESKTOP-58UFOBM;Database=TestDB;User Id=sa;Password=9553595535;TrustServerCertificate=True;")
                .Options;
            Context = new ApiDbToCodeContext(options);
            Context.Database.EnsureCreated();
        }

        public void Dispose()
        {
            Context.Database.EnsureCreated();
            Context.Dispose();
        }
    }
}
