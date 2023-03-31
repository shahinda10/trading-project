using Microsoft.EntityFrameworkCore;
using SignUpForm.Models;

namespace SignUpForm.Data
{
    public class ApplicationContext:DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
        public DbSet<User>users { get; set; }
    }
}
