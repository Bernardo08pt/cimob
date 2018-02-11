using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel;

namespace BackOffice.Models
{
    /// <summary>
    /// Classe que representa os users da aplicação
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
        /// Role do utilizador
        /// </summary>
        private string roleId;
        public string RoleId
        {
            get { return roleId; }
            set
            {
                roleId = value;
                OnPropertyChanged(new PropertyChangedEventArgs("RoleId"));
            }
        }


        /// <summary>
        /// Role do utilizador
        /// </summary>
        private string roleName;
        public string RoleName
        {
            get { return roleName; }
            set
            {
                roleName = value;
                OnPropertyChanged(new PropertyChangedEventArgs("RoleName"));
            }
        }

        /// <summary>
        /// Construtor que recebe um utilizador
        /// </summary>
        /// <param name="user">Um utilizador do tipo Application user</param>
        public ApplicationUser(ApplicationUser user)
        {
            Id = user.Id;
            /*AccessFailedCount = user.AccessFailedCount;
            ConcurrencyStamp = user.ConcurrencyStamp;
            EmailConfirmed = user.EmailConfirmed;
            LockoutEnabled = user.LockoutEnabled;
            LockoutEnd = user.LockoutEnd;
            NormalizedEmail = user.NormalizedEmail;
            NormalizedUserName = user.NormalizedUserName;
            PasswordHash = user.PasswordHash;
            PhoneNumber = user.PhoneNumber;
            PhoneNumberConfirmed = user.PhoneNumberConfirmed;
            SecurityStamp = user.SecurityStamp;
            TwoFactorEnabled = user.TwoFactorEnabled;*/

           // UserName = user.Email;
            Numero = user.Numero;
            Nome= user.Nome;
            Email = user.Email;
            DataNascimento = user.DataNascimento;
            RoleId = user.roleId;
            RoleName = user.RoleName;
        }

        /// <summary>
        /// Construtor em branco
        /// </summary>
        public ApplicationUser() { }
    }
}
