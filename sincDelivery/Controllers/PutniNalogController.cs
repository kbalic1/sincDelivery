using PagedList;
using sincDelivery.BDO.PutniNalog;
using sincDelivery.DAL.UnitsOfWork;
using sincDelivery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sincDelivery.Controllers
{
    public class PutniNalogController : Controller
    {
        CoreUnitOfWork uow = new CoreUnitOfWork();
        // GET: PutniNalog
        public ActionResult Index(int? page)
        {

            if(User.Identity.Name!="admin@sync.com")
            {
                if (page == null)
                {
                    page = 1;
                }
                var listaNalogaVozaca = uow.PutniNalogRepository.DajSveNalogeVozaca(User.Identity.Name);
                int pageSizeV = 5;
                int pageNumberV = (page ?? 1);
                return View(listaNalogaVozaca.ToPagedList(pageNumberV, pageSizeV));
            }
            else
            {
                if (page == null)
                {
                    page = 1;
                }
                var listaNaloga = uow.PutniNalogRepository.DajSveNaloge();
                int pageSize = 5;
                int pageNumber = (page ?? 1);
                return View(listaNaloga.ToPagedList(pageNumber, pageSize));
            }

           
        }

        public ActionResult KreirajPutniNalog ()
        {

            ViewBag.Vozaci = uow.KorisnikRepository.DajSelectListu();
            ViewBag.Vozila= uow.VoziloRepository.DajSelectListu();

            return View();
        }

        [HttpPost]
        public ActionResult KreirajPutniNalog(PutniNalogBDO putniNalogBDO)
        {

            if (!ModelState.IsValid)
            {

                ViewBag.Vozaci = uow.KorisnikRepository.DajSelectListu();
                ViewBag.Vozila = uow.VoziloRepository.DajSelectListu();

                return View(putniNalogBDO);
            }

            uow.PutniNalogRepository.DodajPutniNalog(putniNalogBDO);

            

            return RedirectToAction("Index");
        }

        public ActionResult Nalog (int? NalogID)
        {

            if (NalogID == null)
                return RedirectToAction("Index");

            var nalog = uow.PutniNalogRepository.DajNalogPoIdu((int)NalogID);

            var listaObavjestenja = uow.ObavjestenjeRepository.DajSvaObavjestenjaZaNalog(nalog.ID, nalog.VozacID);

            NalogWrapper nalogWrapper = new NalogWrapper();
            nalogWrapper.putniNalog = nalog;
            nalogWrapper.listaObavjestenja = listaObavjestenja;

            return View(nalogWrapper);
        }


        public ActionResult ZavrsiNalog(int nalogID)
        {

            uow.PutniNalogRepository.ZavrsiNalog(nalogID);

            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}