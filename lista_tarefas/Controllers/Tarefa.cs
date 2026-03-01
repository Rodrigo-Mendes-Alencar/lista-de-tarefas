using System;
namespace Lista_de_tarefas.Controllers
{
    public class Tarefa
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public DateTime DataConclusao { get; private set; }
        public bool Concluida { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public string Mensagem { get; private set; }

        public Tarefa(string nome, DateTime dataConclusao, bool concluida, DateTime dataCriacao, string mensagem)
        {
            Nome = nome;
            DataConclusao = dataConclusao;
            Concluida = concluida;
            DataCriacao = dataCriacao;
            Mensagem = mensagem;
        }
        public Tarefa()
        {

        }

    }
}

