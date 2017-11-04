using sincDelivery.DAL.Repositories.Sifarnici;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sincDelivery.DAL.UnitsOfWork
{
    public class SifarniciUnitOfWork : UnitOfWork
    {
        private VoziloMarkaRepository voziloMarkaRepos;
        public VoziloMarkaRepository VoziloMarkaRepository
        {
            get
            {
                if (this.voziloMarkaRepos == null)
                {
                    this.voziloMarkaRepos = new VoziloMarkaRepository(_context);
                }
                return voziloMarkaRepos;
            }
        }

        private VoziloStatusRepository voziloStatusRepos;
        public VoziloStatusRepository VoziloStatusRepository
        {
            get
            {
                if (this.voziloStatusRepos == null)
                {
                    this.voziloStatusRepos = new VoziloStatusRepository(_context);
                }
                return voziloStatusRepos;
            }
        }

        private VoziloTipRepository voziloTipRepos;
        public VoziloTipRepository VoziloTipRepository
        {
            get
            {
                if (this.voziloTipRepos == null)
                {
                    this.voziloTipRepos = new VoziloTipRepository(_context);
                }
                return voziloTipRepos;
            }
        }

    }
}
