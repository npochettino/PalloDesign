using Servicios;
using Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using UI.Web.ViewModels.Cheques;

namespace UI.Web.Controllers
{
    public class ChequesController : BaseController
    {
        private ChequesServicios _chequesServicios;

        public ChequesController()
        {
            _chequesServicios = new ChequesServicios();
            usr = (Usuario)System.Web.HttpContext.Current.Session["UsuarioActual"];
        }

        // GET: Cheques
        public ActionResult Index(string msj)
        {
            ViewBag.Informacion = msj;

            //Get all cheques
            var Cheques = _chequesServicios.GetAll();
            ChequeIndexViewModel ChequeVM = new ChequeIndexViewModel();
            foreach (var cheque in Cheques)
            {
                ChequeVM.Cheques.Add(cheque);
            }

            return View(ChequeVM);
        }

        public ActionResult Agregar()
        {
            ChequeAgregarViewModel ChequeVM = new ChequeAgregarViewModel();
            ChequeVM.FechaVencimiento = DateTime.Now.Date;
            return View(ChequeVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Agregar(ChequeAgregarViewModel chequeVM)
        {
            if (ModelState.IsValid)
            {
                var cheque = chequeVM.Mapear();

                bool bandera = _chequesServicios.Add(cheque);
                if (bandera)
                {
                    var mensaje = "El Cheque se registro correctamente";
                    return RedirectToAction("Index", new { msj = mensaje });
                }
                else
                {
                    ViewBag.Error = "No se ha podido registrar el cheque, por favor vuelva a intentarlo.";
                    return View(chequeVM);
                }
            }
            else
            {
                ViewBag.Error = "No se ha podido registrar el Cheque, por favor vuelva a intentarlo.";
                return View(chequeVM);
            }
        }

        public ActionResult Eliminar(int id)
        {
            ViewBag.alert = "Se eliminara el cheque";
            ChequeEliminarViewModel cVM = new ChequeEliminarViewModel(_chequesServicios.GetOne(id));
            return View(cVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Eliminar(ChequeEliminarViewModel chequeVM)
        {
            if (chequeVM.Id != 0)
            {
                bool bandera = _chequesServicios.Delete(_chequesServicios.GetOne(chequeVM.Id));
                if (bandera)
                {
                    var mensaje = "El cheque fue eliminado correctamente";
                    return RedirectToAction("Index", new { msj = mensaje });
                }
                else
                {
                    ViewBag.error = "No se ha podido elimianr el cheque, por favor vuelva a intentar.";
                    ChequeEliminarViewModel cVM = new ChequeEliminarViewModel(_chequesServicios.GetOne(chequeVM.Id));
                    return View(cVM);
                }
            }
            else
            {
                ViewBag.error = "No se ha podido elimianr el cheque, por favor vuelva a intentar.";
                ChequeEliminarViewModel cVM = new ChequeEliminarViewModel(_chequesServicios.GetOne(chequeVM.Id));
                return View(cVM);
            }
        }

        public ActionResult Editar(int id)
        {
            var chequeVM = new ChequeEditarViewModel(_chequesServicios.GetOne(id));
            return View(chequeVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(ChequeEditarViewModel ChequeVM)
        {
            if (ChequeVM != null)
            {
                var bandera = _chequesServicios.Update(ChequeVM.Mapear());
                if (bandera)
                {
                    return RedirectToAction("Index", new { msj = "El Cheque se actualizo correctamente." });
                }
                else
                {
                    ViewBag.Error = "No se ha podido actualizar el cheque, por favor vuelva a intentar";
                    return View("Editar", ChequeVM);
                }
            }
            else
            {
                ViewBag.Error = "No se ha podido actualizar el cheque, por favor vuelva a intentar";
                return View("Editar", ChequeVM);
            }
        }

        public ActionResult Detalles(int id)
        {
            var chequeVM = new ChequeDetallesViewModel(_chequesServicios.GetOne(id));
            return View(chequeVM);
        }

        public ActionResult BusquedaCheque(string term)
        {
            var cheques = _chequesServicios.GetByNumberOrBankOrSigner(term);
            var cheque = (from obj in cheques select new { Id = obj.Id, Nombre = obj.Numero + " (" + obj.Banco + ") - ( " + obj.Firmante + " )" });
            return Json(cheque,JsonRequestBehavior.AllowGet);
        }
        
    }
}