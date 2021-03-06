﻿using BackOffice.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace BackOffice
{
    /// <summary>
    /// Lógica de interação para o diálogo editar utilizador
    /// </summary>
    public partial class EditarContaUtilizadorDialog : Window
    {

        public ApplicationUser User { get; set; }
        private IEnumerable<ApplicationUser> listaUsers;
        private IEnumerable<Role> listaRoles;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="users">Lista de utilizadores existentes na base de dados.</param>
        /// <param name="user">Utilizador que se pretende editar. Null se for para adicionar.</param>
        public EditarContaUtilizadorDialog(IEnumerable<Role> roles, IEnumerable<ApplicationUser> users, ApplicationUser user = null)
        {
            InitializeComponent();
            listaUsers = users;
            listaRoles = roles;

            User = user ?? new ApplicationUser();

            if(User.Nome == null)
            {
                User.DataNascimento = DateTime.Now.AddYears(-16);
            }



            DataContext = User;
            ComboBoxRole.ItemsSource = listaRoles;
            ComboBoxRole.DisplayMemberPath = "Name";
            ComboBoxRole.SelectedValuePath = "Id";
        }

        /// <summary>
        /// Evento ao clicar no botão ok do form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (VerificarErros())
                DialogResult = true;
        }

        /// <summary>
        /// Verifica se os campos do form estão bem preenchidos.
        /// </summary>
        /// <returns>True se não existirem erros, False se existirem</returns>
        private bool VerificarErros()
        {
            //Número
            try
            {
                Convert.ToInt32(User.Numero);
            } catch(Exception e) { 
                MessageBox.Show("Insira um número válido.", "Número Inválido", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (listaUsers.Any(user => user.Numero == User.Numero && user.Id != User.Id))
            {
                MessageBox.Show("Já existente para outro utilizador.", "Número Inválido", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            //Nome
            if (User.Nome == "")
            {
                MessageBox.Show("O campo nome não pode estar vazio.", "Nome Inválido", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            //Email
            if (User.Email == "")
            {
                MessageBox.Show("O campo nome não pode estar vazio.", "Nome Inválido", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            try
            {
                new MailAddress(User.Email);
            }
            catch (Exception e)
            {
                MessageBox.Show("Insira um email válido.", "Email Inválido", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (listaUsers.Any(user => user.Email == User.Email && user.Id != User.Id))
            {
                MessageBox.Show("Já existente para outro utilizador.", "Email Inválido", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
     
            //Data de Nascimento
            if (User.DataNascimento == null)
            {
                MessageBox.Show("O campo data de nascimento não pode estar vazio.", "Data de nascimento Inválida", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            try
            {
              Convert.ToDateTime(User.DataNascimento);

                if (User.DataNascimento < DateTime.Now.AddYears(-100) || User.DataNascimento > DateTime.Now.AddYears(-16))
                {
                    MessageBox.Show(string.Format("O utilizador deve ter entre 17 a 100 anos."));
                    return false;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(string.Format("A data é inválida"));
                return false;
            }

            //Data de Nascimento
            if (User.RoleId == null)
            {
                MessageBox.Show("O campo tipo de utilizador não pode estar vazio.", "Tipo de utilizador Inválido", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Evento quando é clicado o botão cancel o form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
        }
        
        /// <summary>
        /// Evento quando o form é fechado
        /// Verifica se existem erros no formulário e lança uma mensagem caso existam
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditarContaUtilizadorDialog_OnClosing(object sender, CancelEventArgs e)
        {
            string message;

            if (this.DialogResult == true && FormHasErrors(out message))
            {
                // Errors still exist.
                MessageBox.Show(message);

                e.Cancel = true;
            }
        }
       
        /// <summary>
        /// Verifica se o form contém erros.
        /// </summary>
        /// <param name="message">Mensagem descritiva do erro</param>
        /// <returns>True se existirem erros, False caso contrário</returns>
        private bool FormHasErrors(out string message)
        {
            StringBuilder sb = new StringBuilder();
            GetErrors(sb, GridFormContaUtilizador);
            message = sb.ToString();
            return message != "";
        }
        
        /// <summary>
        /// Coloca os erros nos controlos do form respetivos
        /// </summary>
        /// <param name="sb">Construtor de strings</param>
        /// <param name="obj">Objeto a verificar</param>
        private void GetErrors(StringBuilder sb, DependencyObject obj)
        {
            foreach (object child in LogicalTreeHelper.GetChildren(obj))
            {
                TextBox element = child as TextBox;
                if (element == null) continue;
                if (Validation.GetHasError(element))
                {
                    sb.Append(element.Text + " has errors:\r\n");
                    foreach (ValidationError error in Validation.GetErrors(element))
                    {
                        sb.Append(" " + error.ErrorContent.ToString());
                        sb.Append("\r\n");
                    }
                }

                // Check the children of this object for errors.
                GetErrors(sb, element);
            }
        }
        
    }
}
