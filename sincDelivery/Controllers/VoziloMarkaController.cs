using sincDelivery.BDO.VoziloMarka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sincDelivery.Controllers
{
    public class VoziloMarkaController : Controller
    {
        // GET: VoziloMarka
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DodajVoziloMarka ()
        {

            return View();
        }

        [HttpPost]
        public ActionResult DodajVoziloMarka(VoziloMarkaBDO voziloMarka)
        {

            return View();
        }
    }
}