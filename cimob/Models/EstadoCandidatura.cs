namespace cimob.Models
{
    /// <summary>
    /// Corresponde à tabela EstadoCandidatura na BD
    /// </summary>
    public class EstadoCandidatura
    {
        public int EstadoCandidaturaID { get; set; }
        public string Descricao { get; set; }
        public string Cor { get; set; }
        public string Icon { get; set; }
    }
}