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

        /// <summary>
        /// Construtor este cria o gráfico com os dados retirados da base de dados
        /// </summary>
        /// <param name="estatisticas">Lista de estatisticas das escolas parceiras existentes na base de dados.</param>
        public GraficoEscolasParceiras()
        {
            InitializeComponent();
            listaEstatisticas = App.Estatisticas.GetEscolasParceiras();

            foreach (EstatisticaEscolaParceira estatistica in listaEstatisticas)
            {
                PieSeries p = new PieSeries();

                EscolasParceiras.Series.Add(
                    new PieSeries
                    {
                        Title = estatistica.Nome,
                        Values = new ChartValues<int> { estatistica.Contagem },
                        DataLabels = true,
                        LabelPoint = chartPoint => string.Format("{0}", chartPoint.Y)
                    });
            }

           DataContext = this;
        }

        public Func<ChartPoint, string> PointLabel { get; set; }
    }
}
