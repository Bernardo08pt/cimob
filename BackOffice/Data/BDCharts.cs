using BackOffice.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows;

namespace BackOffice
{
    public class BDCharts
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;

        public List<EstatisticaEscolaParceira> GetEscolasParceiras()
        {
            List<EstatisticaEscolaParceira> estatisticas = new List<EstatisticaEscolaParceira>();

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            string sql = "Select c.Nome as Nome, count(c.EscolaID) as Contagem From CandidaturaCursos a Left Join Cursos b on a.CursoID = b.CursoID Left Join Escolas c on b.EscolaID = c.EscolaID Group By c.Nome, c.EscolaID;";

            cmd.CommandText = sql;

            try
            {
                con.Open();
                SqlDataReader dr;
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    EstatisticaEscolaParceira e = new EstatisticaEscolaParceira();

                    e.Nome = (string)dr["Nome"];
                    e.Contagem = (int)dr["Contagem"];
                    estatisticas.Add(e);
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
            return estatisticas;
        }
    }
}
