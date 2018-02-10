namespace cimob.Models
{
    /// <summary>
    /// Corresponde à tabela Erros na BD
    /// </summary>
    public class Erro
    {
        public int ErroID { get; set; }
        public string Nome { get; set; }
        public string Mensagem { get; set; }
        public int Codigo { get; set; }
    }
}
