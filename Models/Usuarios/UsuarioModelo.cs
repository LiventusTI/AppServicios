using MySql.Data.MySqlClient;
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

namespace Plataforma.Models.Usuario
{
    public class UsuarioModelo
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        static string connetionString = ConfigurationManager.ConnectionStrings["connetionString"].ConnectionString;

        public static Clases.Usuario VerificarUsuario(string NombreUsuario, string Contrasena)
        {
            Clases.Usuario Usuario = new Clases.Usuario();
            SqlConnection cnn;
            cnn = new SqlConnection(connetionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("Select NOMBREUSUARIO, NOMBRE, APELLIDO, ID_PERFIL, CONTRASENA from USUARIOS WHERE NOMBREUSUARIO = @user AND BINARY_CHECKSUM(CONTRASENA) = BINARY_CHECKSUM(@pass)", cnn);
                command.Parameters.AddWithValue("@user", NombreUsuario);
                command.Parameters.AddWithValue("@pass", Contrasena);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    Usuario.NombreUsuario = reader["NOMBREUSUARIO"].ToString();
                    Usuario.Nombre = reader["NOMBRE"].ToString();
                    Usuario.Apellido = reader["APELLIDO"].ToString();
                    Usuario.IdPerfil = Convert.ToInt32(reader["ID_PERFIL"]);
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
            return Usuario;
        }

        internal static Clases.Usuario GetInfoUsuario(object p)
        {
            throw new NotImplementedException();
        }

        public static Clases.Usuario GetInfoUsuario(string NombreUsuario)
        {
            Clases.Usuario Usuario = new Clases.Usuario();
            SqlConnection cnn;
            cnn = new SqlConnection(connetionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("Select NOMBREUSUARIO, NOMBRE, APELLIDO, ID_PERFIL, CONTRASENA from USUARIOS WHERE NOMBREUSUARIO = @user", cnn);
                command.Parameters.AddWithValue("@user", NombreUsuario);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    Usuario.NombreUsuario = reader["NOMBREUSUARIO"].ToString();
                    Usuario.Nombre = reader["NOMBRE"].ToString();
                    Usuario.Apellido = reader["APELLIDO"].ToString();
                    Usuario.IdPerfil = Convert.ToInt32(reader["ID_PERFIL"]);
                    Usuario.Contrasena = reader["CONTRASENA"].ToString();
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
            return Usuario;
        }

        public static bool EditarContrasena(string NombreUsuario, string pass)
        {
            Clases.Usuario Usuario = new Clases.Usuario();
            SqlConnection cnn;
            cnn = new SqlConnection(connetionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("UPDATE USUARIOS SET CONTRASENA = @pass WHERE NOMBREUSUARIO = @user", cnn);
                command.Parameters.AddWithValue("@user", NombreUsuario);
                command.Parameters.AddWithValue("@pass", pass);
                //SqlDataReader reader = command.ExecuteReader();
                int validar = command.ExecuteNonQuery();
                if (validar == 0)
                {
                    return false;
                }
                else
                {
                    return true;
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

        public static bool EditarContrasenaDMS(string NombreUsuario, string pass)
        {
            string _strDBName = "URI=file:C:/xampp/htdocs/dms/data/content.db";
            IDbConnection _dbc = new SQLiteConnection(_strDBName);
            if (NombreUsuario == "ADMIN")
            {
                NombreUsuario = "admin";
            }
            try {   
                _dbc.Open();
                IDbCommand _dbcm = _dbc.CreateCommand();
                _dbcm.CommandText = "UPDATE `tblUsers` SET `pwd` = '" + GetMD5(pass) + "' WHERE `login` = '" + NombreUsuario + "' ;";
                IDataReader _dbr = _dbcm.ExecuteReader();
                if (_dbr.RecordsAffected == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally {
                _dbc.Close();
            }
        }

        public static string GetMD5(string str)
        {
            MD5 md5 = MD5CryptoServiceProvider.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = md5.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

        public static List<Clases.Tecnico> GetTecnicosByServiceProvider(int IdServiceProvider)
        {
            SqlConnection cnn;
            List<Clases.Tecnico> Tecnico = new List<Clases.Tecnico>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT AplicacionServicio_Usuario.NOMBRE, AplicacionServicio_Usuario.APELLIDO, AplicacionServicio_Usuario.ID_USUARIO FROM AplicacionServicio_Usuario, AplicacionServicio_Perfil WHERE AplicacionServicio_Usuario.ID_SERVICEPROVIDER = @IDSERVICEPROVIDER AND  AplicacionServicio_Usuario.ID_PERFIL = AplicacionServicio_Perfil.ID_PERFIL AND AplicacionServicio_Usuario.ESTADO = 0", cnn);
                command.Parameters.AddWithValue("@IDSERVICEPROVIDER", IdServiceProvider);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Tecnico.Add(new Clases.Tecnico
                    {
                        IdTecnico = Convert.ToInt32(reader["ID_USUARIO"]),
                        NombreTecnico = reader["NOMBRE"].ToString() +" "+ reader["APELLIDO"].ToString()
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
            return Tecnico;
        }

        public static List<Clases.Tecnico> GetTecnicos()
        {
            SqlConnection cnn;
            List<Clases.Tecnico> Tecnico = new List<Clases.Tecnico>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT AplicacionServicio_Usuario.NOMBRE, AplicacionServicio_Usuario.APELLIDO, AplicacionServicio_Usuario.ID_USUARIO FROM AplicacionServicio_Usuario WHERE AplicacionServicio_Usuario.ID_PERFIL = 1 OR AplicacionServicio_Usuario.ID_PERFIL = 2 OR AplicacionServicio_Usuario.ID_PERFIL = 3 AND AplicacionServicio_Usuario.ESTADO = 0", cnn);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Tecnico.Add(new Clases.Tecnico
                    {
                        IdTecnico = Convert.ToInt32(reader["ID_USUARIO"]),
                        NombreTecnico = reader["NOMBRE"].ToString() + " " + reader["APELLIDO"].ToString()
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
            return Tecnico;
        }

        public static List<Clases.Tecnico> GetTecnicosSP()
        {
            SqlConnection cnn;
            List<Clases.Tecnico> Tecnico = new List<Clases.Tecnico>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT ID_USUARIO, NOMBREUSUARIO, NOMBRE, APELLIDO FROM AplicacionServicio_Usuario WHERE ESTADO=0 AND (ID_PERFIL=1 OR ID_PERFIL=2 OR ID_PERFIL=3 OR ID_PERFIL = 1020 OR ID_PERFIL = 1021 OR ID_PERFIL=1022) AND ID_SERVICEPROVIDER=@ID_SERVICEPROVIDER", cnn);
                command.Parameters.AddWithValue("@ID_SERVICEPROVIDER", HttpContext.Current.Session["SP"].ToString());
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Tecnico.Add(new Clases.Tecnico
                    {
                        IdTecnico = Convert.ToInt32(reader["ID_USUARIO"]),
                        NombreTecnico = reader["NOMBRE"].ToString() + " " + reader["APELLIDO"].ToString()
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
            return Tecnico;
        }

        public static List<Clases.Tecnico> GetInfoTecnico(int IdTecnico)
        {
            SqlConnection cnn;
            List<Clases.Tecnico> Tecnico = new List<Clases.Tecnico>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT SP.ID_PAIS, SP.ID_SERVICEPROVIDER  FROM AplicacionServicio_ServiceProvider SP WHERE ID_SERVICEPROVIDER = (SELECT ID_SERVICEPROVIDER FROM AplicacionServicio_Usuario WHERE ID_USUARIO=@ID_USUARIO AND ESTADO=0 AND ID_PERFIL=1)", cnn);
                command.Parameters.AddWithValue("@ID_USUARIO", IdTecnico);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Tecnico.Add(new Clases.Tecnico
                    {
                        IdSP= Convert.ToInt32(reader["ID_SERVICEPROVIDER"]),
                        IdPaisSP= Convert.ToInt32(reader["ID_PAIS"])
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
            return Tecnico;
        }

        public static int GetIdTecnico(string Nombre)
        {
            SqlConnection cnn;
            int id = 0;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT ID_USUARIO FROM AplicacionServicio_Usuario Where AplicacionServicio_Usuario.NOMBREUSUARIO = @NOMBRE", cnn);
                command.Parameters.AddWithValue("@NOMBRE", Nombre);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    id = Convert.ToInt32(reader["ID_USUARIO"]);
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

        public static Clases.Usuario GetPerfilByUser(string NombreUsuario)
        {
            Clases.Usuario Usuario = new Clases.Usuario();
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT ID_USUARIO, ID_PERFIL, EMAIL, ID_SERVICEPROVIDER FROM AplicacionServicio_Usuario WHERE NOMBREUSUARIO = @user;", cnn);
                command.Parameters.AddWithValue("@user", NombreUsuario);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    Usuario.IdPerfil = Convert.ToInt32(reader["ID_PERFIL"]);
                    
                    if (reader["ID_SERVICEPROVIDER"] != DBNull.Value)
                    {
                        Usuario.IdServiceProvider = Convert.ToInt32(reader["ID_SERVICEPROVIDER"]);
                    }
                    else
                    {
                        Usuario.IdServiceProvider = 0;
                    }

                    if (reader["EMAIL"] != DBNull.Value)
                    {
                        Usuario.Correo = reader["EMAIL"].ToString(); ;
                    }
                    else
                    {
                        Usuario.Correo = "";
                    }
                    Usuario.IdUsuario= Convert.ToInt32(reader["ID_USUARIO"]);
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
            return Usuario;
        }


        public static List<Clases.Coordinador> GetUsuarioRetrieval()
        {
            SqlConnection cnn;
            List<Clases.Coordinador> Coordinadores = new List<Clases.Coordinador>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("select * from AplicacionServicio_Usuario where ID_PERFIL = 5 OR NOMBREUSUARIO = 'SSEPULVEDA' OR NOMBREUSUARIO = 'MOLIVARES';", cnn);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Coordinadores.Add(new Clases.Coordinador
                    {
                        IdUsuario = Convert.ToInt32(reader["ID_USUARIO"]),
                        NombreUsuario = reader["NOMBREUSUARIO"].ToString(),
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
            return Coordinadores;
        }

        public static int AgregarUsuario(string NombreUsuario, string Contrasena, string Cliente, int Tipo, string Email)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            int result = 0;
            SqlCommand scCommand = new SqlCommand("AgregarUsuario", cnn);
            scCommand.CommandType = CommandType.StoredProcedure;
            scCommand.Parameters.Add("@NOMBREUSUARIO", SqlDbType.VarChar, 100).Value = NombreUsuario;
            scCommand.Parameters.Add("@CONTRASENA", SqlDbType.VarChar, 100).Value = Contrasena;
            scCommand.Parameters.Add("@CLIENTE", SqlDbType.VarChar, 100).Value = Cliente;
            scCommand.Parameters.Add("@TIPO", SqlDbType.Int, 50).Value = Tipo;
            scCommand.Parameters.Add("@EMAIL", SqlDbType.VarChar, 100).Value = Email;

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

        public static List<Clases.Clientes> GetClientes()
        {
            SqlConnection cnn;
            List<Clases.Clientes> Clientes = new List<Clases.Clientes>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.GetClientes", cnn);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Clientes.Add(new Clases.Clientes
                    {
                        IdUsuario = Convert.ToInt32(reader["ID_USUARIO"]),
                        NombreUsuario = reader["NOMBREUSUARIO"].ToString(),
                        Email = reader["EMAIL"].ToString(),
                        Tipo = reader["TIPOEMPRESA"].ToString(),
                        Estado = reader["ESTADO"].ToString(),
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

            return Clientes;
        }

        public static List<string> GetInfoClienteByServicio(int servicio)
        {
            SqlConnection cnn;
            List<string> email = new List<string>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT EMAIL FROM AplicacionServicio_Contacto WHERE ID_TIPO_CLIENTE=1 AND ID_CLIENTE=(SELECT ID_EXPORTADOR FROM AplicacionServicio_Servicio1 WHERE ID_SERVICIO=@ID_SERVICIO)", cnn);
                command.Parameters.AddWithValue("@ID_SERVICIO", servicio);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    email.Add(reader["EMAIL"].ToString());
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
            return email;
        }

        public static string GetInfoComercialByServicio(int servicio)
        {
            SqlConnection cnn;
            string email = "";
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT EMAIL FROM AplicacionServicio_Usuario U, AplicacionServicio_UsuarioPais UP, AplicacionServicio_Exportador E, AplicacionServicio_Pais P, AplicacionServicio_Servicio1 S WHERE S.ID_EXPORTADOR=E.ID_EXPORTADOR AND E.ID_PAIS=P.ID_PAIS AND P.ID_PAIS=UP.ID_PAIS AND UP.ID_USUARIO=U.ID_USUARIO AND S.ID_SERVICIO=@ID_SERVICIO ", cnn);
                command.Parameters.AddWithValue("@ID_SERVICIO", servicio);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    email = reader["EMAIL"].ToString();
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
            return email;
        }
    }
}