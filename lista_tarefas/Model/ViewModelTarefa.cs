namespace Lista_de_tarefas.Model
{
    public class ViewModelTarefa
    {
        public string Nome { get; set; }
        public DateTime DataEstimada{ get; set; }
        public string? MensagemConclusao { get; set; } = null;
    }
}
