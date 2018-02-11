using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Models
{
    /// <summary>
    /// Classe que representa as estatisticas das candidaturas feitas por escolas do ips
    /// </summary>
    public class EstatisticaCandidaturaIPSEscola : INotifyPropertyChanged
    {
        /// <summary>
        /// Notificador de mudança dos atributos da classe
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Nome da escola ips
        /// </summary>
        private string nome;
        public string Nome
        {
            get { return nome; }
            set
            {
                nome = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Nome"));
            }
        }

        /// <summary>
        /// Contagem de pedidos para outgoing por escola ips
        /// </summary>
        private int contagem;
        public int Contagem
        {
            get { return contagem; }
            set
            {
                contagem = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Contagem"));
            }
        }

        /// <summary>
        /// Ano Letivo
        /// </summary>
        private string ano;
        public string Ano
        {
            get { return ano; }
            set
            {
                ano = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Ano"));
            }
        }

        /// <summary>
        /// Construtor que recebe uma estatistica das candidaturas feitas por escolas do ips
        /// </summary>
        /// <param name="estatistica">Uma estatisca do tipo EstatisticaCandidaturaIPSEscola</param>
        public EstatisticaCandidaturaIPSEscola(EstatisticaCandidaturaIPSEscola estatistica)
        {
            this.Nome = estatistica.Nome;
            this.Contagem = estatistica.Contagem;
            this.ano = estatistica.Ano;
        }

        /// <summary>
        /// Construtor em branco
        /// </summary>
        public EstatisticaCandidaturaIPSEscola() { }

    }
}
