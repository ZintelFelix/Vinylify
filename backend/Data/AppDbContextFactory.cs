using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Vinylify.Backend.Data
{
    /// <summary>
    /// Design-time factory for EF Core tools (migrations, database update).
    /// </summary>
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            // Muss mit exakt demselben Connection-String wie in Program.cs sein
            builder.UseSqlite("Data Source=vinylify.db");
            return new AppDbContext(builder.Options);
        }
    }
}
