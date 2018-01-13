namespace cimob.Models
{
    public class CandidaturaDocumentos
    {
        public int CandidaturaDocumentosID { get; set; }
        public int CandidaturaID { get; set; }
        public int DocumentoID { get; set; }

        public virtual Candidatura Candidatura { get; set; }
        public virtual Documento Documento { get; set; }
    }
}
