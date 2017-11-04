using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PagedList;
using sincDelivery.BDO.Korisnik;
using sincDelivery.DAL;
using sincDelivery.DAL.UnitsOfWork;
using sincDelivery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace sincDelivery.Controllers
{
    public class VozacController : Controller
    {
        CoreUnitOfWork uow = new CoreUnitOfWork();
        SifarniciUnitOfWork sifarniciUow = new SifarniciUnitOfWork();
        sincDeliveryEntities db = new sincDeliveryEntities();
        private ApplicationUserManager _userManager;
        // GET: Vozac
        public ActionResult Index(int? page)
        {

            if (page == null)
            {
                page = 1;
            }
            var listaVozaca = uow.KorisnikRepository.DajSveKorisnikeVozace();
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(listaVozaca.ToPagedList(pageNumber, pageSize));
        }


        public ActionResult DodajVozaca()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DodajVozaca(KorisnikBDO korisnikBDO)
        {

            /* var user = new ApplicationUser {
                 UserName = korisnikBDO.UserName,
                 Email = korisnikBDO.UserName,
                 Ime=korisnikBDO.Ime,
                 Prezime=korisnikBDO.Prezime,
                 Spol=korisnikBDO.Spol,
                 KorisnikTipID=2,
                // Id=korisnikBDO.UserName,
                 KorisnikID=db.AspNetUsers.Count()+1
             };
             var result = await UserManager.CreateAsync(user, korisnikBDO.PasswordHash);*/

           /// AccountController.DodajNovogVozaca(korisnikBDO);

            return RedirectToAction("Index");
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
    }
}