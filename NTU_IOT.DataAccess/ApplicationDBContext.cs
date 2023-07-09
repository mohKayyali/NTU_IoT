using Microsoft.EntityFrameworkCore;
using NTU_IoT.Models;

namespace NTU_IOT.DataAccess;
public class ApplicationDBContext: DbContext
{
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }

    public DbSet<DeviceType> device_types { set; get; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DeviceType>().HasData(
            new DeviceType {
                Id = Guid.NewGuid(),
                name= "Physio",
                topic_name="Physio",
                table_name="physio"},
            new DeviceType
            {
                Id = Guid.NewGuid(),
                name = "Environmental",
                topic_name = "Env",
                table_name = "env"
            }
            ) ;
        
    }

}

