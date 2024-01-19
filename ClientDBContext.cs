using ClientAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ClientAPI
{
    public class ClientDBContext : DbContext
    {
        private readonly string _connectionString;
             
        public ClientDBContext(DbContextOptions<ClientDBContext> options) : base(options)
        {
            
        }

        //public ClientDBContext()
        //{
        //    var configuration = new ConfigurationBuilder()
        //        .SetBasePath(Directory.GetCurrentDirectory())
        //        .AddJsonFile("appsettings.json")
        //        .Build();
        //    _connectionString = configuration.GetConnectionString("ClientDBConection");
        //}

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseNpgsql(_connectionString);
        //    }
        //}

        public DbSet<Client> Clients { get; set; }
    }
}
