using sincDelivery.BDO.Obavjestenje;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sincDelivery.DAL.Repositories.Obavjestenje
{
    public class ObavjestenjeRepository : Repository<DAL.Obavjestenje>
    {
        public ObavjestenjeRepository(sincDeliveryEntities db) : base(db)
        { }


        public void  DodajObavjestenje(ObavjestenjeBDO obavjestenjeBDO)
        {
            DAL.Obavjestenje obavjestenjeDB = new DAL.Obavjestenje();

            obavjestenjeDB.Aktivan = true;
            obavjestenjeDB.DatumIVrijemeKreiranja = DateTime.Now;
            obavjestenjeDB.Naslov = obavjestenjeBDO.Naslov;
            obavjestenjeDB.Tekst = obavjestenjeBDO.Tekst;
            obavjestenjeDB.Lokacija = obavjestenjeBDO.Lokacija;
            obavjestenjeDB.NalogID = obavjestenjeBDO.NalogID;
            obavjestenjeDB.VozacID = obavjestenjeBDO.VozacID;
            obavjestenjeDB.PlaceID = obavjestenjeBDO.PlaceID;
            

            using (var t = db.Database.BeginTransaction())
            {
                try
                {
                    base.Insert(obavjestenjeDB);
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


        public List<ObavjestenjeBDO> DajSvaObavjestenja()
        {
            var query = (from k in db.Obavjestenjes.Where(x => x.Aktivan == true)

                         select new ObavjestenjeBDO
                         {
                             Aktivan = k.Aktivan,
                             NalogID = k.NalogID,
                             DatumIVrijemeKreiranja =k.DatumIVrijemeKreiranja,
                             ID = k.ID,
                             VozacID = k.VozacID,
                             IzdaoObavjestenjeID = k.IzdaoObavjestenjeID,
                             Lokacija = k.Lokacija,
                             Naslov = k.Naslov,
                             Tekst = k.Tekst,
                             LokacijaFull = k.Lokacija.Substring(0, 10),
                             PlaceID=k.PlaceID

                         });

            return query.OrderByDescending(x => x.ID).ToList();
        }

        public List<ObavjestenjeBDO> DajSvaObavjestenjaZaNalog(int ?NalogID, int? VozacID)
        {
            var query = (from k in db.Obavjestenjes.Where(x => x.Aktivan == true && (x.NalogID==NalogID || x.VozacID==VozacID))

                         select new ObavjestenjeBDO
                         {
                             Aktivan = k.Aktivan,
                             NalogID = k.NalogID,
                             DatumIVrijemeKreiranja = k.DatumIVrijemeKreiranja,
                             ID = k.ID,
                             VozacID = k.VozacID,
                             IzdaoObavjestenjeID = k.IzdaoObavjestenjeID,
                             LokacijaFull = k.Lokacija,
                             Naslov = k.Naslov,
                             Tekst = k.Tekst,
                             Lokacija = k.Lokacija.Substring(0, 10),
                             PlaceID=k.PlaceID

                         });

            return query.OrderByDescending(x => x.ID).ToList();
        }


        public bool ObavjestenjektivanFalse(int obavjestenjeID)
        {
            var obavjestenje = db.Obavjestenjes.Where(a => a.ID == obavjestenjeID).FirstOrDefault();

            obavjestenje.Aktivan = false;


            using (var t = db.Database.BeginTransaction())
            {
                try
                {
                    base.Update(obavjestenje);
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
