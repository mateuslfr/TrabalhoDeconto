using Trabalho.Models;
using Microsoft.EntityFrameworkCore;


namespace Trabalho.Data
{
    public class AppDataContext : DbContext
    {
          public AppDataContext(DbContextOptions<AppDataContext> options) : base(options)
    {

    }

    //Classes que vão se tornar tabelas no banco de dados
    public DbSet<Tarefa> Tarefas { get; set; } = null!;
    public DbSet<Categoria> Categorias { get; set; } = null!;
    //public DbSet<EmAndamento> EmAndamentos { get; set; }= null!;
    //public DbSet<Concluida> Concluidas { get; set; } = null!;


       protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    optionsBuilder.UseSqlite("DataSource=tarefa.db");
}

    }
}