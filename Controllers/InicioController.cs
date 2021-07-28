using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using Inluirme_Proyecto.Models;

namespace Inluirme_Proyecto.Controllers
{
    public class InicioController : Controller
    {
        private PROYECTO_INCLUIREntities db = new PROYECTO_INCLUIREntities();

        // GET: Inicio
        public ActionResult Index(string message = "")
        {
            ViewBag.message = message;
            return View();
        }
        [HttpPost]
        public ActionResult Login(string usuario,string clave)
        {
            if(!string.IsNullOrEmpty(usuario) && !string.IsNullOrEmpty(clave))
            {
                var user = db.Usuarios.FirstOrDefault(e => e.usuario == usuario && e.contra == clave);

                if(user != null)
                {
                    FormsAuthentication.SetAuthCookie(user.nombre, true);
                    int id = user.id;
                    return RedirectToAction("Perfil", "Usuarios", new { id });
                }
                var emp = db.Empresas.FirstOrDefault(e => e.usuario == usuario && e.contra == clave);

                if(emp != null)
                {
                    FormsAuthentication.SetAuthCookie(emp.nombre, true);
                    int id = emp.id;
                    return RedirectToAction("Perfil", "Empresas", new { id });
                }

                var uni = db.Universidades.FirstOrDefault(e => e.usuario == usuario && e.contra == clave);

                if(uni != null)
                {
                    FormsAuthentication.SetAuthCookie(uni.nombre, true);
                    int id = uni.id;
                    return RedirectToAction("Perfil", "Universidades", new { id });
                }

                else
                {
                    return RedirectToAction("Index", new {message = "No encontramos tu usuario o contraseña" });
                }
            }

            return RedirectToAction("Index", new { message = "Complete los campos para ingresar a su perfil" });
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", new { message = "Session terminada nos vemos pronto!!!" });
        }

        public ActionResult Capacitacion(string message="")
        {
            ViewBag.message = message;
            return View(db.Capacitaciones.ToList());
        }

        public ActionResult Anotarme(int id)
        {         
            Capacitaciones capacitaciones = db.Capacitaciones.Find(id);
            return View(capacitaciones);
        }

        [HttpPost]
        public ActionResult Anotarme(int idCapacitacion, string usuario,string clave)
        {
            var user = db.Usuarios.FirstOrDefault(e => e.usuario == usuario && e.contra == clave );

            if(user != null)
            {
                int id = user.id;

                var verificartea = db.teaCapacitar.FirstOrDefault(e => e.idCapacitacion == idCapacitacion && e.idUsuarios == id);

                if (verificartea != null)
                    return RedirectToAction("Capacitacion", new { message = "El usuario está inscripto en este curso, no se puede volver a pre-inscribir" });

                teaCapacitar tea = new teaCapacitar();
                tea.idCapacitacion = idCapacitacion;
                tea.idUsuarios = id;
                db.teaCapacitar.Add(tea);
                db.SaveChanges();

                return RedirectToAction("Capacitacion",new { message = "Usuario pre-inscripto en la capacitacion!!!" });
            }
            else
            {

                return RedirectToAction("Capacitacion", new { message = "Usuario no encontrado" });
            }
        }


        public ActionResult Contactos()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contactos(string nombre, string correo, string consulta, string tarea)
        {
            string email = "avel40m@gmail.com";
            string resultado = consulta + ": " + nombre + ", " + tarea;

            WebMail.Send(email, correo, resultado, null, null, null, true, null, null, null, null, null, null);

            return RedirectToAction("Index", "Inicio",new { message = "El mensaje fue enviado correctamente..." });
 
        }


        public ActionResult VerPuestos(string mensaje="")
        {
            ViewBag.mensaje = mensaje;

            var list = db.Encuestas.SqlQuery("select * from Encuestas inner join Empresas on Encuestas.Empresa_id=Empresas.id order by fecha desc").ToList();
            return View(list);
        }

        public ActionResult PostularmeBusqueda(int id)
        {
            ViewBag.id = id;
            return View();
        }

