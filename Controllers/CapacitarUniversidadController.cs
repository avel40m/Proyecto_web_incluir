using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inluirme_Proyecto.Models;

namespace Inluirme_Proyecto.Controllers
{
    public class CapacitarUniversidadController : Controller
    {
        private PROYECTO_INCLUIREntities db = new PROYECTO_INCLUIREntities();
        // GET: CapacitarUniversidad
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Crear(int id)
        {
            Universidades universidades = db.Universidades.Find(id);
            return View(universidades);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Crear(FormCollection collection)
        {
            Capacitaciones capacitaciones = new Capacitaciones();
            capacitaciones.Universidad_id = int.Parse(collection["Universidad_id"]);
            capacitaciones.nombre = collection["nombre"];
            capacitaciones.descripcion = collection["descripcion"];
            capacitaciones.fecha = DateTime.Parse(collection["fecha"]);
            capacitaciones.cantidad = int.Parse(collection["cantidad"]);
            db.Capacitaciones.Add(capacitaciones);
            db.SaveChanges();
            return RedirectToAction("Capacitaciones", "CapacitarUniversidad", new { id = capacitaciones.Universidad_id });
        }

        [Authorize]
        public ActionResult Capacitaciones(int id)
        {
            ViewBag.id = id;
            var list = db.Capacitaciones.SqlQuery("select * from Capacitaciones where Capacitaciones.Universidad_id = {0}", id).ToList();
            return View(list);
        }

        [Authorize]
        public ActionResult Editar(int id)
        {
            Capacitaciones capacitaciones = db.Capacitaciones.Find(id);
            return View(capacitaciones);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Editar([Bind(Include = "id,Universidad_id,nombre,descripcion,fecha,cantidad")] Capacitaciones capacitaciones)
        {


            if (ModelState.IsValid)
            {
                db.Entry(capacitaciones).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Capacitaciones", "CapacitarUniversidad", new { id = capacitaciones.Universidad_id });
            }
            return View(capacitaciones);
        }

        [Authorize]
        public ActionResult Eliminar(int id,int universidad_id)
        {
            var capacitaciones = db.Capacitaciones.Find(id);
            db.Capacitaciones.Remove(capacitaciones);
            db.SaveChanges();
            return RedirectToAction("Capacitaciones", "CapacitarUniversidad", new { id = universidad_id });
        }

        [Authorize]
        public ActionResult VerUsuario(int id,int Universidad_id)
        {
            ViewBag.id = Universidad_id;

            var list = db.Usuarios.SqlQuery("select * from Usuarios inner join teaCapacitar on Usuarios.id = teaCapacitar.idUsuarios where idCapacitacion = {0}", id).ToList();
            return View(list);
        }
    }
}
