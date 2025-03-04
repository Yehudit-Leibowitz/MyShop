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
                .UseSqlServer("Server = SRV2\\PUPILS; Database =ProjectTest; Trusted_Connection = True; TrustServerCertificate = True")
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
