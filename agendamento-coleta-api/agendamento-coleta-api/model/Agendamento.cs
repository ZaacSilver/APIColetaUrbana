namespace agendamento_coleta_api.model
{
    public class Agendamento
    {
        public long Id { get; set; }
        public string Observacoes { get; set; }
        public string Localizacao { get; set; }
        public long RaioLocalizacao { get; set; }
    }
}
