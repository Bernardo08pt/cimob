using BackOffice.Models;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BackOffice.Pages.Graphs
{
    /// <summary>
    /// Interaction logic for GraficoCandidaturasIPSEscolas.xaml
    /// </summary>
    public partial class GraficoCandidaturasIPSEscolas : UserControl
    {
        public IEnumerable<EstatisticaCandidaturaIPSEscola> listaEstatisticas;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="estatisticas">Lista de estatisticas das candidaturas por escola do ips existentes na base de dados.</param>
        public GraficoCandidaturasIPSEscolas()
        {
            InitializeComponent();
            listaEstatisticas = App.Estatisticas.GetCandidaturaIPSEscola();

            SeriesCollection = new SeriesCollection();

            ChartValues<int> cv = new ChartValues<int>();
            List<string> escolas = new List<string>();
            string anoLetivo = listaEstatisticas.First().Ano;

            string escola = listaEstatisticas.First().Nome;
            escolas.Add(escola);

            foreach (EstatisticaCandidaturaIPSEscola estatistica in listaEstatisticas)
            {
                if(escola != estatistica.Nome)
                {
                    escolas.Add(estatistica.Nome);
                    escola = estatistica.Nome;
                }

                if(anoLetivo != estatistica.Ano)
                {
                    SeriesCollection.Add(
                        new ColumnSeries
                        {
                            Title = anoLetivo,
                            Values = cv,
                            DataLabels = true
                    });
                   
                    cv = new ChartValues<int>();
                    anoLetivo = estatistica.Ano;
                }

                cv.Add(estatistica.Contagem);
            }

            SeriesCollection.Add(
                new ColumnSeries
                {
                    Title = anoLetivo,
                    Values = cv,
                    DataLabels = true
                }
            );

            Labels = escolas.ToArray();
            Formatter = value => value.ToString();

            DataContext = this;
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<int, string> Formatter { get; set; }
    }
}
