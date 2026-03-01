using Lista_de_tarefas.Data;
using Lista_de_tarefas.Model;
using Microsoft.AspNetCore.Mvc;

namespace Lista_de_tarefas.Controllers
{
    [ApiController]
    [Route("[Api/Tarefas]")]
    public class TarefaController : Controller
    {
        
        private readonly AppDbContext _context;
        public TarefaController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult AdicionarTarefa()
        {
            var tarefa = new ViewModelTarefa(ViewModelTarefa.Nome, ViewModelTarefa.DataConclusao, ViewModelTarefa.Concluida, ViewModelTarefa.DataCriacao, ViewModelTarefa.Mensagem);
        }
    }
}
