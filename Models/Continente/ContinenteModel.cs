using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Plataforma.Models.Continente
{
    public class ContinenteModel
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        static string connetionString = ConfigurationManager.ConnectionStrings["connetionString"].ConnectionString;

        public static List<Clases.Continente> GetContinentes()
        {
            SqlConnection cnn;
            List<Clases.Continente> Continentes = new List<Clases.Continente>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT ID_CONTINENTE, NOMBRE FROM AplicacionServicio_Continente WHERE ESTADO=0", cnn);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Continentes.Add(new Clases.Continente
                    {
                        Id = Convert.ToInt32(reader["ID_CONTINENTE"]),
                        Nombre = reader["NOMBRE"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
            return Continentes;
        }
    }
}