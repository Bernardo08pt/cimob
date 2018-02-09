using System;
using System.Collections.Generic;
using System.Windows.Controls;
using BackOffice.Models;
using LiveCharts;
using LiveCharts.Wpf;

namespace BackOffice
{
    /// <summary>
    /// Interaction logic for GraficoEscolasParceiras.xaml
    /// </summary>
    public partial class GraficoEscolasParceiras : UserControl
    {
        public IEnumerable<EstatisticaEscolaParceira> listaEstatisticas;

        public GraficoEscolasParceiras(IEnumerable<EstatisticaEscolaParceira> estatisticas)
        {
            InitializeComponent();
            listaEstatisticas = estatisticas;

            PointLabel = chartPoint =>
                string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);

            DataContext = this;
        }

        public Func<ChartPoint, string> PointLabel { get; set; }

        private void Chart_OnDataClick(object sender, ChartPoint chartpoint)
        {
            var chart = (LiveCharts.Wpf.PieChart)chartpoint.ChartView;

            //clear selected slice.
            foreach (PieSeries series in chart.Series)
                series.PushOut = 0;

            var selectedSeries = (PieSeries)chartpoint.SeriesView;
            selectedSeries.PushOut = 8;
        }
    }
}
