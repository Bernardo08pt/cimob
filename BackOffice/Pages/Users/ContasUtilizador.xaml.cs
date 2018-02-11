using BackOffice.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BackOffice.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Net.Mail;
using System.Net;

namespace BackOffice
{
    /// <summary>
    /// Página com toda a lógica inerente à visualização, criação, edição e remoção de contas de utilizador.
    /// </summary>
    public partial class ContasUtilizador : Page
    {
        private ObservableCollection<ApplicationUser> usersList;
        private ObservableCollection<Role> rolesList;
        
        /// <summary>
        /// Construtor da página.
        /// Contém o código para recolher os dados dos utilizadores da base de dados, e guarda-os numa lista.
        /// Atribui esta lista a uma tabela.
        /// </summary>
        public ContasUtilizador()
        {
            InitializeComponent();
            usersList = App.Users.GetUsers();
            rolesList = App.Roles.GetRoles();
            grdUser.ItemsSource = usersList;
        }

        /// <summary>
        /// Evento do botão adicionar
        /// Abre um form para adicionar uma conta
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddRow(object sender, RoutedEventArgs e)
        {
          

            EditarContaUtilizadorDialog dlg = new EditarContaUtilizadorDialog(rolesList, usersList) { Title = "Adicionar Conta de Utilizador" };

            if (dlg.ShowDialog() == true)
            {
                dlg.User.UserName = dlg.User.Email;
                dlg.User.NormalizedEmail = dlg.User.Email.Normalize();
                dlg.User.NormalizedUserName = dlg.User.Email.Normalize();
                dlg.User.SecurityStamp = Guid.NewGuid().ToString();
                string password = CreatePassword(8);

                dlg.User.PasswordHash = HashPassword(password);
                dlg.User.EmailConfirmed = true;
                if (App.Users.InsertUser(dlg.User))
                {
                    usersList.Add(dlg.User);
                    
                    string to = dlg.User.Email;
                    string from = "cimob@no-reply.com";
                    string subject = "Nova conta de utilizador";
                    string body = "<p><span style='font-size: 18px;'>Caro(a) " + dlg.User.Nome + ",<strong> </strong></span></p>" +
                        "<p><span style='font-size: 18px;'> Foi criada uma conta de utilizador na plataforma de candidaturas CIMOB para si. " +
                        "Clique <a href='cimob.azurewebsites.net'>aqui</a> para aceder ao site e efetuar o login.&nbsp;</span></p>" +
                        "<p  style='font-size: 18px;'> As suas credenciais são Email: " + dlg.User.Email + " Password: " + password + 
                        "<p><span style='font-size: 18px;'> Boa sorte no processo de candidaturas.&nbsp;</span></p>" +
                        "<p><br></p>" +
                        "<p><span style = 'font-size: 18px;'> Melhores cumprimentos Equipa CIMOB - IPS </span></p>" +
                        "<p><span style = 'font-size: 14px;'> Nota: este e-mail foi gerado automaticamente, pelo que n&atilde;o deve responder pois quaisquer respostas n&atilde;o ser&atilde;o vistas.</span></p>" +
                         "<span style = 'font-size: 12px;'> &nbsp;</span></p>";
                    MailMessage message = new MailMessage(from, to, subject, body);
                    message.IsBodyHtml = true;
                    SmtpClient client = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential("cimobadm@gmail.com", "cimobadm1")
                    };
                    Console.WriteLine("Changing time out from {0} to 100.", client.Timeout);


                    try
                    {
                        client.Send(message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Exception caught in CreateTimeoutTestMessage(): {0}",
                              ex.ToString());
                    }
                }
            }
        }

        /// <summary>
        /// Evento de duplo click na linha da tabela
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            EditRow();
        }

        /// <summary>
        /// Evento de click no botão editar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            EditRow();
        }

        /// <summary>
        /// Abre um form para editar o user respetivo
        /// </summary>
        private void EditRow()
        {
            ApplicationUser user = grdUser.SelectedItem as ApplicationUser;

            EditarContaUtilizadorDialog dlg = new EditarContaUtilizadorDialog(rolesList, usersList, new ApplicationUser(user)) { Title = "Editar Conta de Utilizador" };

            if (dlg.ShowDialog() == true)
            {
                if (dlg.User != user)
                {
                    user.UserName = dlg.User.Email;
                    user.NormalizedEmail = dlg.User.Email.Normalize();
                    user.NormalizedUserName = dlg.User.Email.Normalize();
                    user.Numero = dlg.User.Numero;
                    user.Nome = dlg.User.Nome;
                    user.Email = dlg.User.Email;
                    user.DataNascimento = dlg.User.DataNascimento;
                    user.RoleId = dlg.User.RoleId;
                    user.RoleName = dlg.User.RoleName;

                    App.Users.UpdateUser(user);
                }
            }
        }

        /// <summary>
        /// Evento do botão remover
        /// Abre um form para remover uma conta
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveRow(object sender, RoutedEventArgs e)
        {
            if (grdUser.SelectedItem == null)
                return;

            ApplicationUser user = grdUser.SelectedItem as ApplicationUser;

            if (MessageBox.Show("Deseseja mesmo apagar a conta de utilizador (Y/N)?", "Apagar Conta de Utilizador?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                App.Users.RemoverUser(user.Id);
                usersList.Remove(user);
            }
        }

        /// <summary>
        /// Cria uma password Random
        /// </summary>
        /// <param name="length">Tamanho da password</param>
        /// <returns>String com a password</returns>
        public string CreatePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890.-_";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }

        /// <summary>
        /// Cria um hash para uma password
        /// </summary>
        /// <param name="password">Password para encriptar</param>
        /// <returns>Passord encriptada</returns>
        public static string HashPassword(string password)
        {
            byte[] salt;
            byte[] buffer2;
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }
    }
}