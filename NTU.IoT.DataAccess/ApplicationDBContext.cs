using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NTU.IoT.Models;

namespace NTU.IoT.DataAccess;
public class ApplicationDBContext: IdentityDbContext
{



    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }

    public DbSet<DeviceType> device_types { set; get; }
    public DbSet<ApplicationUser> applicationUsers { set; get; }
    public DbSet<Device> devices{ set; get; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);



        
            modelBuilder.Entity<DeviceType>().HasData(
            new DeviceType {
                Id = new Guid("5c04a400-eaab-4530-ade5-7a8dd9527d24"),
                name= "Physio",
                topic_name="Physio",
                table_name="physio"},
            new DeviceType
            {
                Id = new Guid("35cadad3-d405-43a2-a706-389bb94cd6ad"),
                name = "Environmental",
                topic_name = "Env",
                table_name = "env"
            }
            ) ;
        
    }

    

}

