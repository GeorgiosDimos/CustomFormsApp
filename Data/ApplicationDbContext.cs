namespace CustomFormsApp.Data
{
    using CustomFormsApp.Models;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<CustomForm> CustomForms { get; set; }
        public DbSet<CustomField> CustomFields { get; set; }
    }
}
