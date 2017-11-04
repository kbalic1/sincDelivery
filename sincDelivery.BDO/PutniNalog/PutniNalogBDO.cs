using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sincDelivery.BDO.PutniNalog
{
    public class PutniNalogBDO
    {
        public int ID { get; set; }
        public string Sifra { get; set; }
        [DisplayName ("Vozilo")]
        public Nullable<int> VoziloID { get; set; }
        [DisplayName("Vozac")]
        public Nullable<int> VozacID { get; set; }
        [DisplayName("Početna kilometraža")]
        public Nullable<int> PocetnaKilometraza { get; set; }
        public Nullable<int> KrajnjaKilometraza { get; set; }
        public Nullable<double> PotrosenoGoriva { get; set; }
        public Nullable<int> ZaposlenikIzdaoID { get; set; }
        public Nullable<int> ZaposlenikZakljucioID { get; set; }
        public Nullable<int> StatusID { get; set; }
        public string Opis { get; set; }
        public Nullable<System.DateTime> DatumIVrijemeIzdavanja { get; set; }
        public Nullable<System.DateTime> DatumIVrijemeZakljucivanja { get; set; }
        [DisplayName("Vrijeme polaska")]
        public Nullable<System.DateTime> DatumIVrijemePolaska { get; set; }
        public Nullable<System.DateTime> DatumIVrijemeDolaska { get; set; }
        public Nullable<bool> Zavrseno { get; set; }
        public Nullable<double> Norma { get; set; }
        public Nullable<bool> Aktivan { get; set; }
        public string Vozac { get; set; }
        public string Vozilo { get; set; }
        public string Polazak { get; set; }
        public string Odrediste { get; set; }
        public string PolazakFull { get; set; }
        public string OdredisteFull { get; set; }
    }
}
