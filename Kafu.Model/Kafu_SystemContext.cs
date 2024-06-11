using Microsoft.EntityFrameworkCore;
using Kafu.Model.Entities;
namespace Kafu.Model
{
    public partial class Kafu_SystemContext : DbContext
    {
        public Kafu_SystemContext()
        {
        }

        public Kafu_SystemContext(DbContextOptions<Kafu_SystemContext> options)
            : base(options)
        {
        }

        public virtual DbSet<EmployeImg> EmployeImg { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<KafuRecord> KafuRecord { get; set; }
        public virtual DbSet<ReasonLookup> ReasonLookup { get; set; }
        public virtual DbSet<EmployeDetails> EmployeDetails { get; set; }

     
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          
        
           
        }
    }
}
