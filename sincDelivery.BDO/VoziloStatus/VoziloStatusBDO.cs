using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sincDelivery.BDO.VoziloStatus
{
   public class VoziloStatusBDO
    {

        public int ID { get; set; }
        public string Naziv { get; set; }
        public Nullable<bool> Aktivan { get; set; }
    }
}
