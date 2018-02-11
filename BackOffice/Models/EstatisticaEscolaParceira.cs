using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Models
{
    /// <summary>
    /// Classe que representa as estatisticas da escola parceira
    /// </summary>
    public class EstatisticaEscolaParceira : INotifyPropertyChanged
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
        /// Nome da escola parceira
        /// </summary>
        private string nome;
        public string Nome {
            get { return nome; }
            set
            {
                nome = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Nome"));
            }
        }

        /// <summary>
        /// Contagem de pedidos para outgoing
        /// </summary>
        private int contagem;
        public int Contagem {
            get { return contagem; }
            set
            {
                contagem = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Contagem"));
            }
        }

        /// <summary>
        /// Construtor que recebe uma estatistica de uma escola parceira
        /// </summary>
        /// <param name="estatistica">Uma estatisca do tipo EstatisticaEscolaParceira</param>
        public EstatisticaEscolaParceira(EstatisticaEscolaParceira estatistica)
        {
            this.Nome = estatistica.Nome;
            this.Contagem = estatistica.Contagem;
        }

        /// <summary>
        /// Construtor em branco
        /// </summary>
        public EstatisticaEscolaParceira() { }

    }
}
