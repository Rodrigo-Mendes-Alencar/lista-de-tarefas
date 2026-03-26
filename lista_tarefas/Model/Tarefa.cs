using System;
namespace Lista_de_tarefas.Model
{
    public class Tarefa
    {
        public int Id { get; private set; }
        public string Nome { get;  set; }
        public DateTime? DataConclusao { get; set; } 
        public DateTime DataEstimada { get;  set; }
        public bool Concluida { get;  set; }
        public DateTime DataCriacao { get;set; }
        public string Mensagem { get; set; }


        public Tarefa(string nome, DateTime dataEstimada, bool concluida, DateTime dataCriacao, string mensagem)
        {
            Nome = nome;
            DataEstimada = dataEstimada;
            Concluida = concluida;
            DataCriacao = dataCriacao;
            Mensagem = mensagem;
            DataConclusao = null;
        }
        public Tarefa()
        {

        }

    }
}

