using sincDelivery.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sincDelivery.DAL.UnitsOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        protected sincDeliveryEntities _context;
        protected DbContextTransaction _atomicTran;


        /// <summary>
        /// base constructor
        /// </summary>
        /// <param name="user"></param>
        public UnitOfWork()
        {
            _context = new sincDeliveryEntities();
        }

        /// <summary>
        /// complete to save changes on db context
        /// </summary>
        /// <returns></returns>

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void DisableChanges()
        {
            _context.Configuration.AutoDetectChangesEnabled = false;
        }

        public void DetectChanges()
        {
            _context.ChangeTracker.DetectChanges();
        }

        public void BeginAtomic()
        {
            _atomicTran = _context.Database.BeginTransaction();
        }
        public void CommitAtomic()
        {
            _atomicTran.Commit();
        }
        public void RollbackAtmoic()
        {
            _atomicTran.Rollback();
        }

        public void RefreshContext()
        {
            _context.Dispose();
            _context = new sincDeliveryEntities();
        }

        #region DISPOSE
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                    if (_atomicTran != null)
                    {
                        _atomicTran.Dispose();
                    }
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
