using BackOffice.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Microsoft.AspNetCore.Identity;

namespace BackOffice
{
    /// <summary>
    /// Classe com a lógica inerente à janela de login da aplicação
    /// </summary>
    public partial class Login : Window
    {
        /// <summary>
        /// Construtor da classe login
        /// </summary>
        public Login()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Evento de quando se clica no botão ok
        /// Verifica se os dados de login estão corretos e abre a main window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            
            if (TextBoxEmail.Text == "")
            {
                MessageBox.Show("Insira o nome de utilizador.", "Nome de utilizador em falta", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if(TextBoxPassword.Password == "")
            {
                MessageBox.Show("Insira a password.", "Password em falta", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            IEnumerable<ApplicationUser> users = App.Users.GetUsers();

            users = users.Select(u => u).Where(u => u.Email == TextBoxEmail.Text && u.RoleName == "Administrador");

            ApplicationUser user = null;
            if (users.Any())
                user = users.First();

            if(user == null)
            {
                MessageBox.Show("Não existe uma conta de utilizador com este email.", "Falha de login", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var p = new PasswordHasher<ApplicationUser>();
            PasswordVerificationResult result = p.VerifyHashedPassword(user, user.PasswordHash, TextBoxPassword.Password);

            if(result == Microsoft.AspNetCore.Identity.PasswordVerificationResult.Success)
            {
                MainWindow window = new MainWindow();
                window.Show();

                Close();
            } else
            {
                MessageBox.Show("Dados incorretos.", "Falha de Login", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

        }

       
    }
}
