using System;
namespace Lista_de_tarefas.Model
{
    public class Tarefa
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public DateOnly DataConclusao { get; private set; }
        public bool Concluida { get; private set; }
        public DateOnly DataCriacao { get; private set; }
        public string Mensagem { get; private set; }

        public Tarefa(string nome, DateOnly dataConclusao, bool concluida, DateOnly dataCriacao, string mensagem)
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

