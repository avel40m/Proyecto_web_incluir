using Inluirme_Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Inluirme_Proyecto.Controllers
{
    public class UsuariosController : Controller
    {
        private PROYECTO_INCLUIREntities db = new PROYECTO_INCLUIREntities();
        // GET: Usuario
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registrar([Bind(Include = "nombre,apellido,dni,telefono,usuario,contra,Discapacidad,Empleo,Conocimiento")] Usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                db.Usuarios.Add(usuarios);
                db.SaveChanges();
                return RedirectToAction("Index", "Inicio", new { message = "El usuario fue registrado correctamente" });
            }
            return View();
        }

        [Authorize]
        public ActionResult Perfil(int? id)
        {
            Usuarios usuarios = db.Usuarios.Find(id);
            return View(usuarios);
        }


        [Authorize]
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios= db.Usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            return View(usuarios);
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "id,nombre,apellido,dni,telefono,usuario,contra,Discapacidad,Empleo,Conocimiento")] Usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuarios).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Perfil","Usuarios",new { id = usuarios.id });
            }
            return View(usuarios);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Eliminar(int? id)
        {
            var usuario = db.Usuarios.Find(id);
            db.Usuarios.Remove(usuario);
            db.SaveChanges();
            return RedirectToAction("Index", "Inicio", new { message = "El usuario fue eliminado con exito!!!" });
        }

    }
}