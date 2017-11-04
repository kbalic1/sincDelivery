using sincDelivery.BDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace sincDelivery.DAL.Interfaces
{
    public interface ISifarnikRepository
    {
        IEnumerable<SelectListViewModel> DajSveAktivne();
        SelectList DajSelectListu(int? defaultId = null);
    }
}
