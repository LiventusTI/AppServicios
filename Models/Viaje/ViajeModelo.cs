using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Plataforma.Models.Viaje
{
    public class ViajeModelo
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        static string connetionString = ConfigurationManager.ConnectionStrings["connetionString"].ConnectionString;

        public static int CrearViaje(string Viaje = "", int PuertoOrigen = 0, int PuertoDestino = 0, int Nave = 0, DateTime? EtaNave = null, DateTime? InicioStacking = null, DateTime? TerminoStacking = null, DateTime? Etd = null, DateTime? EtaPOD = null, int Estado=0)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            int result = 0;
            SqlCommand scCommand = new SqlCommand("AgregarViaje", cnn);
            scCommand.CommandType = CommandType.StoredProcedure;
            scCommand.Parameters.Add("@VIAJE", SqlDbType.VarChar, 50).Value = Viaje;
            scCommand.Parameters.Add("@ID_PUERTOORIGEN", SqlDbType.Int, 50).Value = PuertoOrigen;
            scCommand.Parameters.Add("@ID_PUERTODESTINO", SqlDbType.Int, 50).Value = PuertoDestino;
            scCommand.Parameters.Add("@ID_NAVE", SqlDbType.Int, 50).Value = Nave;
            scCommand.Parameters.Add("@ESTADO", SqlDbType.Int, 100).Value = Estado;
            scCommand.Parameters.Add("@USUARIO", SqlDbType.VarChar, 100).Value = HttpContext.Current.Session["user"].ToString();
            if (EtaNave != null)
            {
                scCommand.Parameters.Add("@ETANAVE", SqlDbType.DateTime, 50).Value = EtaNave;
            }
            else
            {
                scCommand.Parameters.Add("@ETANAVE", SqlDbType.DateTime).Value = System.Data.SqlTypes.SqlDateTime.Null;
            }

            if (InicioStacking != null)
            {
                scCommand.Parameters.Add("@INICIOSTACKING", SqlDbType.DateTime, 50).Value = InicioStacking;
            }
            else
            {
                scCommand.Parameters.Add("@INICIOSTACKING", SqlDbType.DateTime).Value = System.Data.SqlTypes.SqlDateTime.Null;
            }

            if (TerminoStacking != null)
            {
                scCommand.Parameters.Add("@FINSTACKING", SqlDbType.DateTime, 50).Value = TerminoStacking;
            }
            else
            {
                scCommand.Parameters.Add("@FINSTACKING", SqlDbType.DateTime).Value = System.Data.SqlTypes.SqlDateTime.Null;
            }

            if (Etd != null)
            {
                scCommand.Parameters.Add("@ETD", SqlDbType.DateTime, 50).Value = Etd;
            }
            else
            {
                scCommand.Parameters.Add("@ETD", SqlDbType.DateTime).Value = System.Data.SqlTypes.SqlDateTime.Null;
            }

            if (EtaPOD != null)
            {
                scCommand.Parameters.Add("@ETAPOD", SqlDbType.DateTime, 50).Value = EtaPOD;
            }
            else
            {
                scCommand.Parameters.Add("@ETAPOD", SqlDbType.DateTime).Value = System.Data.SqlTypes.SqlDateTime.Null;
            }

            try
            {
                if (scCommand.Connection.State == ConnectionState.Closed)
                {
                    scCommand.Connection.Open();
                }
                //scCommand.ExecuteNonQuery();
                result = scCommand.ExecuteNonQuery();

                if (result == 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                scCommand.Connection.Close();
            }
        }

        public static int EditarViaje(int IdViaje, string Viaje = "", int PuertoOrigen = 0, int PuertoDestino = 0, int Nave = 0, DateTime? EtaNave = null, DateTime? InicioStacking = null, DateTime? TerminoStacking = null, DateTime? Etd = null, DateTime? EtaPOD = null, int Estado = 0)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            int result = 0;
            SqlCommand scCommand = new SqlCommand("EditarViaje", cnn);
            scCommand.CommandType = CommandType.StoredProcedure;
            scCommand.Parameters.Add("@ID_VIAJE", SqlDbType.Int, 50).Value = IdViaje;
            scCommand.Parameters.Add("@VIAJE", SqlDbType.VarChar, 50).Value = Viaje;
            scCommand.Parameters.Add("@ID_PUERTOORIGEN", SqlDbType.Int, 50).Value = PuertoOrigen;
            scCommand.Parameters.Add("@ID_PUERTODESTINO", SqlDbType.Int, 50).Value = PuertoDestino;
            scCommand.Parameters.Add("@ID_NAVE", SqlDbType.Int, 50).Value = Nave;
            scCommand.Parameters.Add("@ETANAVE", SqlDbType.DateTime, 50).Value = EtaNave;
            scCommand.Parameters.Add("@INICIOSTACKING", SqlDbType.DateTime, 50).Value = InicioStacking;
            scCommand.Parameters.Add("@FINSTACKING", SqlDbType.DateTime, 100).Value = TerminoStacking;
            scCommand.Parameters.Add("@ETD", SqlDbType.DateTime, 100).Value = Etd;
            scCommand.Parameters.Add("@ETAPOD", SqlDbType.DateTime, 100).Value = EtaPOD;
            scCommand.Parameters.Add("@ESTADO", SqlDbType.Int, 100).Value = Estado;
            scCommand.Parameters.Add("@USUARIO", SqlDbType.VarChar, 100).Value = HttpContext.Current.Session["user"].ToString();

            try
            {
                if (scCommand.Connection.State == ConnectionState.Closed)
                {
                    scCommand.Connection.Open();
                }
                //scCommand.ExecuteNonQuery();
                result = scCommand.ExecuteNonQuery();

                if (result == 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                scCommand.Connection.Close();
            }
        }

        public static int ValidarViaje(int IdViaje)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            int result = 0;
            SqlCommand scCommand = new SqlCommand("UPDATE AplicacionServicio_Viaje SET VALIDADO=0, USUARIOACCION=@USUARIO WHERE ID_VIAJE=@ID_VIAJE", cnn);
            scCommand.Parameters.Add("@ID_VIAJE", SqlDbType.Int, 50).Value = IdViaje;
            scCommand.Parameters.Add("@USUARIO", SqlDbType.VarChar, 100).Value = HttpContext.Current.Session["user"].ToString();
            try
            {
                if (scCommand.Connection.State == ConnectionState.Closed)
                {
                    scCommand.Connection.Open();
                }
                //scCommand.ExecuteNonQuery();
                result = scCommand.ExecuteNonQuery();

                if (result == 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                scCommand.Connection.Close();
            }
        }

        public static int ValidarViajeCreado(string Viaje = "", int PuertoOrigen = 0, int PuertoDestino = 0, int Nave = 0)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            int result = 0;
            SqlCommand scCommand = new SqlCommand("SELECT COUNT(*) AS CANTIDAD FROM AplicacionServicio_Viaje WHERE VIAJE=@VIAJE AND ID_PUERTOORIGEN=@ID_PUERTOORIGEN AND ID_PUERTODESTINO=@ID_PUERTODESTINO AND ID_NAVE=@ID_NAVE", cnn);
            scCommand.Parameters.Add("@VIAJE", SqlDbType.VarChar, 50).Value = Viaje;
            scCommand.Parameters.Add("@ID_PUERTOORIGEN", SqlDbType.Int, 50).Value = PuertoOrigen;
            scCommand.Parameters.Add("@ID_PUERTODESTINO", SqlDbType.Int, 50).Value = PuertoDestino;
            scCommand.Parameters.Add("@ID_NAVE", SqlDbType.Int, 50).Value = Nave;
            try
            {
                if (scCommand.Connection.State == ConnectionState.Closed)
                {
                    scCommand.Connection.Open();
                }
                SqlDataReader reader = scCommand.ExecuteReader();

                while (reader.Read())
                {
                    result = Convert.ToInt32(reader["CANTIDAD"]);
                }

                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                scCommand.Connection.Close();
            }
        }

        public static int EliminarViaje(int IdViaje)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
           
            SqlCommand scCommand = new SqlCommand("EXEC dbo.EliminarViaje @ID_VIAJE", cnn);
            scCommand.Parameters.Add("@ID_VIAJE", SqlDbType.Int, 50).Value = IdViaje;
            try
            {
                int result = 1;
                if (scCommand.Connection.State == ConnectionState.Closed)
                {
                    scCommand.Connection.Open();
                }
                SqlDataReader reader = scCommand.ExecuteReader();

                while (reader.Read())
                {
                    result = Convert.ToInt32(reader["ELIMINADO"]);
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                scCommand.Connection.Close();
            }
        }

        public static int ValidarViajeConServicios(int IdViaje)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);

            SqlCommand scCommand = new SqlCommand("SELECT COUNT(*) AS CONTSERVICIOS FROM AplicacionServicio_Servicio1 WHERE ID_VIAJE=@ID_VIAJE", cnn);
            scCommand.Parameters.Add("@ID_VIAJE", SqlDbType.Int, 50).Value = IdViaje;

            try
            {
                if (scCommand.Connection.State == ConnectionState.Closed)
                {
                    scCommand.Connection.Open();
                }
                SqlDataReader reader = scCommand.ExecuteReader();
                int ContServicios = 0;
                
                while (reader.Read())
                {
                    ContServicios = Convert.ToInt32(reader["CONTSERVICIOS"]);
                }

                return ContServicios;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

                scCommand.Connection.Close();
            }
        }

        public static List<Clases.Viaje> GetViajes()
        {
            SqlConnection cnn;
            List<Clases.Viaje> Viaje = new List<Clases.Viaje>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarViajes @USUARIO", cnn);
                command.Parameters.AddWithValue("@USUARIO", HttpContext.Current.Session["user"].ToString());
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    DateTime? etaNave = null;
                    DateTime? inicioStacking = null;
                    DateTime? finStacking = null;
                    DateTime? etd = null;
                    DateTime? etaPod = null;

                    if (reader["ETANAVE"] != DBNull.Value)
                    {
                        etaNave = Convert.ToDateTime(reader["ETANAVE"]);
                    }

                    if (reader["FECHAINISTACKING"] != DBNull.Value)
                    {
                        inicioStacking = Convert.ToDateTime(reader["FECHAINISTACKING"]);
                    }

                    if (reader["FECHAFINSTACKING"] != DBNull.Value)
                    {
                        finStacking = Convert.ToDateTime(reader["FECHAFINSTACKING"]);
                    }

                    if (reader["ETD"] != DBNull.Value)
                    {
                        etd = Convert.ToDateTime(reader["ETD"]);
                    }

                    if (reader["ETAPOD"] != DBNull.Value)
                    {
                        etaPod = Convert.ToDateTime(reader["ETAPOD"]);
                    }
                    Viaje.Add(new Clases.Viaje
                    {
                        Id = Convert.ToInt32(reader["ID_VIAJE"]),
                        NumeroViaje = reader["VIAJE"].ToString(),
                        PuertoOrigen = reader["PUERTOORIGEN"].ToString(),
                        PuertoDestino = reader["PUERTODESTINO"].ToString(),
                        Nave = reader["NAVE"].ToString(),
                        EtaNave = etaNave,
                        InicioStacking = inicioStacking,
                        FinStacking = finStacking,
                        Etd = etd,
                        EtaPod = etaPod,
                        Estado = Convert.ToInt32(reader["ESTADO"]),
                        Validado= Convert.ToInt32(reader["VALIDADO"]),
                        IdPuertoOrigen = Convert.ToInt32(reader["ID_PUERTOORIGEN"]),
                        IdPuertoDestino= Convert.ToInt32(reader["ID_PUERTODESTINO"]),
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
            return Viaje;
        }

        public static List<Clases.Viaje> GetNumViajesFromViajes()
        {
            SqlConnection cnn;
            List<Clases.Viaje> Viaje = new List<Clases.Viaje>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarNumViajesPorViaje @USUARIO", cnn);
                command.Parameters.Add("@USUARIO", SqlDbType.VarChar, 100).Value = HttpContext.Current.Session["user"].ToString();
                SqlDataReader reader = command.ExecuteReader();
                int cont = 1;
                while (reader.Read())
                {
                    Viaje.Add(new Clases.Viaje
                    {
                        Id = cont,
                        NumeroViaje = reader["VIAJE"].ToString(),
                    });
                    cont++;
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
            return Viaje;
        }

        public static List<Clases.Viaje> GetPOFromViajes()
        {
            SqlConnection cnn;
            List<Clases.Viaje> Viaje = new List<Clases.Viaje>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT DISTINCT V.ID_PUERTOORIGEN, P.NOMBRE FROM AplicacionServicio_Viaje V INNER JOIN AplicacionServicio_Puertos P ON V.ID_PUERTOORIGEN = P.ID_PUERTOORIGEN WHERE V.ESTADO = 0; ", cnn);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Viaje.Add(new Clases.Viaje
                    {
                        PuertoOrigen = reader["NOMBRE"].ToString(),
                        IdPuertoOrigen = Convert.ToInt32(reader["ID_PUERTOORIGEN"])
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
            return Viaje;
        }

        public static List<Clases.Viaje> GetPDFromViajes()
        {
            SqlConnection cnn;
            List<Clases.Viaje> Viaje = new List<Clases.Viaje>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT DISTINCT V.ID_PUERTODESTINO, P.NOMBRE FROM AplicacionServicio_Viaje V INNER JOIN AplicacionServicio_Puertos P ON V.ID_PUERTODESTINO = P.ID_PUERTOORIGEN WHERE V.ESTADO = 0; ", cnn);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Viaje.Add(new Clases.Viaje
                    {
                        PuertoDestino = reader["NOMBRE"].ToString(),
                        IdPuertoDestino = Convert.ToInt32(reader["ID_PUERTODESTINO"])
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
            return Viaje;
        }

        public static List<Clases.Viaje> GetPuertosOrigenViaje(string NumViaje="")
        {
            SqlConnection cnn;
            List<Clases.Viaje> Viaje = new List<Clases.Viaje>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarPuertosOrigenPorViaje @NUMVIAJE, @USUARIO", cnn);
                command.Parameters.Add("@NUMVIAJE", SqlDbType.VarChar, 50).Value = NumViaje;
                command.Parameters.Add("@USUARIO", SqlDbType.VarChar, 100).Value = HttpContext.Current.Session["user"].ToString();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Viaje.Add(new Clases.Viaje
                    {
                        PuertoOrigen = reader["NOMBRE"].ToString(),
                        IdPuertoOrigen = Convert.ToInt32(reader["ID_PUERTOORIGEN"])
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
            return Viaje;
        }

        public static List<Clases.Viaje> GetPuertosDestinoViaje(string NumViaje="", int IdPuertoOrigen=0)
        {
            SqlConnection cnn;
            List<Clases.Viaje> Viaje = new List<Clases.Viaje>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarPuertosDestinoPorViaje @NUMVIAJE, @ID_PUERTOORIGEN", cnn);
                command.Parameters.Add("@NUMVIAJE", SqlDbType.VarChar, 50).Value = NumViaje;
                command.Parameters.Add("@ID_PUERTOORIGEN", SqlDbType.Int, 50).Value = IdPuertoOrigen;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Viaje.Add(new Clases.Viaje
                    {
                        PuertoDestino = reader["NOMBRE"].ToString(),
                        IdPuertoDestino = Convert.ToInt32(reader["ID_PUERTODESTINO"])
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
            return Viaje;
        }

        public static Clases.Viaje GetInfoViaje(int IdViaje)
        {
            SqlConnection cnn;
            Clases.Viaje Viaje = new Clases.Viaje();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand scCommand = new SqlCommand("ConsultarViajePorId", cnn);
                scCommand.CommandType = CommandType.StoredProcedure;
                scCommand.Parameters.Add("@ID_VIAJE", SqlDbType.Int, 50).Value = IdViaje;
                SqlDataReader reader = scCommand.ExecuteReader();
                while (reader.Read())
                {
                    DateTime? etaNave = null;
                    DateTime? inicioStacking = null;
                    DateTime? finStacking = null;
                    DateTime? etd = null;
                    DateTime? etaPod = null;

                    if (reader["ETANAVE"] != DBNull.Value)
                    {
                        etaNave = Convert.ToDateTime(reader["ETANAVE"]);
                    }

                    if (reader["FECHAINISTACKING"] != DBNull.Value)
                    {
                        inicioStacking = Convert.ToDateTime(reader["FECHAINISTACKING"]);
                    }

                    if (reader["FECHAFINSTACKING"] != DBNull.Value)
                    {
                        finStacking = Convert.ToDateTime(reader["FECHAFINSTACKING"]);
                    }

                    if (reader["ETD"] != DBNull.Value)
                    {
                        etd = Convert.ToDateTime(reader["ETD"]);
                    }

                    if (reader["ETAPOD"] != DBNull.Value)
                    {
                        etaPod = Convert.ToDateTime(reader["ETAPOD"]);
                    }

                    Viaje.Id = Convert.ToInt32(reader["ID_VIAJE"]);
                    Viaje.NumeroViaje = reader["VIAJE"].ToString();
                    Viaje.IdPuertoOrigen = Convert.ToInt32(reader["ID_PUERTOORIGEN"]);
                    Viaje.IdPuertoDestino = Convert.ToInt32(reader["ID_PUERTODESTINO"]);
                    Viaje.IdNave = Convert.ToInt32(reader["ID_NAVE"]);
                    Viaje.EtaNave = etaNave;
                    Viaje.InicioStacking =inicioStacking;
                    Viaje.FinStacking = finStacking;
                    Viaje.Etd = etd;
                    Viaje.EtaPod = etaPod;
                    Viaje.Estado = Convert.ToInt32(reader["ESTADO"]);
                    Viaje.Validado = Convert.ToInt32(reader["VALIDADO"]);
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
            return Viaje;
        }

        public static Clases.Viaje GetViajeServicio(string Viaje = "", int PuertoOrigen = 0, int PuertoDestino = 0)
        {
            SqlConnection cnn;
            Clases.Viaje Viajes = new Clases.Viaje();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand scCommand = new SqlCommand("ConsultarViajeServicio", cnn);
                scCommand.CommandType = CommandType.StoredProcedure;
                scCommand.Parameters.Add("@VIAJE", SqlDbType.VarChar, 50).Value = Viaje;
                scCommand.Parameters.Add("@ID_PUERTOORIGEN", SqlDbType.Int, 50).Value = PuertoOrigen;
                scCommand.Parameters.Add("@ID_PUERTODESTINO", SqlDbType.Int, 50).Value = PuertoDestino;
                SqlDataReader reader = scCommand.ExecuteReader();
                while (reader.Read())
                {
                    if (Convert.ToInt32(reader["ID_VIAJE"]) != 0)
                    {
                        DateTime? etaNave = null;
                        DateTime? inicioStacking = null;
                        DateTime? finStacking = null;
                        DateTime? etd = null;
                        DateTime? etaPod = null;

                        if (reader["ETANAVE"]!= DBNull.Value)
                        {
                            etaNave = Convert.ToDateTime(reader["ETANAVE"]);
                        }

                        if (reader["FECHAINISTACKING"] != DBNull.Value)
                        {
                            inicioStacking = Convert.ToDateTime(reader["FECHAINISTACKING"]);
                        }

                        if (reader["FECHAFINSTACKING"] != DBNull.Value)
                        {
                            finStacking = Convert.ToDateTime(reader["FECHAFINSTACKING"]);
                        }

                        if (reader["ETD"] != DBNull.Value)
                        {
                            etd= Convert.ToDateTime(reader["ETD"]);
                        }

                        if (reader["ETAPOD"] != DBNull.Value)
                        {
                            etaPod= Convert.ToDateTime(reader["ETAPOD"]);
                        }


                        Viajes.Id = Convert.ToInt32(reader["ID_VIAJE"]);
                        Viajes.NumeroViaje = reader["VIAJE"].ToString();
                        Viajes.IdPuertoOrigen = Convert.ToInt32(reader["ID_PUERTOORIGEN"]);
                        Viajes.IdPuertoDestino = Convert.ToInt32(reader["ID_PUERTODESTINO"]);
                        Viajes.IdNave = Convert.ToInt32(reader["ID_NAVE"]);
                        Viajes.EtaNave = etaNave;
                        Viajes.InicioStacking = inicioStacking;
                        Viajes.FinStacking = finStacking;
                        Viajes.Etd = etd;
                        Viajes.EtaPod = etaPod;
                        Viajes.Estado = Convert.ToInt32(reader["ESTADO"]);
                        Viajes.Validado = Convert.ToInt32(reader["VALIDADO"]);
                    }
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
            return Viajes;
        }

        public static List<Clases.Viaje> GetViajesPuertos(int IdPuertoOrigen, int IdPuertoDestino)
        {
            SqlConnection cnn;
            List<Clases.Viaje> Viajes = new List<Clases.Viaje>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("select * from aplicacionservicio_viaje where ID_PUERTOORIGEN = @IDPUERTOORIGEN AND ID_PUERTODESTINO = @IDPUERTODESTINO AND ESTADO=0;", cnn);
                command.Parameters.AddWithValue("@IDPUERTOORIGEN", IdPuertoOrigen);
                command.Parameters.AddWithValue("@IDPUERTODESTINO", IdPuertoDestino);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Viajes.Add(new Clases.Viaje
                    {
                        NumeroViaje = reader["VIAJE"].ToString(),
                        Id = Convert.ToInt32(reader["ID_VIAJE"]),
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
            return Viajes;
        }

    }
}