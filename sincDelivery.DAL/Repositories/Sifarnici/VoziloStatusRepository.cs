using sincDelivery.BDO;
using sincDelivery.BDO.VoziloStatus;
using sincDelivery.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace sincDelivery.DAL.Repositories.Sifarnici
{
    public class VoziloStatusRepository : Repository<DAL.VoziloStatu>, ISifarnikRepository
    {
        public VoziloStatusRepository(sincDeliveryEntities db) : base(db)
        { }

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
            return (from g in db.VoziloStatus.Where(x => x.Aktivan == true)

                    select new SelectListViewModel
                    {
                        Id = g.ID,
                        Opis = g.Naziv
                    }).ToList();
        }


        public List<VoziloStatusBDO> DajSveVoziloMarke()
        {
            var query = (from k in db.VoziloStatus.Where(x => x.Aktivan == true)

                         select new VoziloStatusBDO
                         {
                             ID = k.ID,
                             Naziv = k.Naziv,
                             Aktivan = k.Aktivan
                         });

            return query.ToList();
        }

        public void VoziloStatusAktivanFalse(int voziloStatusID)
        {
            var voziloStatusDB = db.VoziloStatus.Where(k => k.ID == voziloStatusID).FirstOrDefault();

            voziloStatusDB.Aktivan = false;

            using (var t = db.Database.BeginTransaction())
            {
                try
                {
                    base.Update(voziloStatusDB);
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

        public VoziloStatusBDO DodajGrad(VoziloStatusBDO bdoModel)
        {
            DAL.VoziloStatu dbModel = new DAL.VoziloStatu();

            dbModel.Aktivan = true;
            dbModel.Naziv = bdoModel.Naziv;
            dbModel.ID = db.VoziloStatus.Count() + 1;



            using (var t = db.Database.BeginTransaction())
            {
                try
                {
                    dbModel = base.Insert(dbModel);
                    base.Complete();
                    t.Commit();

                    bdoModel.ID = dbModel.ID;
                }
                catch (Exception e)
                {
                    t.Rollback();
                    throw e;
                }
            }

            return bdoModel;
        }


    }

}
