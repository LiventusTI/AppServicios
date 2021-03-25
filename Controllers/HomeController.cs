using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Plataforma.Models;
using Plataforma.Models.Usuario;
using System.Web.UI;

namespace Plataforma.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index(string user, string pass, string lang)
        {
            
            Clases.Usuario Usuario = new Clases.Usuario();

            if (user == null && pass == null && lang == null) {
                Session.Add("User", Session["User"]);
                Session.Add("Pass", Session["Pass"]);
                Session.Add("lang", Session["lang"]);
                Session.Add("Nombre", Session["Nombre"]);
                Session.Add("Perfil", Session["Perfil"]);
                Session.Add("SP", Session["SP"]);
                Session.Add("Correo", Session["Correo"]);
                return View();
            }

            if (user == "admin")
            {
                string auxuser = user;
            }
            else {
                string auxuser = user.ToUpper();
            }

            Usuario = UsuarioModelo.VerificarUsuario(user, pass);
            Session.Add("Perfil", Usuario.IdPerfil);

            int perfilaplicacion = 0;
            if (Usuario.NombreUsuario != null)
            {
                Clases.Usuario usuario = new Clases.Usuario();
                usuario = UsuarioModelo.GetPerfilByUser(Usuario.NombreUsuario);
                perfilaplicacion = usuario.IdPerfil;
                Session.Add("PerfilInterior", perfilaplicacion);
                Session.Add("SP", usuario.IdServiceProvider);
                Session.Add("Correo", usuario.Correo);
            }
            else {
                Response.Write("<script>alert('Usuario o Contraseña Incorrectos');</script>");
                return View("Login");

            }

            if (Usuario.NombreUsuario != null)
            {
                if (user == null)
                {
                    Session.Add("User", Session["User"]);
                }
                else if (user == "")
                {
                    Session.Add("User", Session["User"]);
                }
                else
                {
                    Session.Add("User", user);
                }

                if (pass == null)
                {
                    Session.Add("Pass", Session["Pass"]);
                }
                else if (pass == "")
                {
                    Session.Add("Pass", Session["Pass"]);
                }
                else
                {
                    Session.Add("Pass", pass);
                }

                if (lang == null)
                {
                    Session.Add("lang", Session["lang"]);
                }
                else if (lang == "")
                {
                    Session.Add("lang", Session["lang"]);
                }
                else
                {
                    Session.Add("lang", lang);
                }

                Session.Add("Nombre", Usuario.Nombre + " " + Usuario.Apellido);

                if (Session["User"] == null)
                {
                    return View("Login");
                }


                if (Session["Nombre"] == null)
                {
                    return View("Login");
                }
                return View();

            }else{

                Response.Write("<script>alert('Usuario o Contraseña Incorrectos');</script>");
                return View("Login");
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}