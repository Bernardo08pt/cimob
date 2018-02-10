using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Models
{
    /// <summary>
    /// Classe que representa as estatisticas do programa de mobilidade    
    /// </summary>
    public class EstatisticaProgramaMobilidade : INotifyPropertyChanged
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
        /// Programa de mobilidade
        /// </summary>
        private string programa;
        public string Programa
        {
            get { return programa; }
            set
            {
                programa = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Programa"));
            }
        }

        /// <summary>
        /// Contagem de pedidos feitos a um programa de mobilidade
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
        /// Construtor que recebe uma estatistica de um programa de mobilidade
        /// </summary>
        /// <param name="estatistica">Uma estatisca do tipo EstatisticaProgramaMobilidade</param>
        public EstatisticaProgramaMobilidade(EstatisticaProgramaMobilidade estatistica)
        {
            this.Programa = estatistica.Programa;
            this.Contagem = estatistica.Contagem;
        }

        /// <summary>
        /// Construtor em branco
        /// </summary>
        public EstatisticaProgramaMobilidade() { }
    }
}
