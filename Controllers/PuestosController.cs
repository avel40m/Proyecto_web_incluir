using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inluirme_Proyecto.Models;

namespace Inluirme_Proyecto.Controllers
{
    public class PuestosController : Controller
    {
        private PROYECTO_INCLUIREntities db = new PROYECTO_INCLUIREntities();

        [Authorize]
        public ActionResult VerPuestos(int id)
        {
            ViewBag.id = id;

            var list = db.Encuestas.SqlQuery("select * from Encuestas where Encuestas.Empresa_id = {0}", id).ToList();

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
        public ActionResult Crear([Bind(Include = "Empresa_id,nombre,descripcion,fecha")]Encuestas encuestas)
        {
            if (ModelState.IsValid)
            {
                db.Encuestas.Add(encuestas);
                db.SaveChanges();
                return RedirectToAction("Preguntas","Puestos",new { id = encuestas.id,Empresa_id = encuestas.Empresa_id });
            }
            return View();
        }


        [Authorize]
        public ActionResult Preguntas(int id, int Empresa_id)
        {
            ViewBag.id = id;
            ViewBag.empresa_id = Empresa_id;  
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Preguntas(FormCollection collection)
        {
            int id = int.Parse(collection["Empresa_id"]);

            Preguntas preguntas = new Preguntas();
            preguntas.Encuesta_id = int.Parse(collection["Encuesta_id"]);
            preguntas.descripcion = collection["descripcion"];
            preguntas.descripcion_1 = collection["descripcion_1"];
            preguntas.descripcion_2 = collection["descripcion_2"];
            db.Preguntas.Add(preguntas);
            db.SaveChanges();

            return RedirectToAction("VerPuestos", "Puestos", new { id });

        }

        [Authorize]
        public ActionResult Eliminar(int id,int Empresa_id)
        {
            var puesto = db.Encuestas.Find(id);
            db.Encuestas.Remove(puesto);
            db.SaveChanges();
            return RedirectToAction("VerPuestos", "Puestos", new {id = Empresa_id });
        }



        [Authorize]
        public ActionResult VerPreguntas(int id,int Empresa_id)
        {
            ViewBag.Empresa_id = Empresa_id;

            var list = db.Preguntas.SqlQuery("select top 1 * from Preguntas where Encuesta_id = {0}", id).ToList();

            return View(list);
        }

        [Authorize]
        public ActionResult VerPostulantes(int encuestaid,int empresaid)
        {
            ViewBag.empresaid = empresaid;

            var list = db.Preguntas.SqlQuery("select * from Preguntas inner join Usuarios on Preguntas.Usuario_id = Usuarios.id where Encuesta_id = {0}", encuestaid).ToList();
            return View(list);
        }



    }
}