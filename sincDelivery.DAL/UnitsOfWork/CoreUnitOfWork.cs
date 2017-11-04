using sincDelivery.DAL.Repositories.Korisnik;
using sincDelivery.DAL.Repositories.Obavjestenje;
using sincDelivery.DAL.Repositories.PutniNalog;
using sincDelivery.DAL.Repositories.Vozilo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sincDelivery.DAL.UnitsOfWork
{
    public class CoreUnitOfWork : UnitOfWork, IDisposable
    {
        private KorisnikRepository korisnikReposos;
        public KorisnikRepository KorisnikRepository
        {
            get
            {
                if (this.korisnikReposos == null)
                {
                    this.korisnikReposos = new KorisnikRepository(_context);
                }
                return korisnikReposos;
            }
        }

        private VoziloRepository voziloRepos;
        public VoziloRepository VoziloRepository
        {
            get
            {
                if (this.voziloRepos == null)
                {
                    this.voziloRepos = new VoziloRepository(_context);
                }
                return voziloRepos;
            }
        }

        private PutniNalogRepository putniNalogRepos;
        public PutniNalogRepository PutniNalogRepository
        {
            get
            {
                if (this.putniNalogRepos == null)
                {
                    this.putniNalogRepos = new PutniNalogRepository(_context);
                }
                return putniNalogRepos;
            }
        }

        private ObavjestenjeRepository obavjestenjeRepos;
        public ObavjestenjeRepository ObavjestenjeRepository
        {
            get
            {
                if (this.obavjestenjeRepos == null)
                {
                    this.obavjestenjeRepos = new ObavjestenjeRepository(_context);
                }
                return obavjestenjeRepos;
            }
        }
    }
}
