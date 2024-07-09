using Journey.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Journey.Infrastructure;

public class JourneyDbContext : DbContext
{
    //Passamos a entidade TRIP para dbset pois ela tem referencia com o contexto real do BANCO
    //trips é o nome da tabela no banco
    public DbSet<Trip> Trips { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source= C:\\Users\\USER\\Documents\\projetos\\BDSQLlite\\JourneyDatabase.db");
    }

    // usado quando não quero ter um link direto com a entidade, no caso abaixo
    //ela será usada atraves da Trips
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Activity>().ToTable("Activities");
    }
}
