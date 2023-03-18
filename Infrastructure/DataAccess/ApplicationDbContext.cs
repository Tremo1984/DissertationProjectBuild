using ApplicationCore.Models.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess;

internal class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<VehicleDataModel> Vehicles { get; set; }
    public DbSet<VehicleMotHistoryModel> MotHistory { get; set; }
    public DbSet<MotHistoryCommentsModel> MotHistoryComments { get; set; }
}