        [HttpPost]
        public ActionResult VerificacionU(int idEncuesta,string usuario, string clave)
        {
        
            var user = db.Usuarios.FirstOrDefault(e => e.usuario == usuario && e.contra == clave);

            if(user != null)
            {
                 int idUsuario = user.id;

                var verificar = db.Preguntas.FirstOrDefault(e => e.Encuesta_id == idEncuesta && e.Usuario_id == idUsuario);
                if (verificar != null)
                    return RedirectToAction("VerPuestos", "Inicio",new { mensaje = "Está cuenta realizo la encuesta, por tal motivo no permite realizar nuevamente la encuesta, muchas gracias!!!" });


                return RedirectToAction("VerEncuesta", "Inicio", new {idEncuesta,idUsuario });
            }

            return RedirectToAction("VerPuestos", "Inicio", new { mensaje = "No se encontro el usuario en nuestro sistema, verifique su cuenta" });
            }


        public ActionResult VerEncuesta(int idEncuesta,int idUsuario)
        {
            ViewBag.Encuesta = idEncuesta;
            ViewBag.Usuario = idUsuario;

            var list = db.Preguntas.SqlQuery("select top 1 * from Preguntas where Preguntas.Encuesta_id = {0}", idEncuesta).ToList();
            
            return View(list);
        }

        [HttpPost]
        public ActionResult VerEncuestas([Bind(Include = "Encuesta_id,Usuario_id,descripcion,respuesta,descripcion_1,respuesta_1,descripcion_2,respuesta_2")] Preguntas preguntas)
        {
            db.Preguntas.Add(preguntas);
            db.SaveChanges();

            return RedirectToAction("VerPuestos", "Inicio",new { mensaje ="Se guardo correctamente su respuesta!!!" });
        }

        public ActionResult VerBecas(string mensaje = "")
        {
            ViewBag.mensaje = mensaje;
            var list = db.Encuestas.SqlQuery("select * from Encuestas inner join Universidades on Encuestas.Universidad_id = Universidades.id order by fecha desc").ToList();
            return View(list);
        }

        public ActionResult PostularmeBecas(int id)
        {
            ViewBag.id = id;
            return View();
        }

        [HttpPost]
        public ActionResult VerificacionB(int idEncuesta, string usuario, string clave)
        {

            var user = db.Usuarios.FirstOrDefault(e => e.usuario == usuario && e.contra == clave);

            if (user != null)
            {
                int idUsuario = user.id;

                var verificar = db.Preguntas.FirstOrDefault(e => e.Encuesta_id == idEncuesta && e.Usuario_id == idUsuario);
                if (verificar != null)
                    return RedirectToAction("VerBecas", "Inicio", new { mensaje = "Está cuenta realizo la encuesta, por tal motivo no permite realizar nuevamente la encuesta, muchas gracias!!!" });


                return RedirectToAction("VerEncuestasBecas", "Inicio", new { idEncuesta, idUsuario });
            }

            return RedirectToAction("VerBecas", "Inicio", new { mensaje = "No se encontro el usuario en nuestro sistema, verifique su cuenta" });

        }

        public ActionResult VerEncuestasBecas(int idEncuesta,int idUsuario)
        {
            ViewBag.Encuesta = idEncuesta;
            ViewBag.Usuario = idUsuario;

            var list = db.Preguntas.SqlQuery("select top 1 * from Preguntas where Preguntas.Encuesta_id = {0}", idEncuesta).ToList();

            return View(list);
        }

        [HttpPost]
        public ActionResult VerEncuestasBecas([Bind(Include = "Encuesta_id,Usuario_id,descripcion,respuesta,descripcion_1,respuesta_1,descripcion_2,respuesta_2")] Preguntas preguntas)
        {
            db.Preguntas.Add(preguntas);
            db.SaveChanges();

            return RedirectToAction("VerBecas", "Inicio", new { mensaje = "Se guardo correctamente su respuesta!!!" });
        }
    }
}