namespace cimob.Models
{
    /// <summary>
    /// Corresponde à tabela TipoMobilidade na BD
    /// </summary>
    public class TipoMobilidade
    {
        public int TipoMobilidadeID { get; set; }
        public string Descricao { get; set; }
        public int Estagio { get; set; }
    }
}