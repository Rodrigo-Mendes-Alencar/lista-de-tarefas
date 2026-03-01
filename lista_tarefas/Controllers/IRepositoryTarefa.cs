using System;
namespace Lista_de_tarefas.Controllers
{
	public interface IRepositoryTarefa
	{
		public void AdicionarTarefa(Tarefa tarefa);
		public void AtualizarTarefa(Tarefa tarefa);
		public void ExcluirTarefa(int id);
		public IEnumerable<Tarefa> LIstarTarefas();
		public void listarTarefasPendentes();
		public void listarTarefasVencidas();
    }
}

