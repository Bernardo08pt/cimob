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

        public List<EstatisticaProgramaMobilidade> GetProgramasMobilidade()
        {
            List<EstatisticaProgramaMobilidade> estatisticas = new List<EstatisticaProgramaMobilidade>();

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            string sql = "Select t.Descricao as Programa, t.Estagio as Estagio, count(c.CandidaturaID) as Contagem From TiposMobilidade t Left Join Candidaturas c on t.TipoMobilidadeID = c.TipoMobilidadeID Group By t.Descricao, t.Estagio;";

            cmd.CommandText = sql;

            try
            {
                con.Open();
                SqlDataReader dr;
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    EstatisticaProgramaMobilidade e = new EstatisticaProgramaMobilidade();

                    if(((int)dr["Estagio"]) == 1)
                        e.Programa = (string)dr["Programa"] + " Estágio";
                    else
                        e.Programa = (string)dr["Programa"];

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

        public List<EstatisticaCandidaturaIPSEscola> GetCandidaturaIPSEscola()
        {
            List<EstatisticaCandidaturaIPSEscola> estatisticas = new List<EstatisticaCandidaturaIPSEscola>();

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            string sql = "Select c.Descricao as Nome, count(c.IpsEscolaID) as Contagem, a.AnoLetivo as Ano From Candidaturas a Left Join IpsCursos b on a.IpsCursoID = b.IpsCursoID Left Join IpsEscolas c on b.IpsEscolaID = c.IpsEscolaID Group By c.Descricao, c.IpsEscolaID, a.AnoLetivo;";

            cmd.CommandText = sql;

            try
            {
                con.Open();
                SqlDataReader dr;
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    EstatisticaCandidaturaIPSEscola e = new EstatisticaCandidaturaIPSEscola();

                    e.Nome = (string)dr["Nome"];
                    e.Contagem = (int)dr["Contagem"];
                    e.Ano = (string)dr["Ano"];
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
