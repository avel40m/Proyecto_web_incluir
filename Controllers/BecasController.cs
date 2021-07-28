using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inluirme_Proyecto.Models;
namespace Inluirme_Proyecto.Controllers
{
    public class BecasController : Controller
    {
        PROYECTO_INCLUIREntities db = new PROYECTO_INCLUIREntities();
        
        [Authorize]
        public ActionResult VerBecas(int id)
        {
            ViewBag.id = id;

            var list = db.Encuestas.SqlQuery("select * from Encuestas where Encuestas.Universidad_id = {0}", id).ToList();

            return View(list);
        }

        [Authorize]
        public ActionResult Crear(int id)
        {
            ViewBag.id = id;
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Crear([Bind(Include = "Universidad_id,nombre,descripcion,fecha")] Encuestas encuestas)
        {
            if (ModelState.IsValid)
            {
                db.Encuestas.Add(encuestas);
                db.SaveChanges();
                return RedirectToAction("Preguntas","Becas", new { id = encuestas.id , Universidad_id = encuestas.Universidad_id });
            }
            return View();
        }

        [Authorize]
        public ActionResult Preguntas(int id, int Universidad_id)
        {
            ViewBag.id = id;
            ViewBag.Universidad_id = Universidad_id;
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Preguntas(FormCollection collection)
        {
            int id = int.Parse(collection["Universidad_id"]);

            Preguntas preguntas = new Preguntas();
            preguntas.Encuesta_id = int.Parse(collection["Encuesta_id"]);
            preguntas.descripcion = collection["descripcion"];
            preguntas.descripcion_1 = collection["descripcion_1"];
            preguntas.descripcion_2 = collection["descripcion_2"];
            db.Preguntas.Add(preguntas);
            db.SaveChanges();

            return RedirectToAction("VerBecas", "Becas", new { id });
        }

        [Authorize]
        public ActionResult Eliminar(int id,int Universidad_id)
        {
            var capacitaciones = db.Encuestas.Find(id);
            db.Encuestas.Remove(capacitaciones);
            db.SaveChanges();
            return RedirectToAction("VerBecas","Becas",new { id = Universidad_id });
        }

        [Authorize]
        public ActionResult VerPreguntas(int id, int Universidad_id)
        {
            ViewBag.Universidad_id = Universidad_id;

            var list = db.Preguntas.SqlQuery("select top 1 * from Preguntas where Encuesta_id = {0}", id).ToList();

            return View(list);
        }

        [Authorize]
        public ActionResult VerPostulantes(int id, int universidadid)
        {
            ViewBag.universidadid = universidadid;

            var list = db.Preguntas.SqlQuery("select * from Preguntas inner join Usuarios on Preguntas.Usuario_id = Usuarios.id where Encuesta_id = {0}", id).ToList();
            return View(list);
        }
    }
}