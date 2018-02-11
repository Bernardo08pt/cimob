using System;
using System.Windows;
using System.Windows.Controls;

namespace BackOffice
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Evenvo para abrir o menu 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AbrirMenu_Click(object sender, RoutedEventArgs e)
        {
            (sender as Button).ContextMenu.IsEnabled = true;
            (sender as Button).ContextMenu.PlacementTarget = (sender as Button);
            (sender as Button).ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
            (sender as Button).ContextMenu.IsOpen = true;
        }

        /// <summary>
        /// Evenvo para abrir a página de Visualizar Graficos no frame 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemEstatisticas_Click(object sender, RoutedEventArgs e)
        {
            FrameCentral.Source = new Uri("Pages/Graphs/VisualizarGraficos.xaml", UriKind.Relative);
            FrameCentral.HorizontalAlignment = HorizontalAlignment.Stretch;
        }

        /// <summary>
        /// Evenvo para abrir a página de Contas Utilizador no frame 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemContas_Click(object sender, RoutedEventArgs e)
        {
            FrameCentral.Source = new Uri("Pages/Users/ContasUtilizador.xaml", UriKind.Relative);
            FrameCentral.HorizontalAlignment = HorizontalAlignment.Center;
        }

    }
}
