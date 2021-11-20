using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;

namespace wpfRabbitMQ.Postgres.DB
{
    public class Context : DbContext
    {
        public Context() : base()
        {
            OperacoesIniciais(false);
        }



        public Context(bool blnVersionamento = false) : base()
        {
            OperacoesIniciais(blnVersionamento);
        }



        private void OperacoesIniciais(bool blnVersionamento = false)
        {
            ChangeTracker.LazyLoadingEnabled = false;
            ChangeTracker.AutoDetectChangesEnabled = false;

            if (blnVersionamento)
            {
                Database.Migrate();
            }
        }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var strLocalBanco = "127.0.0.1";
            var port = "5432";
            var strUser = "postgres";
            var strSenha = "developer";
            var strNomeBanco = "PostgresRabbitMQ";

            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseNpgsql($"Host={strLocalBanco}; " + $"Port={port}; " + $"User Id={strUser};" + $"Password={strSenha};" + $"Database={strNomeBanco};")
                              .EnableSensitiveDataLogging()
                              .LogTo(message => Debug.WriteLine("\r\n######################################\r\n" + message), Microsoft.Extensions.Logging.LogLevel.Information);
        }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.Restrict;

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Context).Assembly);

            base.OnModelCreating(modelBuilder);
        }



        public DbSet<tbUser> tbUser { get; set; }
    }
}
