using Plataforma.Models;
using Plataforma.Models.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Plataforma.Controllers
{
    public class UsuariosController : Controller
    {
        // GET: Usuarios
        public ActionResult VisualizarUsuarios()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        // GET: Usuarios/Details/5
        public ActionResult ModificarDatos()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            Clases.Usuario Usuario = new Clases.Usuario();
            Usuario = UsuarioModelo.GetInfoUsuario(Session["user"].ToString().ToUpper());
            ViewBag.NombreUsuario = Usuario.NombreUsuario;
            ViewBag.Nombre = Usuario.Nombre;
            ViewBag.Contrasena = Usuario.Contrasena;
            ViewBag.Apellido = Usuario.Apellido;
            return View();
        }

        public ActionResult EditarContrasena(string pass)
        {
            bool Valor1 = false;
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            //Editar Contraseña en BD INTRANET
            bool Valor = UsuarioModelo.EditarContrasena(Session["user"].ToString().ToUpper(), pass);
            //Editar Contraseña en BD DMS
            if (Convert.ToInt32(Session["Perfil"]) == 10) {
                 Valor1 = true;
            } else {
                 Valor1 = UsuarioModelo.EditarContrasenaDMS(Session["user"].ToString().ToUpper(), pass);
            }
            if (Valor == true && Valor1 == true) {
                Response.Write("<script>alert('Contraseña Modificada Correctamente, Vuelva a Iniciar Sesión');</script>");
                return View("../Home/Login");
            } else {
                Response.Write("<script>alert('No se puede actualizar Contraseña');</script>");
                return View("../Home/Login");
            }
        }

        [HttpPost]
        public string GetTecnicos()
        {
            List<Clases.Tecnico> Tecnicos = new List<Clases.Tecnico>();
            Tecnicos = UsuarioModelo.GetTecnicos();
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Tecnicos);
            return datos;
        }

        [HttpPost]
        public string GetTecnicosSP()
        {
            List<Clases.Tecnico> Tecnicos = new List<Clases.Tecnico>();
            Tecnicos = UsuarioModelo.GetTecnicosSP();
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Tecnicos);
            return datos;
        }

        [HttpPost]
        public string GetInfoTecnico(int IdTecnico)
        {
            List<Clases.Tecnico> Tecnicos = new List<Clases.Tecnico>();
            Tecnicos = UsuarioModelo.GetInfoTecnico(IdTecnico);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Tecnicos);
            return datos;
        }

        [HttpPost]
        public string GetTecnicosByServiceProvider(int IdServiceProvider)
        {
            List<Clases.Tecnico> Tecnicos = new List<Clases.Tecnico>();
            Tecnicos = UsuarioModelo.GetTecnicosByServiceProvider(IdServiceProvider);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Tecnicos);
            return datos;
        }

        [HttpPost]
        public string GetUsuarioRetrieval()
        {
            List<Clases.Coordinador> Tecnicos = new List<Clases.Coordinador>();
            Tecnicos = UsuarioModelo.GetUsuarioRetrieval();
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Tecnicos);
            return datos;
        }
    }
}
