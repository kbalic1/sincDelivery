using sincDelivery.BDO.Vozilo;
using sincDelivery.DAL.UnitsOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace sincDelivery.Controllers
{
    public class VoziloController : Controller
    {
        CoreUnitOfWork uow = new CoreUnitOfWork();
        SifarniciUnitOfWork sifarniciUow = new SifarniciUnitOfWork();

        // GET: Vozilo
        public ActionResult Index(int? page)
        {
           /* if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                if (Helperi.KorisnikHelper.DajPodatkeUsera().Username != "Lena")
                    return RedirectToAction("Index", "Home");
            }*/

            if (page == null)
            {
                page = 1;
            }
            var listaVozila = uow.VoziloRepository.DajSvaVozila();
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(listaVozila.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult DodajVozilo ()
        {

            ViewBag.MarkeVozila = sifarniciUow.VoziloMarkaRepository.DajSelectListu();
            ViewBag.StatusVozila = sifarniciUow.VoziloStatusRepository.DajSelectListu();
            ViewBag.TipVozila = sifarniciUow.VoziloTipRepository.DajSelectListu();

            return View();
        }

        [HttpPost]
        public ActionResult DodajVozilo(VoziloBDO voziloBDO)
        {

            if (!ModelState.IsValid)
            {
                ViewBag.MarkeVozila = sifarniciUow.VoziloMarkaRepository.DajSelectListu();
                ViewBag.StatusVozila = sifarniciUow.VoziloStatusRepository.DajSelectListu();
                ViewBag.TipVozila = sifarniciUow.VoziloTipRepository.DajSelectListu();

                return View(voziloBDO);
            }

            if (ModelState.IsValid)
            {
                var novoVozilo = uow.VoziloRepository.DodajNoviArtikal(voziloBDO);

                if (novoVozilo.ID == 0)
                    throw new Exception("Došlo je do greške, vozilo nije uspješno spašeno. Molimo pokušajte kasnije.");
            }

               

            return RedirectToAction("Index","Home");
        }


        public ActionResult ObrisiVozilo(int voziloID)
        {

            uow.VoziloRepository.VoziloAktivanFalse(voziloID);

            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}