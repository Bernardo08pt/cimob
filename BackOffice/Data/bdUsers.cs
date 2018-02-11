using BackOffice.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BackOffice.Data
{
    /// <summary>
    /// Classe com métodos para fazer o CRUD da tabela de utilizadores
    /// </summary>
    public class BDUsers
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString; 

        /// <summary>
        /// Retorna todos os utilizadores que existem na base de dados
        /// </summary>
        /// <returns>Lista com todos os utilizadores existentes na base de dados</returns>
        public ObservableCollection<ApplicationUser> GetUsers()
        {
            ObservableCollection<ApplicationUser> users = new ObservableCollection<ApplicationUser>();

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            string sql = "SELECT A.Id, A.Numero, A.Nome, A.Email, A.DataNascimento, B.RoleId AS RoleId, C.Name AS RoleName " +
                          "FROM AspNetUsers AS A " +
                          "LEFT JOIN AspNetUserRoles AS B ON A.Id = B.UserId " +
                          "LEFT JOIN AspNetRoles AS C ON C.Id = B.RoleId";

            cmd.CommandText = sql;

            try
            {
                con.Open();
                SqlDataReader dr;
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    ApplicationUser u = new ApplicationUser();

                    u.Id = (string)dr["Id"];
                    u.Numero = (int)dr["Numero"];
                    u.Nome = (string)dr["Nome"];
                    u.Email = (string)dr["Email"];
                    u.DataNascimento = (DateTime)dr["DataNascimento"];
                    u.RoleId = (string)dr["RoleId"];
                    u.RoleName = (string)dr["RoleName"];
                    users.Add(u);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            finally
            {
                con.Close();
            }
            return users;
        }

        /// <summary>
        /// Insere um utilizador na base de dados
        /// </summary>
        /// <param name="user">Utilizador a inserir</param>
        /// <returns>Id do utilizador inserido</returns>
        public bool InsertUser(ApplicationUser user)
        {
            bool result = true;
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = con;

            string sqlInsert = "INSERT INTO AspNetUsers(Id,AccessFailedCount, " +
                                    "ConcurrencyStamp, EmailConfirmed, LockoutEnabled, " +
                                    " NormalizedEmail, NormalizedUserName, " +
                                    "PasswordHash,  PhoneNumberConfirmed, SecurityStamp," +
                                    "TwoFactorEnabled, UserName, Numero, " +
                                    "Nome, Email, DataNascimento) " +
                                    "VALUES(@Id, @accesFailedCount, @concurrecyCount, @emailConfirmed," +
                                    "@lockoutEnabled,  @normalizedEmail, @normalizedUserName," +
                                    "@passwordHash, @phoneNumberConfirmed, @securityStamp," +
                                    "@twoFactorEnabled, @userName, @numero, @nome, @email, @dataNascimento);" +
                                    "INSERT INTO AspNetUserRoles(RoleId, UserId) VALUES(@roleId, @userId);";

            cmd.CommandText = sqlInsert;

            cmd.Parameters.AddWithValue("@id", user.Id);
            cmd.Parameters.AddWithValue("@accesFailedCount", user.AccessFailedCount);
            cmd.Parameters.AddWithValue("@concurrecyCount", user.ConcurrencyStamp);
            cmd.Parameters.AddWithValue("@emailConfirmed", user.EmailConfirmed);
            cmd.Parameters.AddWithValue("@lockoutEnabled", user.LockoutEnabled);
            cmd.Parameters.AddWithValue("@normalizedEmail", user.NormalizedEmail);
            cmd.Parameters.AddWithValue("@securityStamp", user.SecurityStamp);
            cmd.Parameters.AddWithValue("@normalizedUserName", user.NormalizedUserName);
            cmd.Parameters.AddWithValue("@passwordHash", user.PasswordHash);
            cmd.Parameters.AddWithValue("@phoneNumberConfirmed", user.PhoneNumberConfirmed);
            cmd.Parameters.AddWithValue("@twoFactorEnabled", user.TwoFactorEnabled);
            cmd.Parameters.AddWithValue("@userName", user.UserName);
            cmd.Parameters.AddWithValue("@numero", user.Numero);
            cmd.Parameters.AddWithValue("@nome", user.Nome);
            cmd.Parameters.AddWithValue("@email", user.Email);
            cmd.Parameters.AddWithValue("@dataNascimento", user.DataNascimento);
            cmd.Parameters.AddWithValue("@roleId", user.RoleId);
            cmd.Parameters.AddWithValue("@userId", user.Id);

            try
            {
                con.Open();

               cmd.ExecuteScalar();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                result = false;
            }
            finally
            {
                con.Close();
            }

            return result;  
        }

        /// <summary>
        /// Faz update de um utilizador na base de dados
        /// </summary>
        /// <param name="user">Utilizador a fazer update</param>
        public void UpdateUser(ApplicationUser user)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = con;

            string sql = "UPDATE AspNetUsers SET UserName = @userName, Numero = @numero, Nome = @nome, Email = @email, DataNascimento= @dataNascimento WHERE (Id = @id);" +
                         "UPDATE AspNetUserRoles SET RoleId = @roleId WHERE (UserId = @userId);";
            
            cmd.CommandText = sql;


            cmd.Parameters.AddWithValue("@userName", user.UserName);
            cmd.Parameters.AddWithValue("@numero", user.Numero);
            cmd.Parameters.AddWithValue("@nome", user.Nome);
            cmd.Parameters.AddWithValue("@email", user.Email);
            cmd.Parameters.AddWithValue("@dataNascimento", user.DataNascimento);
            cmd.Parameters.AddWithValue("@id", user.Id);
            cmd.Parameters.AddWithValue("@roleId", user.RoleId);
            cmd.Parameters.AddWithValue("@userId", user.Id);

            int regs = 0;

            try
            {
                con.Open();

                regs = cmd.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            finally
            {
                con.Close();
            }
            
        }

        /// <summary>
        /// Remove um utilizador na base de dados
        /// </summary>
        /// <param name="id">Id do utilizador a remover</param>
        public void RemoverUser(string id)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = con;

            string sql = "DELETE FROM AspNetUsers WHERE (Id = @id)";
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@Id", id);

            int regs = 0;

            try
            {
                con.Open();

                regs = cmd.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            finally
            {
                con.Close();
            }

            MessageBox.Show(regs + " registo apagado");
        }

    }
}
