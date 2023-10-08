namespace Trabalho.Models
{
    public class Tarefa
    {
        public int Id { get; set; }
        public string Nome { get; set; } = "";
        public string Descricao { get; set; } = "";
        public DateTime Prazo { get; set; }
        
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }

        // Outras propriedades ou métodos relevantes para Tarefa podem ser adicionados aqui.
    }
}
