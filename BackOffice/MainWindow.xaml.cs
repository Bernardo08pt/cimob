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

        private void AbrirMenu_Click(object sender, RoutedEventArgs e)
        {
            (sender as Button).ContextMenu.IsEnabled = true;
            (sender as Button).ContextMenu.PlacementTarget = (sender as Button);
            (sender as Button).ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
            (sender as Button).ContextMenu.IsOpen = true;
        }

        private void MenuItemEstatisticas_Click(object sender, RoutedEventArgs e)
        {
            FrameCentral.Source = new Uri("Pages/Graphs/VisualizarGraficos.xaml", UriKind.Relative);
            FrameCentral.HorizontalAlignment = HorizontalAlignment.Stretch;
        }
        private void MenuItemContas_Click(object sender, RoutedEventArgs e)
        {
            FrameCentral.Source = new Uri("Pages/Users/ContasUtilizador.xaml", UriKind.Relative);
            FrameCentral.HorizontalAlignment = HorizontalAlignment.Center;
        }

    }
}
