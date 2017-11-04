using sincDelivery.BDO;
using sincDelivery.BDO.VoziloMarka;
using sincDelivery.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace sincDelivery.DAL.Repositories.Sifarnici
{
        public class VoziloMarkaRepository : Repository<DAL.VoziloMarka>, ISifarnikRepository
        {
            public VoziloMarkaRepository(sincDeliveryEntities db) : base(db)
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
                return (from g in db.VoziloMarkas.Where(x => x.Aktivan == true)

                        select new SelectListViewModel
                        {
                            Id = g.ID,
                            Opis = g.Naziv
                        }).ToList();
            }


            public List<VoziloMarkaBDO> DajSveVoziloMarke()
            {
                var query = (from k in db.VoziloMarkas.Where(x => x.Aktivan == true)

                             select new VoziloMarkaBDO
                             {
                                 ID = k.ID,
                                 Naziv = k.Naziv,
                                 Aktivan = k.Aktivan
                             });

                return query.ToList();
            }

            public void VoziloMArkaAktivanFalse(int voziloMarkaID)
            {
                var voziloMarkaDB = db.VoziloMarkas.Where(k => k.ID == voziloMarkaID).FirstOrDefault();

            voziloMarkaDB.Aktivan = false;

                using (var t = db.Database.BeginTransaction())
                {
                    try
                    {
                        base.Update(voziloMarkaDB);
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

            public VoziloMarkaBDO DodajGrad(VoziloMarkaBDO bdoModel)
            {
                DAL.VoziloMarka dbModel = new DAL.VoziloMarka();

                dbModel.Aktivan = true;
                dbModel.Naziv = bdoModel.Naziv;
                dbModel.ID = db.VoziloMarkas.Count() + 1;



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
