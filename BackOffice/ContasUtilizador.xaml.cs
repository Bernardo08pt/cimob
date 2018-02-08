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

using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using BackOffice.Models;
using BackOffice.Data;

namespace BackOffice
{
    /// <summary>
    /// Página com toda a lógica inerente à visualização, criação, edição e remoção de contas de utilizador.
    /// </summary>
    public partial class ContasUtilizador : Page
    {

        private static BDUsers users = new BDUsers();
        public static BDUsers Users { get => users; }
        private ObservableCollection<ApplicationUser> usersList;

        /// <summary>
        /// Construtor da página.
        /// Contém o código para recolher os dados dos utilizadores da base de dados, e guarda-os numa lista.
        /// Atribui esta lista a uma tabela.
        /// </summary>
        public ContasUtilizador()
        {
            InitializeComponent();
            usersList = Users.GetUsers();
            grdUser.ItemsSource = usersList;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditRow(object sender, MouseButtonEventArgs e)
        {
            var obj = grdUser.SelectedItem as ApplicationUser;
        }
    }
}
