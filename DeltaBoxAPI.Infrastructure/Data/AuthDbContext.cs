using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaBoxAPI.Infrastructure.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);

        //    var readerRoleId = "258b1cb1-dfbe-4b32-b668-8dafcad14eb7";
        //    var writerRoleId = "b8d99851-fd9d-4ad4-b440-3e088c7f34ed";

        //    var roles = new List<IdentityRole>
        //    {
        //        new IdentityRole
        //        {
        //            Id = readerRoleId,
        //            ConcurrencyStamp = readerRoleId,
        //            Name = "READER",
        //            NormalizedName = "Reader".ToUpper()
        //        },
        //        new IdentityRole
        //        {
        //            Id = writerRoleId,
        //            ConcurrencyStamp = writerRoleId,
        //            Name = "WRITER",
        //            NormalizedName = "Writer".ToUpper()
        //        }
        //    };

        //    builder.Entity<IdentityRole>().HasData(roles);
        //}
    }
}
