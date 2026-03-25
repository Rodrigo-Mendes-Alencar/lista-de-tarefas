using Lista_de_tarefas.Data;
using Lista_de_tarefas.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> AdicionarTarefaAsync(ViewModelTarefa viewtarefa)
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
            await _context.SaveChangesAsync();
            return Ok(tarefa);
        }
        [HttpPut("Concluir/{id}")]
        public async Task<IActionResult> ConcluirTarefaAsync(int id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);
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
            await _context.SaveChangesAsync();
            return Ok(tarefa.Mensagem);
        }

        [HttpPut("Editar/{id}")]
        public async Task<IActionResult> AtualizarTarefaAsync(int id, [FromBody] ViewModelTarefa viewtarefa)
        {
            var tarefa =  await _context.Tarefas.FindAsync(id);
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
             await _context.SaveChangesAsync();
            return Ok(tarefa);
        }

        [HttpGet("Todas")]
        public async Task<IActionResult> ListarTarefasAsync()
        {
            var tarefas = await _context.Tarefas.ToListAsync();
            return Ok(tarefas);
        }

        [HttpGet("Pendentes/NaoVencida")]
        public async Task<IActionResult> ListarTarefasPendentesAsync()
        {
            var pendente = _context.Tarefas.Where(t => t.Concluida == false);
            var noPrazo =  await pendente.Where(t => t.DataEstimada >= DateTime.Now).ToListAsync();
            return Ok(noPrazo);
        }

        [HttpGet("Atrasadas/Pendente")]
        public async Task<IActionResult> ListarTarefasAtrasadasAsync()
        {
            var pendente = _context.Tarefas.Where(t => t.Concluida == false);
            var atrasada = await pendente.Where(t => t.DataEstimada < DateTime.Now).ToListAsync();
            return Ok(atrasada);
        }

        [HttpGet("Concluidas")]
        public async Task<IActionResult> ListarTarefasConcluidasAsync()
        {
            var concluida = await _context.Tarefas.Where(t => t.Concluida == true).ToListAsync();
            return Ok(concluida);
        }

        [HttpDelete("Excluir/{id}")]
        public async Task<IActionResult> DeleteAsync(int id) 
        {
            if ( await _context.Tarefas.FindAsync(id) == null)
            {
                return NotFound();
            }
            _context.Tarefas.Remove(_context.Tarefas.Find(id));
            await _context.SaveChangesAsync();    
            return Ok();
        }


    }
}   
