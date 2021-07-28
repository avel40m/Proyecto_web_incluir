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
    public class EmpresasController : Controller
    {
        private PROYECTO_INCLUIREntities db = new PROYECTO_INCLUIREntities();
        // GET: Empresas
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registrar([Bind(Include ="nombre,cuit,provincia,ciudad,domicilio,telefono,mail,usuario,contra")] Empresas empresas)
        {
            if (ModelState.IsValid)
            {
                db.Empresas.Add(empresas);
                db.SaveChanges();
                return RedirectToAction("Index", "Inicio", new { message = "La empresa fue registrado correctamente" });
            }
            return View();
        }

        [Authorize]
        public ActionResult Perfil(int id)
        {
            Empresas empresas = db.Empresas.Find(id);
            return View(empresas);
        }

        [Authorize]
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empresas empresas = db.Empresas.Find(id);
            if (empresas == null)
            {
                return HttpNotFound();
            }
            return View(empresas);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "id,nombre,cuit,provincia,ciudad,domicilio,telefono,mail,usuario,contra")] Empresas empresas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empresas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Perfil","Empresas",new { id = empresas.id });
            }
            return View(empresas);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Eliminar(int? id)
        {
            var empresa = db.Empresas.Find(id);
            db.Empresas.Remove(empresa);
            db.SaveChanges();
            return RedirectToAction("Index", "Inicio", new { message = "La empresa fue eliminada con exito!!!" });
        }





    }
}