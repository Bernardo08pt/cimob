using BackOffice.Data;
using BackOffice.Models;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;

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
        /// Evento de duplo clique numa linha da tabela.
        /// Abre um form para editar o user respetivo
        /// </summary>
        /// <param name="sender">Variável que contém o objeto eviado pelo evento.</param>
        /// <param name="e">Argumentos do evento acionado pelo duplo clique.</param>
        private void EditRow(object sender, MouseButtonEventArgs e)
        {
            ApplicationUser user = grdUser.SelectedItem as ApplicationUser;

            EditarContaUtilizadorDialog dlg = new EditarContaUtilizadorDialog(usersList, new ApplicationUser(user)) { Title = "Editar Empresa" };

            if (dlg.ShowDialog() == true && dlg.User != user)
            {
                user.Numero = dlg.User.Numero;
                user.Nome = dlg.User.Nome;
                user.Email = dlg.User.Email;
                user.DataNascimento = dlg.User.DataNascimento;

                Users.UpdateUser(user);
            }
        }
    }
}
