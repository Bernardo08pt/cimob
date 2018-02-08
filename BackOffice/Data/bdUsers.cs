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
    public class BDUsers
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;

        public ObservableCollection<ApplicationUser> GetUsers()
        {
            ObservableCollection<ApplicationUser> users = new ObservableCollection<ApplicationUser>();

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            string sql = "SELECT id, Numero, Nome, Email, DataNascimento FROM aspnetusers";

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
                    u.DataNascimento = (DateTime)dr["DataNascimento"];
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


    }
}
