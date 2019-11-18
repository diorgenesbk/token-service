using Microsoft.EntityFrameworkCore;
using Token.Domain.Entities;
using Token.Infrastructure.Mapping;

namespace Token.Infrastructure.Context
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> context) : base(context) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(new UserMap().Configure);
        }
    }
}
