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
    /// Classe responsável por aceder os roles que estão na base de dados
    /// </summary>
    public class BDRoles
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString; 
        /// <summary>
        /// Devolve todos os roles que estão na base de dados
        /// </summary>
        /// <returns>ObservableCollection com todos os roles</returns>
        public ObservableCollection<Role> GetRoles()
        {
            ObservableCollection<Role> roles = new ObservableCollection<Role>();

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            string sql = "SELECT Id, Name FROM AspNetRoles";

            cmd.CommandText = sql;

            try
            {
                con.Open();
                SqlDataReader dr;
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Role r = new Role();

                    r.Id = (string)dr["Id"];
                    r.Name = (string)dr["Name"];
                    roles.Add(r);
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
            return roles;
        }
    }
}
