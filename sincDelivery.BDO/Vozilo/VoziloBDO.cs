using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sincDelivery.BDO.Vozilo
{
   public class VoziloBDO
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Molimo unesite naziv")]
        public string Naziv { get; set; }
        [DisplayName("Garažni broj")]
        public string GarazniBroj { get; set; }
        [DisplayName("Registarski broj")]
        public string RegistarskiBroj { get; set; }
        [DisplayName("Broj šasije")]
        public string BrojSasije { get; set; }
        public Nullable<int> Kilometraza { get; set; }
        [DisplayName("Marka vozila")]
        public Nullable<int> MarkaVozilaID { get; set; }
        [DisplayName("Tip vozila")]
        public Nullable<int> TipVozilaID { get; set; }
        [DisplayName("Status vozila")]
        public Nullable<int> StatusVozilaID { get; set; }
        [DisplayName("Prosječna potrošnja")]
        public Nullable<double> ProsjecnaPotrosnja { get; set; }
        public Nullable<double> TrenutnaLokacijaX { get; set; }
        public Nullable<double> TrenutnaLokacijaY { get; set; }
        public Nullable<bool> Aktivan { get; set; }
    }
}
