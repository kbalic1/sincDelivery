using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sincDelivery.BDO.Obavjestenje
{
    public class ObavjestenjeBDO
    {

        public int ID { get; set; }
        public string Tekst { get; set; }
        public string Lokacija { get; set; }
        public string Naslov { get; set; }
        public Nullable<bool> Aktivan { get; set; }
        public Nullable<System.DateTime> DatumIVrijemeKreiranja { get; set; }
        public string IzdaoObavjestenjeID { get; set; }
        public Nullable<int> VozacID { get; set; }
        public Nullable<int> NalogID { get; set; }
        public string LokacijaFull { get; set; }
        public string PlaceID { get; set; }

    }
}
