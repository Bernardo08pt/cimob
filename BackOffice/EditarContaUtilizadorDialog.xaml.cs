using BackOffice.Models;
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
using System.Windows.Shapes;

namespace BackOffice
{
    /// <summary>
    /// Interaction logic for EditarContaUtilizadorDialog.xaml
    /// </summary>
    public partial class EditarContaUtilizadorDialog : Window
    {
        public ApplicationUser User { get; set; }
        private IEnumerable<ApplicationUser> listaUsers;

        public EditarContaUtilizadorDialog(IEnumerable<ApplicationUser> users, ApplicationUser user = null)
        {
            InitializeComponent();
            listaUsers = users;

            User = user ?? new ApplicationUser();

            this.DataContext = User;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            //if (AtualizarEmpresa())
                DialogResult = true;
        }

        //private bool AtualizarEmpresa()
        //{
        //    if (listaEmpresas.Any(empresa => empresa.Sigla == Empresa.Sigla && empresa.EmpresaId != Empresa.EmpresaId))
        //    {
        //        MessageBox.Show("Sigla Inválida. Já existente para outra empresa.", "Sigla Inválida", MessageBoxButton.OK, MessageBoxImage.Error);
        //        return false;
        //    }
        //    return true;
        //}

        //private void CancelButton_Click(object sender, RoutedEventArgs e)
        //{
        //}
        ///*

        //// Nível 5
        //private void TextBoxSigla_OnError(object sender, ValidationErrorEventArgs e)
        //{
        //    if (e.Action == ValidationErrorEventAction.Added)
        //        MessageBox.Show(e.Error.ErrorContent.ToString());
        //}
        //*/
        //// Nível 5
        //private void EditarEmpresaDialog_OnClosing(object sender, CancelEventArgs e)
        //{
        //    // manually fire the bindings so we get the validation initially
        //    TextBoxSigla.GetBindingExpression(TextBox.TextProperty).UpdateSource();
        //    string message;

        //    if (this.DialogResult == true && FormHasErrors(out message))
        //    {
        //        // Errors still exist.
        //        MessageBox.Show(message);

        //        e.Cancel = true;
        //    }
        //}
        ///*
        //// Nível 5
        //private bool FormHasErrors(out string message)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    GetErrors(sb, GridFormEmpresa);
        //    message = sb.ToString();
        //    return message != "";
        //}

        //// Nível 5
        //private void GetErrors(StringBuilder sb, DependencyObject obj)
        //{
        //    foreach (object child in LogicalTreeHelper.GetChildren(obj))
        //    {
        //        TextBox element = child as TextBox;
        //        if (element == null) continue;
        //        if (Validation.GetHasError(element))
        //        {
        //            sb.Append(element.Text + " has errors:\r\n");
        //            foreach (ValidationError error in Validation.GetErrors(element))
        //            {
        //                sb.Append(" " + error.ErrorContent.ToString());
        //                sb.Append("\r\n");
        //            }
        //        }

        //        // Check the children of this object for errors.
        //        GetErrors(sb, element);
        //    }
        //}
        //*/
    }
}
