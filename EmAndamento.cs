using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trabalho.Models
{
    public class EmAndamento
    {
    public int Id { get; set; }
    
     public int TarefaId { get; set; }
    public Tarefa Tarefa { get; set; }
    }
}
