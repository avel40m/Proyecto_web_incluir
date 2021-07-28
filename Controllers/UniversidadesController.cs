using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Inluirme_Proyecto.Models;

namespace Inluirme_Proyecto.Controllers
{
    public class UniversidadesController : Controller
    {
        private PROYECTO_INCLUIREntities db = new PROYECTO_INCLUIREntities();
        // GET: Universidades
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registrar([Bind(Include ="nombre,cuit,provincia,ciudad,domicilio,telefono,mail,usuario,contra")]Universidades universidades)
        {
            if (ModelState.IsValid)
            {
                db.Universidades.Add(universidades);
                db.SaveChanges();
                return RedirectToAction("Index", "Inicio", new { message = "La universidad fue registrado correctamente" });
            }
            return View();
        }

        [Authorize]
        public ActionResult Perfil(int id)
        {
            Universidades universidades = db.Universidades.Find(id);
            return View(universidades);
        }

        [Authorize]
        public ActionResult Editar(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Universidades universidades = db.Universidades.Find(id);
            if(universidades == null)
            {
                return HttpNotFound();
            }
            return View(universidades);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "id,nombre,cuit,provincia,ciudad,domicilio,telefono,mail,usuario,contra")] Universidades universidades)
        {
            if (ModelState.IsValid)
            {
                db.Entry(universidades).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Perfil", "Universidades", new { id = universidades.id });
            }
            return View(universidades);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Eliminar(int? id)
        {
            var universidad = db.Universidades.Find(id);
            db.Universidades.Remove(universidad);
            db.SaveChanges();
            return RedirectToAction("Index", "Inicio", new { message = "La universidad fue eliminada con exito!!!" });
        }
    }
}