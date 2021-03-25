using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Plataforma.Controllers
{
    public class AplicacionesController : Controller
    {
        // GET: Aplicaciones
        public ActionResult AplicacionTecnica()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        public ActionResult BI()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        public ActionResult BI2()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        public ActionResult Comparacion()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        public ActionResult CRM()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        public ActionResult DMS()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            ViewBag.User = Session["User"].ToString().ToUpper();
            ViewBag.Pass = Session["Pass"];
            return View();
        }

        // GET: Aplicaciones/Details/5
        public ActionResult ConstruccionServicios()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }

            return View();
        }

        // GET: Aplicaciones/Details/5
        public ActionResult ConstruccionModulos()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }

            return View();
        }

    }
}
