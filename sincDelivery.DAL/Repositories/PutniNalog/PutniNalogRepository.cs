using sincDelivery.BDO;
using sincDelivery.BDO.PutniNalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace sincDelivery.DAL.Repositories.PutniNalog
{
   public class PutniNalogRepository : Repository<DAL.PutniNalog>
    {
        public PutniNalogRepository(sincDeliveryEntities db) : base(db)
        { }


        public void DodajPutniNalog (PutniNalogBDO putniNalogBDO)
        {

            DAL.PutniNalog putniNalogDB = new DAL.PutniNalog();

            putniNalogDB.Aktivan = true;
            putniNalogDB.DatumIVrijemeIzdavanja = DateTime.Now;
            putniNalogDB.DatumIVrijemePolaska = putniNalogBDO.DatumIVrijemePolaska;
            putniNalogDB.Opis = putniNalogBDO.Opis;
            putniNalogDB.PocetnaKilometraza = putniNalogBDO.PocetnaKilometraza;
            putniNalogDB.Sifra = putniNalogBDO.Sifra;
            putniNalogDB.VozacID = putniNalogBDO.VozacID;
            putniNalogDB.StatusID =1;
            putniNalogDB.VoziloID = putniNalogBDO.VoziloID;
            putniNalogDB.LokacijaPolazak = putniNalogBDO.Polazak;
            putniNalogDB.LokacijaOdrediste = putniNalogBDO.Odrediste;
            putniNalogDB.ID = db.PutniNalogs.Count() + 1;
            using (var t = db.Database.BeginTransaction())
            {
                try
                {
                    base.Insert(putniNalogDB);
                    base.Complete();
                    t.Commit();
                }
                catch (Exception e)
                {
                    t.Rollback();
                    throw e;
                }
            }

        }

        public List<PutniNalogBDO> DajSveNaloge()
        {
            var query = (from k in db.PutniNalogs.Where(x => x.Aktivan == true)

                         select new PutniNalogBDO
                         {
                             Aktivan = k.Aktivan,
                             Sifra = k.Sifra,
                             Vozilo = k.Vozilo.Naziv+" "+k.Vozilo.GarazniBroj,
                             ID = k.ID,
                             VozacID = k.VozacID,
                             DatumIVrijemePolaska = k.DatumIVrijemePolaska,
                             DatumIVrijemeIzdavanja = k.DatumIVrijemeIzdavanja,
                             Opis = k.Opis,
                             Polazak=k.LokacijaPolazak.Substring(0,10),
                             Odrediste=k.LokacijaOdrediste.Substring(0,10),
                            
                         });

            return query.OrderByDescending(x => x.ID).ToList();
        }


        public List<PutniNalogBDO> DajSveNalogeVozaca(string username)
        {
            var idKorisnika = db.AspNetUsers.Where(x => x.UserName == username).FirstOrDefault().KorisnikID;
            var query = (from k in db.PutniNalogs.Where(x => x.Aktivan == true && x.VozacID== idKorisnika)
                        
                         select new PutniNalogBDO
                         {
                             Aktivan = k.Aktivan,
                             Sifra = k.Sifra,
                             Vozilo = k.Vozilo.Naziv + " " + k.Vozilo.GarazniBroj,
                             ID = k.ID,
                             VozacID = k.VozacID,
                             DatumIVrijemePolaska = k.DatumIVrijemePolaska,
                             DatumIVrijemeIzdavanja = k.DatumIVrijemeIzdavanja,
                             Opis = k.Opis,
                             Polazak = k.LokacijaPolazak.Substring(0, 10),
                             Odrediste = k.LokacijaOdrediste.Substring(0, 10),

                         });

            return query.OrderByDescending(x => x.ID).ToList();
        }


        public PutniNalogBDO DajNalogPoIdu(int nalogID)
        {
            var query = (from k in db.PutniNalogs.Where(x => x.Aktivan == true && x.ID==nalogID)

                         select new PutniNalogBDO
                         {
                             Aktivan = k.Aktivan,
                             Sifra = k.Sifra,
                             Vozilo = k.Vozilo.Naziv + " " + k.Vozilo.GarazniBroj,
                             ID = k.ID,
                             VozacID = k.VozacID,
                             DatumIVrijemePolaska = k.DatumIVrijemePolaska,
                             DatumIVrijemeIzdavanja = k.DatumIVrijemeIzdavanja,
                             Opis = k.Opis,
                             Polazak = k.LokacijaPolazak.Substring(0, 10),
                             Odrediste = k.LokacijaOdrediste.Substring(0, 10),
                             PolazakFull = k.LokacijaPolazak,
                             OdredisteFull = k.LokacijaOdrediste,

                         });

            return query.OrderByDescending(x => x.ID).FirstOrDefault();
        }


        public SelectList DajSelectListu(int? defaultId = default(int?))
        {
            if (defaultId != null)
            {
                List<SelectListViewModel> list = (List<SelectListViewModel>)DajSveAktivne();
                return new SelectList(list, "Id", "Opis", defaultId);
            }
            return new SelectList(DajSveAktivne(), "Id", "Opis");
        }

        public IEnumerable<SelectListViewModel> DajSveAktivne()
        {
            return (from g in db.PutniNalogs.Where(x => x.Aktivan == true)

                    select new SelectListViewModel
                    {
                        Id = g.ID,
                        Opis = g.Sifra
                    }).ToList();
        }


        public bool ZavrsiNalog(int nalogID)
        {
            var nalog = db.PutniNalogs.Where(a => a.ID == nalogID).FirstOrDefault();

            nalog.Aktivan = false;
            nalog.StatusID = 2;


            using (var t = db.Database.BeginTransaction())
            {
                try
                {
                    base.Update(nalog);
                    base.Complete();
                    t.Commit();

                    return true;
                }
                catch (Exception e)
                {
                    t.Rollback();
                    throw e;
                }
            }
        }

    }
}
