namespace cimob.Models
{
    /// <summary>
    /// Corresponde à tabela Ajuda na BD
    /// </summary>
    public class Ajuda
    {
        public int AjudaID { get; set; }
        public string Pagina { get; set; }
        public string Nome { get; set; }
        public string Titulo { get; set; }
        public string Corpo { get; set; }
    }
}
