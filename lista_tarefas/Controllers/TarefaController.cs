using Lista_de_tarefas.Data;
using Lista_de_tarefas.Model;
using Microsoft.AspNetCore.Mvc;

namespace Lista_de_tarefas.Controllers
{
    [ApiController]
    [Route("Api/Tarefas")]
    public class TarefaController : ControllerBase
    {
        private readonly AppDbContext _context;
        public TarefaController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult AdicionarTarefa(ViewModelTarefa viewtarefa)
        {
            if (viewtarefa.MensagemConclusao == null || viewtarefa.MensagemConclusao == "string")
            {
                viewtarefa.MensagemConclusao = "Tarefa finalizada com sucesso!";
            }
            if (viewtarefa.Nome == null || viewtarefa.Nome == "string")
            {
                return BadRequest("O nome da tarefa é obrigatório.");
            }
            if (viewtarefa.DataEstimada <= DateTime.Now)
            {
                return BadRequest("A data estimada deve ser maior do que data atual.");
            }
            var tarefa = new Tarefa(viewtarefa.Nome, viewtarefa.DataEstimada, false, DateTime.Now, viewtarefa.MensagemConclusao);

            _context.Tarefas.Add(tarefa);
            _context.SaveChanges();
            return Ok(tarefa);
        }
        [HttpPut("Concluir/{id}")]
        public IActionResult ConcluirTarefa(int id)
        {
            var tarefa = _context.Tarefas.Find(id);
            if (tarefa == null)
            {
                return NotFound();
            }
            else if (tarefa.Concluida == true)
            {
                return BadRequest("A tarefa já foi concluída.");
            }
            else if (tarefa.DataEstimada < DateTime.Now)
            {
                return BadRequest("A tarefa está atrasada, não é possível concluir.");
            }
            tarefa.Concluida = true;
            tarefa.DataConclusao = DateTime.Now;
            _context.SaveChanges();
            return Ok(tarefa.Mensagem);
        }

        [HttpPut("Editar/{id}")]
        public IActionResult AtualizarTarefa(int id, [FromBody] ViewModelTarefa viewtarefa)
        {
            var tarefa = _context.Tarefas.Find(id);
            if (tarefa == null)
            {
                return NotFound();
            }
            if (viewtarefa.MensagemConclusao == null || viewtarefa.MensagemConclusao == "string")
            {
                viewtarefa.MensagemConclusao = "Tarefa finalizada com sucesso!";
            }
            if (viewtarefa.Nome == null || viewtarefa.Nome == "string")
            {
                return BadRequest("O nome da tarefa é obrigatório.");
            }
            if (viewtarefa.DataEstimada <= DateTime.Now)
            {
                return BadRequest("A data estimada deve ser maior do que data atual.");
            }
            tarefa.Nome = viewtarefa.Nome;
            tarefa.DataEstimada = viewtarefa.DataEstimada;
            tarefa.Mensagem = viewtarefa.MensagemConclusao;
            _context.SaveChanges();
            return Ok(tarefa);
        }

        [HttpGet("Todas")]
        public IActionResult ListarTarefas()
        {
            var tarefas = _context.Tarefas.ToList();
            return Ok(tarefas);
        }

        [HttpGet("Pendentes/NaoVencida")]
        public IActionResult ListarTarefasPendentes()
        {
            var pendente = _context.Tarefas.Where(t => t.Concluida == false);
            var noPrazo = pendente.Where(t => t.DataEstimada >= DateTime.Now).ToList();
            return Ok(noPrazo);
        }

        [HttpGet("Atrasadas/Pendente")]
        public IActionResult ListarTarefasAtrasadas()
        {
            var pendente = _context.Tarefas.Where(t => t.Concluida == false);
            var atrasada = pendente.Where(t => t.DataEstimada < DateTime.Now).ToList();
            return Ok(atrasada);
        }

        [HttpGet("Concluidas")]
        public IActionResult ListarTarefasConcluidas()
        {
            var concluida = _context.Tarefas.Where(t => t.Concluida == true).ToList();
            return Ok(concluida);
        }

        [HttpDelete("Excluir/{id}")]
        public IActionResult Delete(int id) 
        {
            if (_context.Tarefas.Find(id) == null)
            {
                return NotFound();
            }
            _context.Tarefas.Remove(_context.Tarefas.Find(id));
            _context.SaveChanges();    
            return Ok();
        }


    }
}   
