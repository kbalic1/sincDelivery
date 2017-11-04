using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sincDelivery.BDO.Korisnik
{
    public class KorisnikBDO
    {
        public string Id { get; set; }
        public int KorisnikID { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }

        [DisplayName("Grad")]
        public Nullable<int> GradID { get; set; }

        [DisplayName("Datum rođenja")]
        public Nullable<System.DateTime> DatumRodjenja { get; set; }
        public string Telefon { get; set; }
        public string ProfilnaSlika { get; set; }
        public Nullable<bool> PremiumAccount { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public Nullable<System.DateTime> LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }

        [DisplayName("Korisničko ime")]
        public string UserName { get; set; }
        public string Opis { get; set; }
        public Nullable<bool> Aktivan { get; set; }
        public string Spol { get; set; }

        public string GradKorisnika { get; set; }
        public bool PostavljenPassword { get; set; }

        public int BrojLajkova { get; set; }
        public int BrojDislajkova { get; set; }
        public bool UserLajkovan { get; set; }
        public bool UserDislajkovan { get; set; }
    }
}
