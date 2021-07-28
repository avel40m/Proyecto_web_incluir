using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inluirme_Proyecto.Models;

namespace Inluirme_Proyecto.Controllers
{
    public class CapacitarController : Controller
    {
        private PROYECTO_INCLUIREntities db = new PROYECTO_INCLUIREntities();

        [Authorize]
        // GET: Capacitar
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        public ActionResult Crear(int id)
        {
            Empresas empresas = db.Empresas.Find(id);
            return View(empresas);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Crear(FormCollection collection)
        {
            Capacitaciones capacitaciones = new Capacitaciones();
            capacitaciones.Empresa_id = int.Parse(collection["Empresa_id"]);
            capacitaciones.nombre = collection["nombre"];
            capacitaciones.descripcion = collection["descripcion"];
            capacitaciones.fecha = DateTime.Parse(collection["fecha"]);
            capacitaciones.cantidad = int.Parse(collection["cantidad"]);
            db.Capacitaciones.Add(capacitaciones);
            db.SaveChanges();
            return RedirectToAction("Capacitaciones", "Capacitar",new { id = capacitaciones.Empresa_id });
        }

        [Authorize]
        public ActionResult Capacitaciones(int id)
        {
            ViewBag.id = id;
            var list = db.Capacitaciones.SqlQuery("SELECT * FROM Capacitaciones where Empresa_id = {0}", id).ToList();

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
        public ActionResult Editar([Bind(Include ="id,Empresa_id,nombre,descripcion,fecha,cantidad")]Capacitaciones capacitaciones)
        {
            

            if (ModelState.IsValid)
            {
                db.Entry(capacitaciones).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Capacitaciones","Capacitar",new { id = capacitaciones.Empresa_id});
            }
            return View(capacitaciones);
        }

        [Authorize]
        public ActionResult Eliminar(int id, int Empresa_id)
        {
            var capacitaciones = db.Capacitaciones.Find(id);
            db.Capacitaciones.Remove(capacitaciones);
            db.SaveChanges();
            return RedirectToAction("Capacitaciones", "Capacitar",new { id = Empresa_id });
        }
        
        [Authorize]
        public ActionResult VerUsuarios(int id,int Empresa_id)
        {
            ViewBag.id = Empresa_id;

            var list = db.Usuarios.SqlQuery("select * from Usuarios inner join teaCapacitar on Usuarios.id = teaCapacitar.idUsuarios where teaCapacitar.idCapacitacion = {0}", id).ToList();

            return View(list);
        }
    }
}