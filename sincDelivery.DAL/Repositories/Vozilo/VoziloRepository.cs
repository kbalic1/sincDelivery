using sincDelivery.BDO;
using sincDelivery.BDO.Vozilo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace sincDelivery.DAL.Repositories.Vozilo
{
    public class VoziloRepository : Repository<DAL.Vozilo>
    {
        public VoziloRepository(sincDeliveryEntities db) : base(db)
        { }


        public List<VoziloBDO> DajSvaVozila ()
        {
            var query = (from k in db.Voziloes.Where(x => x.Aktivan == true)

                             select new VoziloBDO
                         {
                             Aktivan = k.Aktivan,
                             BrojSasije = k.BrojSasije,
                             GarazniBroj = k.GarazniBroj,
                             ID = k.ID,
                             Kilometraza = k.Kilometraza,
                             MarkaVozilaID = k.MarkaVozilaID,
                             Naziv = k.Naziv,
                             ProsjecnaPotrosnja = k.ProsjecnaPotrosnja,
                             RegistarskiBroj = k.RegistarskiBroj,
                             StatusVozilaID = k.StatusVozilaID,
                             TipVozilaID = k.TipVozilaID,
                             TrenutnaLokacijaX = k.TrenutnaLokacijaX,
                             TrenutnaLokacijaY = k.TrenutnaLokacijaY
        });

            return query.OrderByDescending(x => x.ID).ToList();
        }

        public VoziloBDO DodajNoviArtikal(VoziloBDO bdoModel)
        {

            bdoModel.Aktivan = true;
            bdoModel.ID = db.Voziloes.Count() + 1;

            var dbModel = PretvoriBdoUDb(bdoModel);

            using (var t = db.Database.BeginTransaction())
            {
                try
                {
                    dbModel = base.Insert(dbModel);
                    base.Complete();
                    t.Commit();

                }
                catch (Exception e)
                {
                    t.Rollback();
                    throw e;
                }
            }

            return bdoModel;
        }

        public DAL.Vozilo PretvoriBdoUDb(VoziloBDO voziloBDO)
        {
            DAL.Vozilo dbModel = new DAL.Vozilo();

            dbModel.Aktivan = voziloBDO.Aktivan;
            dbModel.BrojSasije = voziloBDO.BrojSasije;
            dbModel.GarazniBroj = voziloBDO.GarazniBroj;
            dbModel.ID = voziloBDO.ID;
            dbModel.Kilometraza = voziloBDO.Kilometraza;
            dbModel.MarkaVozilaID = voziloBDO.MarkaVozilaID;
            dbModel.Naziv = voziloBDO.Naziv;
            dbModel.ProsjecnaPotrosnja = voziloBDO.ProsjecnaPotrosnja;
            dbModel.RegistarskiBroj = voziloBDO.RegistarskiBroj;
            dbModel.StatusVozilaID = voziloBDO.StatusVozilaID;
            dbModel.TipVozilaID = voziloBDO.TipVozilaID;
            dbModel.TrenutnaLokacijaX = voziloBDO.TrenutnaLokacijaX;
            dbModel.TrenutnaLokacijaY = voziloBDO.TrenutnaLokacijaY;

            return dbModel;

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
            return (from g in db.Voziloes.Where(x => x.Aktivan == true )

                    select new SelectListViewModel
                    {
                        Id = g.ID,
                        Opis = g.Naziv + " " + g.GarazniBroj
                    }).ToList();
        }

        public bool VoziloAktivanFalse (int voziloID)
        {
            var vozilo = db.Voziloes.Where(a => a.ID == voziloID).FirstOrDefault();

            vozilo.Aktivan = false;
          

            using (var t = db.Database.BeginTransaction())
            {
                try
                {
                    base.Update(vozilo);
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
