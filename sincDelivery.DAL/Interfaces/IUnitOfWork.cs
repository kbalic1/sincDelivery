using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sincDelivery.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        int Complete();
        void DisableChanges();
        void DetectChanges();
        void RefreshContext();

    }
}
