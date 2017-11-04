using sincDelivery.BDO;
using sincDelivery.BDO.VoziloTip;
using sincDelivery.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace sincDelivery.DAL.Repositories.Sifarnici
{
    public class VoziloTipRepository : Repository<DAL.VoziloTip>, ISifarnikRepository
    {
        public VoziloTipRepository(sincDeliveryEntities db) : base(db)
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
            return (from g in db.VoziloTips.Where(x => x.Aktivan == true)

                    select new SelectListViewModel
                    {
                        Id = g.ID,
                        Opis = g.Naziv
                    }).ToList();
        }


        public List<VoziloTipBDO> DajSveVoziloTip()
        {
            var query = (from k in db.VoziloTips.Where(x => x.Aktivan == true)

                         select new VoziloTipBDO
                         {
                             ID = k.ID,
                             Naziv = k.Naziv,
                             Aktivan = k.Aktivan
                         });

            return query.ToList();
        }

        public void VoziloTipAktivanFalse(int voziloTipID)
        {
            var voziloTipDB = db.VoziloTips.Where(k => k.ID == voziloTipID).FirstOrDefault();

            voziloTipDB.Aktivan = false;

            using (var t = db.Database.BeginTransaction())
            {
                try
                {
                    base.Update(voziloTipDB);
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

        public VoziloTipBDO DodajGrad(VoziloTipBDO bdoModel)
        {
            DAL.VoziloTip dbModel = new DAL.VoziloTip();

            dbModel.Aktivan = true;
            dbModel.Naziv = bdoModel.Naziv;
            dbModel.ID = db.VoziloTips.Count() + 1;



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
