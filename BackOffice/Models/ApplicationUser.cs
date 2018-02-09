using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel;

namespace BackOffice.Models
{
    /// <summary>
    /// Classe que representa os useres da aplicação
    /// </summary>
    public class ApplicationUser : IdentityUser, INotifyPropertyChanged
    {
        /// <summary>
        /// Notificador de mudança dos atributos da classe
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Número do Utilizador
        /// </summary>
        private int numero;
        public int Numero {
            get { return numero; }
            set
            {
                numero = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Numero"));
            }
        }

        /// <summary>
        /// Nome do utilizador
        /// </summary>
        private string nome;
        public string Nome
        {
            get { return nome; }
            set
            {
                nome = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Nome"));
            }
        }

        /// <summary>
        /// Data de nascimento do utilizador
        /// </summary>
        private DateTime dataNascimento;
        public DateTime DataNascimento
        {
            get { return dataNascimento; }
            set
            {
                dataNascimento = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DataNascimento"));
            }
        }

        /// <summary>
        /// Email do utilizador
        /// </summary>
        private string email;
        override
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Email"));
            }
        }

        /// <summary>
        /// Construtor que recebe um utilizador
        /// </summary>
        /// <param name="user">Um utilizador do tipo Application user</param>
        public ApplicationUser(ApplicationUser user)
        {
            this.Id = user.Id;
            this.AccessFailedCount = user.AccessFailedCount;
            this.ConcurrencyStamp = user.ConcurrencyStamp;
            this.EmailConfirmed = user.EmailConfirmed;
            this.LockoutEnabled = user.LockoutEnabled;
            this.LockoutEnd = user.LockoutEnd;
            this.NormalizedEmail = user.NormalizedEmail;
            this.NormalizedUserName = user.NormalizedUserName;
            this.PasswordHash = user.PasswordHash;
            this.PhoneNumber = user.PhoneNumber;
            this.PhoneNumberConfirmed = user.PhoneNumberConfirmed;
            this.SecurityStamp = user.SecurityStamp;
            this.TwoFactorEnabled = user.TwoFactorEnabled;
            this.UserName = user.UserName;

            this.Numero = user.Numero;
            this.Nome= user.Nome;
            this.Email = user.Email;
            this.DataNascimento = user.DataNascimento;
        }

        /// <summary>
        /// Construtor em branco
        /// </summary>
        public ApplicationUser() { }
    }
}
