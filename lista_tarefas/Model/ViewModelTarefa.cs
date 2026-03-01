namespace Lista_de_tarefas.Model
{
    public class ViewModelTarefa
    {
        public string Nome { get; set; }
        public DateOnly DataConclusao { get; set; }
        public bool Concluida { get; set; }
        public DateOnly DataCriacao { get; set; }
        public string Mensagem { get; set; }


        public ViewModelTarefa(string nome, DateOnly dataConclusao, bool concluida, DateOnly dataCriacao, string mensagem)
        {
            Nome = nome;
            DataConclusao = dataConclusao;
            Concluida = concluida;
            DataCriacao = dataCriacao;
            Mensagem = mensagem;
        }
    }
}
