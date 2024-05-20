using Microsoft.EntityFrameworkCore;

namespace Company.Models
{
    public class CompanyContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=Company;Integrated Security=True;Trusted_Connection=True;TrustServerCertificate=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dependent>().HasKey(table => new { table.ESSN, table.Name });
            modelBuilder.Entity<EmployeeProject>().HasKey(table => new { table.ESSN, table.PNo });
            modelBuilder.Entity<DepartmentLocation>().HasKey(table => new { table.DNumber, table.Location });
            modelBuilder.Entity<Department>()
                                            .HasMany(d => d.Employees)
                                            .WithOne(e => e.Department)
                                            .HasForeignKey(e => e.DNo);
        }

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Dependent> Dependents { get; set; }
        public virtual DbSet<EmployeeProject> EmployeesProjects { get; set; }
        public virtual DbSet<DepartmentLocation> DepartmentsLocations { get; set; }

    }
}
