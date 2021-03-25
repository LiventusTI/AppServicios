using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Plataforma.Models.Contenedor
{
    public class ContenedorModelo
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        static string connetionString = ConfigurationManager.ConnectionStrings["connetionString"].ConnectionString;
        public static int GetIdCajaContenedor(string NombreCaja) {
            SqlConnection cnn;
            int id = 0;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT ID_CAJACONTENEDOR FROM APLICACIONSERVICIO_CAJACONTENEDOR WHERE DESCRIPCION = @DESCRIPCION", cnn);
                command.Parameters.AddWithValue("@DESCRIPCION", NombreCaja);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                        id = Convert.ToInt32(reader["ID_CAJACONTENEDOR"]);
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
            return id;
        }

        public static List<Clases.Contenedor> GetAlertasContenedores(List<Clases.Contenedor> Contenedores)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);

            foreach (Clases.Contenedor contenedor in Contenedores)
            {
                try
                {
                    cnn.Open();
                    SqlCommand command = new SqlCommand("EXEC dbo.ConsultarAlertaByContenedor @ID_CONTENEDOR", cnn);
                    command.Parameters.AddWithValue("@ID_CONTENEDOR", contenedor.IdContenedor);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        contenedor.DetalleAlerta = reader["DETALLE_ALERTA"].ToString();
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
            }


            return Contenedores;
        }

        public static Clases.InvetarioContenedor GetContenedoresByPais(int IdPais) {

            SqlConnection cnn;
            Clases.InvetarioContenedor Inventario = new Clases.InvetarioContenedor();
            cnn = new SqlConnection(connectionString);

            var i = 0;
            var j = 0;
            var contador = 0;
            var contadorS = 0;
            SqlCommand scCommand = new SqlCommand("GetInventarioByPais", cnn);
            scCommand.CommandType = CommandType.StoredProcedure;
            scCommand.Parameters.Add("@IDPAIS", SqlDbType.Int, 50).Value = IdPais;

            try
            {
                if (scCommand.Connection.State == ConnectionState.Closed)
                {
                    scCommand.Connection.Open();
                }
                //scCommand.ExecuteNonQuery();
                SqlDataReader reader = scCommand.ExecuteReader();

                while (reader.Read())
                {
                    i++;
                    if (Convert.ToInt32(reader["SCRUBBER"]) == 0) {
                        j++;
                    }

                    if (Convert.ToInt32(reader["ID_NAVIERA"]) != 0)
                    {
                        if (Inventario.Navieras.Count == 0)
                        {
                            Inventario.Navieras.Add(reader["NOMBRE"].ToString());
                            contador = contador + 1;
                            Inventario.Contenedores.Add(contador);
                            if (Convert.ToInt32(reader["SCRUBBER"]) == 0)
                            {
                                contadorS = contadorS + 1;
                                Inventario.ContenedoresWS.Add(contadorS);
                            }
                            else
                            {
                                Inventario.ContenedoresWS.Add(contadorS);
                            }
                        }
                        else {
                            if (Inventario.Navieras.Exists(x => x == reader["NOMBRE"].ToString()))
                            {
                                int index = Inventario.Navieras.FindIndex(z => z == reader["NOMBRE"].ToString());
                                Inventario.Contenedores[index] = Inventario.Contenedores[index] + 1;
                                if (Convert.ToInt32(reader["SCRUBBER"]) == 0)
                                {
                                    Inventario.ContenedoresWS[index] = Inventario.ContenedoresWS[index] + 1;
                                }
                            }
                            else {
                                contador = 0;
                                contadorS = 0;
                                Inventario.Navieras.Add(reader["NOMBRE"].ToString());
                                contador = contador + 1;
                                Inventario.Contenedores.Add(contador);
                                if (Convert.ToInt32(reader["SCRUBBER"]) == 0)
                                {
                                    contadorS = contadorS + 1;
                                    Inventario.ContenedoresWS.Add(contadorS);
                                }else
                                {
                                    Inventario.ContenedoresWS.Add(contadorS);
                                }
                            }
                        }
                    }
                }
                Inventario.CantidadContenedores = i;
                Inventario.CantidadScrubber = j;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                scCommand.Connection.Close();
            }

            return Inventario;
        }

        public static Clases.InvetarioContenedor GetContenedoresByCiudad(int IdCiudad)
        {
            SqlConnection cnn;
            Clases.InvetarioContenedor Inventario = new Clases.InvetarioContenedor();
            cnn = new SqlConnection(connectionString);
            var i = 0;
            var j = 0;
            var contador = 0;
            var contadorS = 0;
            SqlCommand scCommand = new SqlCommand("GetInventarioCiudad", cnn);
            scCommand.CommandType = CommandType.StoredProcedure;
            scCommand.Parameters.Add("@IDCIUDAD", SqlDbType.Int, 50).Value = IdCiudad;

            try
            {
                if (scCommand.Connection.State == ConnectionState.Closed)
                {
                    scCommand.Connection.Open();
                }
                //scCommand.ExecuteNonQuery();
                SqlDataReader reader = scCommand.ExecuteReader();

                while (reader.Read())
                {
                    i++;
                    if (Convert.ToInt32(reader["SCRUBBER"]) == 0)
                    {
                        j++;
                    }

                    if (Convert.ToInt32(reader["ID_NAVIERA"]) != 0)
                    {
                        if (Inventario.Navieras.Count == 0)
                        {
                            Inventario.Navieras.Add(reader["NOMBRE"].ToString());
                            contador = contador + 1;
                            Inventario.Contenedores.Add(contador);
                            if (Convert.ToInt32(reader["SCRUBBER"]) == 0)
                            {
                                contadorS = contadorS + 1;
                                Inventario.ContenedoresWS.Add(contadorS);
                            }
                            else
                            {
                                Inventario.ContenedoresWS.Add(contadorS);
                            }
                        }
                        else
                        {
                            if (Inventario.Navieras.Exists(x => x == reader["NOMBRE"].ToString()))
                            {
                                int index = Inventario.Navieras.FindIndex(z => z == reader["NOMBRE"].ToString());
                                Inventario.Contenedores[index] = Inventario.Contenedores[index] + 1;
                                if (Convert.ToInt32(reader["SCRUBBER"]) == 0)
                                {
                                    Inventario.ContenedoresWS[index] = Inventario.ContenedoresWS[index] + 1;
                                }
                            }
                            else
                            {
                                contador = 0;
                                contadorS = 0;
                                Inventario.Navieras.Add(reader["NOMBRE"].ToString());
                                contador = contador + 1;
                                Inventario.Contenedores.Add(contador);
                                if (Convert.ToInt32(reader["SCRUBBER"]) == 0)
                                {
                                    contadorS = contadorS + 1;
                                    Inventario.ContenedoresWS.Add(contadorS);
                                }
                                else
                                {
                                    Inventario.ContenedoresWS.Add(contadorS);
                                }
                            }
                        }
                    }
                }
                Inventario.CantidadContenedores = i;
                Inventario.CantidadScrubber = j;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                scCommand.Connection.Close();
            }

            return Inventario;
        }

        public static Clases.InvetarioContenedor GetContenedoresByDeposito(int IdDeposito)
        {
            SqlConnection cnn;
            Clases.InvetarioContenedor Inventario = new Clases.InvetarioContenedor();
            cnn = new SqlConnection(connectionString);
            var i = 0;
            var j = 0;
            var contador = 0;
            var contadorS = 0;
            SqlCommand scCommand = new SqlCommand("GetInventarioDeposito", cnn);
            scCommand.CommandType = CommandType.StoredProcedure;
            scCommand.Parameters.Add("@IDDEPOSITO", SqlDbType.Int, 50).Value = IdDeposito;

            try
            {
                if (scCommand.Connection.State == ConnectionState.Closed)
                {
                    scCommand.Connection.Open();
                }
                //scCommand.ExecuteNonQuery();
                SqlDataReader reader = scCommand.ExecuteReader();

                while (reader.Read())
                {
                    i++;
                    if (Convert.ToInt32(reader["SCRUBBER"]) == 0)
                    {
                        j++;
                    }

                    if (Convert.ToInt32(reader["ID_NAVIERA"]) != 0)
                    {
                        if (Inventario.Navieras.Count == 0)
                        {
                            Inventario.Navieras.Add(reader["NOMBRE"].ToString());
                            contador = contador + 1;
                            Inventario.Contenedores.Add(contador);
                            if (Convert.ToInt32(reader["SCRUBBER"]) == 0)
                            {
                                contadorS = contadorS + 1;
                                Inventario.ContenedoresWS.Add(contadorS);
                            }
                            else
                            {
                                Inventario.ContenedoresWS.Add(contadorS);
                            }
                        }
                        else
                        {
                            if (Inventario.Navieras.Exists(x => x == reader["NOMBRE"].ToString()))
                            {
                                int index = Inventario.Navieras.FindIndex(z => z == reader["NOMBRE"].ToString());
                                Inventario.Contenedores[index] = Inventario.Contenedores[index] + 1;
                                if (Convert.ToInt32(reader["SCRUBBER"]) == 0)
                                {
                                    Inventario.ContenedoresWS[index] = Inventario.ContenedoresWS[index] + 1;
                                }
                            }
                            else
                            {
                                contador = 0;
                                contadorS = 0;
                                Inventario.Navieras.Add(reader["NOMBRE"].ToString());
                                contador = contador + 1;
                                Inventario.Contenedores.Add(contador);
                                if (Convert.ToInt32(reader["SCRUBBER"]) == 0)
                                {
                                    contadorS = contadorS + 1;
                                    Inventario.ContenedoresWS.Add(contadorS);
                                }
                                else
                                {
                                    Inventario.ContenedoresWS.Add(contadorS);
                                }
                            }
                        }
                    }
                }
                Inventario.CantidadContenedores = i;
                Inventario.CantidadScrubber = j;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                scCommand.Connection.Close();
            }

            return Inventario;
        }

        public static List<Clases.Contenedor> GetContenedores(int Estado)
        {
            SqlConnection cnn;
            List<Clases.Contenedor> Contenedor = new List<Clases.Contenedor>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                string consulta = "EXEC dbo.ConsultarContenedoresPorEstado @ESTADO, @USUARIO";

                SqlCommand command = new SqlCommand(consulta, cnn);
                command.Parameters.AddWithValue("@ESTADO", Estado);
                command.Parameters.Add("@USUARIO", SqlDbType.VarChar, 100).Value = HttpContext.Current.Session["user"].ToString();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    DateTime? fecha = null;
                    DateTime? fechaBateria = null;
                    DateTime? fechaGestion = null;
                    DateTime? fechaBateriaModem = null;
                    int contDiasTravelingControlador = 0;
                    int contDiasSleepControlador = 0;
                    int contDiasTravelingModem = 0;
                    int contDiasSleepModem = 0;
                    if (reader["FECHAREALIZACION"] != DBNull.Value)
                    {
                        fecha = Convert.ToDateTime(reader["FECHAREALIZACION"]);
                    }

                    if (reader["FECHABATERIA"] != DBNull.Value)
                    {
                        fechaBateria = Convert.ToDateTime(reader["FECHABATERIA"]);
                    }

                    if (reader["CONT_DIAS_TRAVELING_CONTROLADOR"] != DBNull.Value)
                    {
                        contDiasTravelingControlador = Convert.ToInt32(reader["CONT_DIAS_TRAVELING_CONTROLADOR"]);
                    }

                    if (reader["CONT_DIAS_SLEEP_CONTROLADOR"] != DBNull.Value)
                    {
                        contDiasSleepControlador = Convert.ToInt32(reader["CONT_DIAS_SLEEP_CONTROLADOR"]);
                    }

                    if (reader["CONT_DIAS_TRAVELING_MODEM"] != DBNull.Value)
                    {
                        contDiasTravelingModem = Convert.ToInt32(reader["CONT_DIAS_TRAVELING_MODEM"]);
                    }

                    if (reader["CONT_DIAS_SLEEP_MODEM"] != DBNull.Value)
                    {
                        contDiasSleepModem = Convert.ToInt32(reader["CONT_DIAS_SLEEP_MODEM"]);
                    }

                    if (reader["FECHA_GESTION"] != DBNull.Value)
                    {
                        fechaGestion = Convert.ToDateTime(reader["FECHA_GESTION"]);
                    }

                    if (reader["FECHABATERIA_MODEM"] != DBNull.Value)
                    {
                        fechaBateriaModem = Convert.ToDateTime(reader["FECHABATERIA_MODEM"]);
                    }

                    Contenedor.Add(new Clases.Contenedor
                    {
                        IdContenedor = Convert.ToInt32(reader["ID_CONTENEDOR"]),
                        NumeroContenedor = reader["CONTENEDOR"].ToString(),
                        IdReserva = Convert.ToInt32(reader["ID_RESERVA"]),
                        Deposito = reader["NOMBREDEPOSITO"].ToString(),
                        Ciudad = reader["NOMBRECIUDAD"].ToString(),
                        Pais = reader["NOMBREPAIS"].ToString(),
                        Naviera = reader["NOMBRENAVIERA"].ToString(),
                        AnoContenedor = Convert.ToInt32(reader["ANOCONTENEDOR"]),
                        Fecha = fecha,
                        NombreMaquinaria = reader["NOMBREMAQUINARIA"].ToString(),
                        CajaContenedor = reader["NOMBRECAJA"].ToString(),
                        Scrubber = Convert.ToInt32(reader["SCRUBBER"]),
                        KitCortina = Convert.ToInt32(reader["KIT_CORTINA"]),
                        Controlador = reader["CONTROLADOR"].ToString(),
                        Bateria = reader["BATERIA"].ToString(),
                        FechaBateria = fechaBateria,
                        DiasBateria = Convert.ToInt32(reader["DIASBATERIA"]),
                        CONT_DIAS_TRAVELING_CONTROLADOR = contDiasTravelingControlador,
                        CONT_DIAS_SLEEP_CONTROLADOR = contDiasSleepControlador,
                        CONT_DIAS_TRAVELING_MODEM = contDiasTravelingModem,
                        CONT_DIAS_SLEEP_MODEM = contDiasSleepModem,
                        Gestion = reader["GESTION"].ToString(),
                        FechaGestion = fechaGestion,
                        Modem = reader["MODEM"].ToString(),
                        BateriaModem = reader["BATERIA_MODEM"].ToString(),
                        FechaBateriaModem = fechaBateriaModem,
                        EstadoOperacionControlador = reader["ESTADO_CONTROLADOR"].ToString(),
                        EstadoOperacionModem = reader["ESTADO_MODEM"].ToString()
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
            return Contenedor;
        }

        public static List<Clases.Contenedor> GetContenedoresAprobados()
        {
            SqlConnection cnn;
            List<Clases.Contenedor> Contenedor = new List<Clases.Contenedor>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                string consulta = "SELECT ID_CONTENEDOR, CONTENEDOR FROM AplicacionServicio_Contenedores WHERE ESTADO=1";

                SqlCommand command = new SqlCommand(consulta, cnn);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Contenedor.Add(new Clases.Contenedor
                    {
                        IdContenedor = Convert.ToInt32(reader["ID_CONTENEDOR"]),
                        NumeroContenedor = reader["CONTENEDOR"].ToString()

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
            return Contenedor;
        }

        public static List<Clases.Contenedor> GetContenedoresAprobadosSP(string Usuario)
        {
            SqlConnection cnn;
            List<Clases.Contenedor> Contenedor = new List<Clases.Contenedor>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                string consulta = "EXEC dbo.ConsultarContenedoresSP @USUARIO";
                
                SqlCommand command = new SqlCommand(consulta, cnn);
                command.Parameters.Add("@USUARIO", SqlDbType.VarChar, 50).Value = Usuario;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Contenedor.Add(new Clases.Contenedor
                    {
                        IdContenedor = Convert.ToInt32(reader["ID_CONTENEDOR"]),
                        NumeroContenedor = reader["CONTENEDOR"].ToString()

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
            return Contenedor;
        }

        public static List<Clases.EstadoContenedor> GetEstadoContenedores()
        {
            SqlConnection cnn;
            List<Clases.EstadoContenedor> Estados = new List<Clases.EstadoContenedor>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                string consulta = "SELECT ID_ESTADO, DESCRIPCION FROM AplicacionServicio_EstadoLeaktest";

                SqlCommand command = new SqlCommand(consulta, cnn);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Estados.Add(new Clases.EstadoContenedor
                    {
                        Id = Convert.ToInt32(reader["ID_ESTADO"]),
                        Nombre= reader["DESCRIPCION"].ToString()
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
            return Estados;
        }

        public static int CancelarContenedor(int IdContenedor)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            int result = 0;

            SqlCommand scCommand = new SqlCommand("CancelarContenedor", cnn);
            scCommand.CommandType = CommandType.StoredProcedure;
            scCommand.Parameters.Add("@IDCONTENEDOR", SqlDbType.Int, 50).Value = IdContenedor;

            try
            {
                if (scCommand.Connection.State == ConnectionState.Closed)
                {
                    scCommand.Connection.Open();
                }
                //scCommand.ExecuteNonQuery();
                result = scCommand.ExecuteNonQuery();

                if (result != 0)
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

        public static int GuardarGestion(int IdContenedor, string Gestion = "", DateTime? FechaGestion = null)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            int result = 0;

            SqlCommand scCommand = new SqlCommand("GuardarGestion", cnn);
            scCommand.CommandType = CommandType.StoredProcedure;
            scCommand.Parameters.Add("@IDCONTENEDOR", SqlDbType.Int, 50).Value = IdContenedor;
            scCommand.Parameters.Add("@GESTION", SqlDbType.VarChar, 500).Value = Gestion;
            //scCommand.Parameters.Add("@FECHA_GESTION", SqlDbType.DateTime, 500).Value = FechaGestion;

            if (FechaGestion == null)
            {
                scCommand.Parameters.Add("@FECHA_GESTION", SqlDbType.DateTime).Value = System.Data.SqlTypes.SqlDateTime.Null;
            }
            else
            {
                scCommand.Parameters.Add("@FECHA_GESTION", SqlDbType.DateTime).Value = FechaGestion;
            }

            try
            {
                if (scCommand.Connection.State == ConnectionState.Closed)
                {
                    scCommand.Connection.Open();
                }
                //scCommand.ExecuteNonQuery();
                result = scCommand.ExecuteNonQuery();

                if (result != 0)
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

        public static int DisponibilizarContenedor(int IdContenedor)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            int result = 0;

            SqlCommand scCommand = new SqlCommand("DisponibilizarContenedor", cnn);
            scCommand.CommandType = CommandType.StoredProcedure;
            scCommand.Parameters.Add("@IDCONTENEDOR", SqlDbType.Int, 50).Value = IdContenedor;

            try
            {
                if (scCommand.Connection.State == ConnectionState.Closed)
                {
                    scCommand.Connection.Open();
                }
                //scCommand.ExecuteNonQuery();
                result = scCommand.ExecuteNonQuery();

                if (result != 0)
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

        public static List<Clases.DetalleContenedor> GetContenedoresByReserva(int IdReserva)
        {
            SqlConnection cnn;
            List<Clases.DetalleContenedor> Contenedor = new List<Clases.DetalleContenedor>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("Select * from aplicacionservicio_DetalleReservaContenedor detalle, Aplicacionservicio_Contenedores contenedor where detalle.contenedor = contenedor.contenedor and detalle.id_reserva = @IDRESERVA and (contenedor.Estado = 2 OR contenedor.ESTADO = 3 OR contenedor.ESTADO = 5 OR contenedor.ESTADO = 6 OR contenedor.ESTADO = 7);", cnn);
                command.Parameters.AddWithValue("@IDRESERVA", IdReserva);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Contenedor.Add(new Clases.DetalleContenedor
                    {
                        Contenedor = reader["CONTENEDOR"].ToString(),
                        IdMaquinaria = Convert.ToInt32(reader["ID_MAQUINARIA"]),
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
            return Contenedor;
        }

        public static List<Clases.DetalleContenedor> GetAllContenedoresByReserva(int IdReserva)
        {
            SqlConnection cnn;
            List<Clases.DetalleContenedor> Contenedor = new List<Clases.DetalleContenedor>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("Select * from aplicacionservicio_DetalleReservaContenedor detalle, Aplicacionservicio_Contenedores contenedor where detalle.contenedor = contenedor.contenedor and detalle.id_reserva = @IDRESERVA;", cnn);
                command.Parameters.AddWithValue("@IDRESERVA", IdReserva);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Contenedor.Add(new Clases.DetalleContenedor
                    {
                        Contenedor = reader["CONTENEDOR"].ToString(),
                        IdMaquinaria = Convert.ToInt32(reader["ID_MAQUINARIA"]),
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
            return Contenedor;
        }

        public static List<Clases.ResultadoLeaktest> GetInfoFiltro(int IdContenedor)
        {
            SqlConnection cnn;
            List<Clases.ResultadoLeaktest> Contenedor = new List<Clases.ResultadoLeaktest>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarInfoContenedor @ID_CONTENEDOR", cnn);
                command.Parameters.AddWithValue("@ID_CONTENEDOR", IdContenedor);

                SqlDataReader reader = command.ExecuteReader();

                string controlador;
                string bateria;
                int scrubber;
                string sello_perno1;
                string sello_perno2;
                string sello_tapa;
                int id_reserva;
                string deposito_leaktest;
                string pais_leaktest;

                while (reader.Read())
                {
                    if (reader["CONTROLADOR"] != DBNull.Value)
                    {
                        controlador = reader["CONTROLADOR"].ToString();
                    }
                    else
                    {
                        controlador = "";
                    }

                    if (reader["BATERIA"] != DBNull.Value)
                    {
                        bateria = reader["BATERIA"].ToString();
                    }
                    else
                    {
                        bateria = "";
                    }

                    if (reader["SCRUBBER"] != DBNull.Value)
                    {
                        scrubber = Convert.ToInt32(reader["SCRUBBER"]);
                    }
                    else
                    {
                        scrubber = 0;
                    }

                    if (reader["SELLOPERNO1"] != DBNull.Value)
                    {
                        sello_perno1 = reader["SELLOPERNO1"].ToString();
                    }
                    else
                    {
                        sello_perno1 = "";
                    }

                    if (reader["SELLOPERNO2"] != DBNull.Value)
                    {
                        sello_perno2 = reader["SELLOPERNO2"].ToString();
                    }
                    else
                    {
                        sello_perno2 = "";
                    }

                    if (reader["SELLOTAPA"] != DBNull.Value)
                    {
                        sello_tapa = reader["SELLOTAPA"].ToString();
                    }
                    else
                    {
                        sello_tapa = "";
                    }

                    if (reader["ID_RESERVA"] != DBNull.Value)
                    {
                        id_reserva = Convert.ToInt32(reader["ID_RESERVA"]);
                    }
                    else
                    {
                        id_reserva = 0;
                    }

                    if (reader["PAIS_LEAKTEST"] != DBNull.Value)
                    {
                        pais_leaktest = reader["PAIS_LEAKTEST"].ToString();
                    }
                    else
                    {
                        pais_leaktest = "";
                    }

                    if (reader["DEPOSITO_LEAKTEST"] != DBNull.Value)
                    {
                        deposito_leaktest = reader["DEPOSITO_LEAKTEST"].ToString();
                    }
                    else
                    {
                        deposito_leaktest = "";
                    }

                    DateTime? fecha = null;
                    if (reader["FECHA_CONTROLADOR"] != DBNull.Value)
                    {
                        fecha = Convert.ToDateTime(reader["FECHA_CONTROLADOR"]);
                    }

                    Contenedor.Add(new Clases.ResultadoLeaktest
                    {
                        Controlador = controlador,
                        Bateria = bateria,
                        Scrubber = scrubber,
                        Selloperno1 = sello_perno1,
                        Selloperno2 = sello_perno2,
                        Sellotapa = sello_tapa,
                        IdReserva = id_reserva,
                        Pais = pais_leaktest,
                        Deposito = deposito_leaktest,
                        Modem= reader["MODEM"].ToString(),
                        BateriaModem = reader["BATERIA_MODEM"].ToString(),
                        FechaControlador = fecha,
                        Tecnico = reader["NOMBRETECNICO"].ToString(),
                        TipoLugar = reader["TIPOLUGAR"].ToString(),
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
            return Contenedor;
        }


        public static List<Clases.ResultadoLeaktest> GetInfoFiltroSP(int IdContenedor)
        {
            SqlConnection cnn;
            List<Clases.ResultadoLeaktest> Contenedor = new List<Clases.ResultadoLeaktest>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarLeaktestContenedorSP @IDCONTENEDOR", cnn);
                command.Parameters.AddWithValue("@IDCONTENEDOR", IdContenedor);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    DateTime? fecha = null;
                    if (reader["FECHA_CONTROLADOR"] != DBNull.Value)
                    {
                        fecha = Convert.ToDateTime(reader["FECHA_CONTROLADOR"]);
                    }

                        Contenedor.Add(new Clases.ResultadoLeaktest
                        {
                            Selloperno1 = reader["SELLOPERNO1"].ToString(),
                            Selloperno2 = reader["SELLOPERNO2"].ToString(),
                            Sellotapa = reader["SELLOTAPA"].ToString(),
                            Deposito = reader["NOMBREDEPOSITO"].ToString(),
                            Pais = reader["NOMBREPAIS"].ToString(),
                            Controlador = reader["NUMCONTROLADOR"].ToString(),
                            Bateria = reader["BATERIA"].ToString(),
                            FechaControlador = fecha,
                            Tecnico= reader["NOMBRETECNICO"].ToString(),
                            TipoLugar= reader["TIPOLUGAR"].ToString(),
                            Modem= reader["MODEM"].ToString(),
                            BateriaModem= reader["BATERIA_MODEM"].ToString(),
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
            return Contenedor;
        }

        public static Clases.Contenedor GetIdContedor(string NumContenedor)
        {
            SqlConnection cnn;
            Clases.Contenedor contenedor = new Clases.Contenedor();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT ISNULL(ID_CONTENEDOR, 0) AS ID_CONTENEDOR FROM APLICACIONSERVICIO_CONTENEDORES WHERE CONTENEDOR = @CONTENEDOR;", cnn);
                command.Parameters.AddWithValue("@CONTENEDOR", NumContenedor);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    contenedor.IdContenedor = Convert.ToInt32(reader["ID_CONTENEDOR"]);
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
            return contenedor;
        }

        public static List<Clases.Contenedor> GetContenedoresEditar()
        {
            SqlConnection cnn;
            List<Clases.Contenedor> Contenedor = new List<Clases.Contenedor>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                string consulta = "select * from aplicacionservicio_contenedores";

                SqlCommand command = new SqlCommand(consulta, cnn);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Contenedor.Add(new Clases.Contenedor
                    {
                        IdContenedor = Convert.ToInt32(reader["ID_CONTENEDOR"]),
                        NumeroContenedor = reader["CONTENEDOR"].ToString(),
                        EstadoContenedor = Convert.ToInt32(reader["ESTADO"]),

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
            return Contenedor;
        }

        public static List<Clases.Contenedor> GetContenedoresCancelados()
        {
            SqlConnection cnn;
            List<Clases.Contenedor> Contenedor = new List<Clases.Contenedor>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarContenedoresCancelados @USUARIO", cnn);
                command.Parameters.Add("@USUARIO", SqlDbType.VarChar, 100).Value = HttpContext.Current.Session["user"].ToString();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Contenedor.Add(new Clases.Contenedor
                    {
                        IdContenedor = Convert.ToInt32(reader["ID_CONTENEDOR"]),
                        NumeroContenedor = reader["CONTENEDOR"].ToString(),
                        Fecha = Convert.ToDateTime(reader["FECHA"]),

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
            return Contenedor;
        }

        public static List<Clases.Contenedor> FiltroPorFechasCancelados(int Tipo, DateTime FechaIni, DateTime FechaFin)
        {
            SqlConnection cnn;
            List<Clases.Contenedor> Contenedores = new List<Clases.Contenedor>();
            cnn = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand();
            try
            {
                cnn.Open();
                if (Tipo == 1)
                {
                    command = new SqlCommand("ConsultarContenedoresHistoricosCancelados", cnn);
                }

                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@FECHAINI", SqlDbType.DateTime, 50).Value = FechaIni;
                command.Parameters.Add("@FECHAFIN", SqlDbType.DateTime, 50).Value = FechaFin;

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {


                    Contenedores.Add(new Clases.Contenedor
                    {
                        IdContenedor = Convert.ToInt32(reader["ID_CONTENEDOR"]),
                        NumeroContenedor = reader["CONTENEDOR"].ToString(),
                        Fecha = Convert.ToDateTime(reader["FECHA"]),
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
            return Contenedores;
        }

        public static Clases.Contenedor GetInfoContenedor(string Contenedor)
        {

            SqlConnection cnn;
            Clases.Contenedor Contenedores = new Clases.Contenedor();
            cnn = new SqlConnection(connectionString);

            SqlCommand scCommand = new SqlCommand("GetInfoContenedor", cnn);
            scCommand.CommandType = CommandType.StoredProcedure;
            scCommand.Parameters.Add("@CONTENEDOR", SqlDbType.VarChar, 50).Value = Contenedor;

            try
            {
                if (scCommand.Connection.State == ConnectionState.Closed)
                {
                    scCommand.Connection.Open();
                }
                //scCommand.ExecuteNonQuery();
                SqlDataReader reader = scCommand.ExecuteReader();

                while (reader.Read())
                {
                    Contenedores.IdContenedor = Convert.ToInt32(reader["ID_CONTENEDOR"]);
                    Contenedores.IdMaquinaria = Convert.ToInt32(reader["ID_MAQUINARIA"]);
                    Contenedores.IdCaja = Convert.ToInt32(reader["ID_CAJA"]);
                    Contenedores.AnoContenedor = Convert.ToInt32(reader["ANO"]);
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

            return Contenedores;
        }

        public static int QuitarScrubber(int IdContenedor)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("UPDATE APLICACIONSERVICIO_CONTENEDORES SET SCRUBBER = 1 WHERE ID_CONTENEDOR = @IDCONTENEDOR", cnn);
                command.Parameters.AddWithValue("@IDCONTENEDOR", IdContenedor);

                //SqlDataReader reader = command.ExecuteReader();
                int validar = command.ExecuteNonQuery();
                if (validar == 0)
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
                cnn.Close();
            }
        }

        public static string ObtenerNumContenedor(int IdContenedor)
        {
            string contenedor = "";
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT CONTENEDOR FROM APLICACIONSERVICIO_CONTENEDORES WHERE ID_CONTENEDOR = @IDCONTENEDOR", cnn);
                command.Parameters.AddWithValue("@IDCONTENEDOR", IdContenedor);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (reader["CONTENEDOR"] != DBNull.Value)
                    {
                        contenedor = reader["CONTENEDOR"].ToString();
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

            return contenedor;
        }

        public static int CambiarEstadoContenedor(string contenedor, int estado)
        {
            int respuesta = 0;
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC EditarEstadoContenedor @CONTENEDOR, @ESTADO", cnn);
                command.Parameters.AddWithValue("@CONTENEDOR", contenedor);
                command.Parameters.AddWithValue("@ESTADO", estado);
                respuesta = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            return respuesta;
        }
    }
}