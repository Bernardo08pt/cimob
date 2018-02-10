using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using BackOffice.Models;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace BackOffice.Pages.Graphs
{
    /// <summary>
    /// Interaction logic for GraficoProgramasMobilidade.xaml
    /// </summary>
    public partial class GraficoProgramasMobilidade 
    {
        public IEnumerable<EstatisticaProgramaMobilidade> listaEstatisticas;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="estatisticas">Lista de estatisticas dos programas de mobilidade existentes na base de dados.</param>
        public GraficoProgramasMobilidade()
        {
            InitializeComponent();
            listaEstatisticas = App.Estatisticas.GetProgramasMobilidade();

            SeriesCollection = new SeriesCollection();

            foreach (EstatisticaProgramaMobilidade estatistica in listaEstatisticas)
            {
                SeriesCollection.Add(
                    new PieSeries
                    {
                        Title = estatistica.Programa,
                        Values = new ChartValues<int> { estatistica.Contagem },
                        DataLabels = true
                        });
            }

            DataContext = this;
        }

        public SeriesCollection SeriesCollection { get; set; }
    }
}
