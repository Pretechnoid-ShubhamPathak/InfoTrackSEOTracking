namespace Backend.Helpers;

using Microsoft.EntityFrameworkCore;
using Backend.Entities;
using System.Linq;

public class DataContext : DbContext
{
    protected readonly IConfiguration Configuration;

    public DataContext(){}

    public DataContext(IConfiguration configuration){
        Configuration = configuration;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder options){
        // connect to sql server database
        options.UseSqlServer(Configuration.GetConnectionString("WebApiDatabase"), o => o
            .MinBatchSize(1)
            .MaxBatchSize(100));
    }
    public DbSet<User> Users { get; set; }
    public DbSet<SearchHistoryEntry> SearchHistoryManager { get; set; }
    

}