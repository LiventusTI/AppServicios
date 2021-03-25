using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace Plataforma.Models.TratamientoCO2
{
    public class TratamientoCO2Modelo
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        static string connetionString = ConfigurationManager.ConnectionStrings["connetionString"].ConnectionString;
        public static List<Clases.TratamientoCO2> GetTratamientoCO2()
        {
            SqlConnection cnn;
            List<Clases.TratamientoCO2> TratamientosCO2 = new List<Clases.TratamientoCO2>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT ID_TRATAMIENTO_CO2, NOMBRE_TRATAMIENTO_CO2, ESTADO FROM AplicacionServicio_TratamientoCO2", cnn);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    TratamientosCO2.Add(new Clases.TratamientoCO2
                    {
                        Id = Convert.ToInt32(reader["ID_TRATAMIENTO_CO2"]),
                        Nombre = reader["NOMBRE_TRATAMIENTO_CO2"].ToString(),
                        Estado = Convert.ToInt32(reader["ESTADO"])
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
            return TratamientosCO2;
        }
    }
}