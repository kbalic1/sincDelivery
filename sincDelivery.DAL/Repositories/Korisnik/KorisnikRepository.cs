using sincDelivery.BDO;
using sincDelivery.BDO.Korisnik;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace sincDelivery.DAL.Repositories.Korisnik
{

    public class KorisnikRepository : Repository<AspNetUser>
    {
        public KorisnikRepository(sincDeliveryEntities db) : base(db)
        { }

        public List<KorisnikBDO> DajSveKorisnike()
        {
            var query = (from k in db.AspNetUsers.Where(x => x.Aktivan == true)
                             // join g in db.Grads on k.GradID equals g.GradID into gd
                             // from g in gd.DefaultIfEmpty()
                         select new KorisnikBDO
                         {
                             UserName = k.UserName,
                             Ime = k.Ime,
                             Id = k.Id,
                             GradID = k.GradID,
                             // GradKorisnika = g.Naziv,
                             Email = k.Email,
                             DatumRodjenja = k.DatumRodjenja,
                             Telefon = k.Telefon,
                             ProfilnaSlika = k.ProfilnaSlika,
                             KorisnikID = k.KorisnikID,
                             Prezime = k.Prezime,
                             //  PremiumAccount = k.PremiumAccount,
                             EmailConfirmed = k.EmailConfirmed,
                             Opis = k.Opis
                         });

            return query.OrderByDescending(x => x.DatumRodjenja).ToList();
        }

        public List<KorisnikBDO> DajSveKorisnikeVozace()
        {
            var query = (from k in db.AspNetUsers.Where(x => x.Aktivan == true && x.KorisnikTipID == 2)
                             // join g in db.Grads on k.GradID equals g.GradID into gd
                             // from g in gd.DefaultIfEmpty()
                         select new KorisnikBDO
                         {
                             UserName = k.UserName,
                             Ime = k.Ime,
                             Id = k.Id,
                             GradID = k.GradID,
                             // GradKorisnika = g.Naziv,
                             Spol=k.Spol,
                             
                             Email = k.Email,
                             DatumRodjenja = k.DatumRodjenja,
                             Telefon = k.Telefon,
                             ProfilnaSlika = k.ProfilnaSlika,
                             KorisnikID = k.KorisnikID,
                             Prezime = k.Prezime,
                             //  PremiumAccount = k.PremiumAccount,
                             EmailConfirmed = k.EmailConfirmed,
                             Opis = k.Opis
                         });

            return query.OrderByDescending(x => x.DatumRodjenja).ToList();
        }

        public KorisnikBDO DajKorisnikBDOPoGuidu(string guidUsera)
        {
            var query = (from k in db.AspNetUsers.Where(kor => kor.Id == guidUsera && kor.Aktivan == true)
                             // join g in db.Grads on k.GradID equals g.GradID into gd
                             // from g in gd.DefaultIfEmpty()
                         select new KorisnikBDO
                         {
                             UserName = k.UserName,
                             Ime = k.Ime,
                             Id = k.Id,
                             //   GradID = k.GradID,
                             //     GradKorisnika = g.Naziv,
                             Email = k.Email,
                             DatumRodjenja = k.DatumRodjenja,
                             Telefon = k.Telefon,
                             ProfilnaSlika = k.ProfilnaSlika,
                             KorisnikID = k.KorisnikID,
                             Prezime = k.Prezime,
                             //   PremiumAccount = k.PremiumAccount,
                             EmailConfirmed = k.EmailConfirmed,
                             Opis = k.Opis,
                             Spol = k.Spol
                         });

            return query.FirstOrDefault();
        }

        public KorisnikBDO DajKorisnikBDOPoUsernameu(string username)
        {
            var query = (from k in db.AspNetUsers.Where(kor => kor.UserName == username && kor.Aktivan == true)
                             //  join g in db.Grads on k.GradID equals g.GradID into gd
                             //from g in gd.DefaultIfEmpty()
                         select new KorisnikBDO
                         {
                             UserName = k.UserName,
                             Ime = k.Ime,
                             Id = k.Id,
                             GradID = k.GradID,
                             //    GradKorisnika = g.Naziv,
                             Email = k.Email,
                             DatumRodjenja = k.DatumRodjenja,
                             Telefon = k.Telefon,
                             ProfilnaSlika = k.ProfilnaSlika,
                             KorisnikID = k.KorisnikID,
                             Prezime = k.Prezime,
                             //      PremiumAccount = k.PremiumAccount,
                             EmailConfirmed = k.EmailConfirmed,
                             Opis = k.Opis,
                             Spol = k.Spol,
                             Aktivan = k.Aktivan
                         });

            return query.FirstOrDefault();
        }

        public void UpdateKorisnika(KorisnikBDO korisnikBDO)
        {
            var korisnikDB = db.AspNetUsers.Where(k => k.Id == korisnikBDO.Id).FirstOrDefault();

            korisnikDB.Ime = korisnikBDO.Ime;
            korisnikDB.Prezime = korisnikBDO.Prezime;
            korisnikDB.Telefon = korisnikBDO.Telefon;
            korisnikDB.GradID = korisnikBDO.GradID;
            korisnikDB.DatumRodjenja = korisnikBDO.DatumRodjenja;
            korisnikDB.ProfilnaSlika = korisnikBDO.ProfilnaSlika;
            korisnikDB.Opis = korisnikBDO.Opis;
            // JOS OPIS FALI U BAZI 

            using (var t = db.Database.BeginTransaction())
            {
                try
                {
                    base.Update(korisnikDB);
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

        public void DodajNovogVozaca(KorisnikBDO korisnikBDO)
        {
            var korisnikDB = new DAL.AspNetUser();

            korisnikDB.Ime = korisnikBDO.Ime;
            korisnikDB.Prezime = korisnikBDO.Prezime;
            korisnikDB.Telefon = korisnikBDO.Telefon;
            korisnikDB.Spol = korisnikBDO.Spol;
            korisnikDB.KorisnikID = db.AspNetUsers.Count() + 1;
            korisnikDB.UserName = korisnikBDO.UserName;
            korisnikDB.Id = korisnikBDO.UserName;
           // korisnikDB.PasswordHash=Has
         //   korisnikDB.GradID = korisnikBDO.GradID;
         //   korisnikDB.DatumRodjenja = korisnikBDO.DatumRodjenja;
         //   korisnikDB.ProfilnaSlika = korisnikBDO.ProfilnaSlika;
           // korisnikDB.Opis = korisnikBDO.Opis;
            // JOS OPIS FALI U BAZI 

            using (var t = db.Database.BeginTransaction())
            {
                try
                {
                    base.Insert(korisnikDB);
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


        public void KorisnikAktivanFalse(int korisnikID)
        {
            var korisnikDB = db.AspNetUsers.Where(k => k.KorisnikID == korisnikID).FirstOrDefault();

            korisnikDB.Aktivan = false;

            using (var t = db.Database.BeginTransaction())
            {
                try
                {
                    base.Update(korisnikDB);
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

        public void KorisnikZakljucaj(int korisnikID)
        {
            var korisnikDB = db.AspNetUsers.Where(k => k.KorisnikID == korisnikID).FirstOrDefault();

            korisnikDB.LockoutEnabled = false;

            using (var t = db.Database.BeginTransaction())
            {
                try
                {
                    base.Update(korisnikDB);
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

        public string DajUsernameKorisnikaPoId(int id)
        {
            return db.AspNetUsers.Where(x => x.KorisnikID == id).FirstOrDefault().UserName;
        }

        public string DajJedinstvenNoviUsername(string predlozeniUsername)
        {
            if (String.IsNullOrEmpty(predlozeniUsername)) return "";

            var listaKorisnika = db.AspNetUsers.ToList();

            int i = 1;

            while (listaKorisnika.Where(k => k.UserName == predlozeniUsername).FirstOrDefault() != null)
            {
                predlozeniUsername = predlozeniUsername + i.ToString();
                i++;
            }

            return predlozeniUsername;
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
            return (from g in db.AspNetUsers.Where(x => x.Aktivan == true && x.KorisnikTipID==2)

                    select new SelectListViewModel
                    {
                        Id = g.KorisnikID,
                        Opis = g.Ime+" "+g.Prezime
                    }).ToList();
        }
    }

}
