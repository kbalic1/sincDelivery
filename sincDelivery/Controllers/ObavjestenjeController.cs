﻿using Microsoft.AspNet.SignalR;
using PagedList;
using sincDelivery.BDO.Obavjestenje;
using sincDelivery.DAL.UnitsOfWork;
using sincDelivery.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sincDelivery.Controllers
{
    public class ObavjestenjeController : Controller
    {
        CoreUnitOfWork uow = new CoreUnitOfWork();
        // GET: Obavjestenje
        public ActionResult Index(int? page)
        {
            if (page == null)
            {
                page = 1;
            }
            var listaObavjestenja = uow.ObavjestenjeRepository.DajSvaObavjestenja();
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(listaObavjestenja.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult KreirajNovoObavjestenje()
        {
            ViewBag.Vozaci = uow.KorisnikRepository.DajSelectListu();
            ViewBag.Nalozi = uow.PutniNalogRepository.DajSelectListu();
            return View();
        }

        [HttpPost]
        public ActionResult KreirajNovoObavjestenje(ObavjestenjeBDO obavjestenjeBDO)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.Vozaci = uow.KorisnikRepository.DajSelectListu();
                ViewBag.Nalozi = uow.PutniNalogRepository.DajSelectListu();
                return View(obavjestenjeBDO);
            }

            uow.ObavjestenjeRepository.DodajObavjestenje(obavjestenjeBDO);

            var context = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            context.Clients.All.SendNotification("Notification");

            return RedirectToAction("Index");
        }


        public ActionResult ObrisiObavjestenje(int obavjestenjeID)
        {

            uow.ObavjestenjeRepository.ObavjestenjektivanFalse(obavjestenjeID);

            var context = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            context.Clients.All.SendNotification("Notification");

            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}