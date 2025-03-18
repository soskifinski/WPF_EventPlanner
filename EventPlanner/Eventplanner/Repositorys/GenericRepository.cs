using Eventplanner.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Eventplanner.Repository
{
    public class GenericRepository<T, TContext> : IGenericRepository<T> where T : class where TContext : DbContext
    {
        protected readonly TContext _context;
        private bool disposedValue;

        public GenericRepository(TContext context)
        {
            _context = context;
        }

        public virtual T Find(params object[] keys)
        {
            return _context.Set<T>().Find(keys);
        }

        public virtual async Task<T> FindAsync(params object[] keys)
        {
            return await _context.Set<T>().FindAsync(keys);
        }

        public virtual T FirstOrDefault()
        {
            return _context.Set<T>().FirstOrDefault();
        }

        public virtual T FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().FirstOrDefault(predicate);
        }

        public virtual async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public virtual bool Any(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Any(predicate);
        }

        public virtual async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().AnyAsync(predicate);
        }

        public virtual T SingleOrDefault(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().SingleOrDefault(predicate);
        }

        public virtual async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().SingleOrDefaultAsync(predicate);
        }

        public virtual List<T> ToList()
        {
            return _context.Set<T>().ToList();
        }

        public virtual async Task<List<T>> ToListAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public virtual void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public virtual void AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
        }

        public virtual void AddOrUpdate(T entity)
        {
            _context.Set<T>().AddOrUpdate(entity);
        }

        public virtual void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public virtual void RemoveRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public virtual void SaveChanges()
        {
            _context.SaveChanges();
        }

        public virtual async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public virtual bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                // Nicht verwaltete Ressourcen (nicht verwaltete Objekte) freigeben und Finalizer überschreiben
                // Große Felder auf NULL setzen
                disposedValue = true;
            }
        }

        // // Finalizer nur überschreiben, wenn "Dispose(bool disposing)" Code für die Freigabe nicht verwalteter Ressourcen enthält
        // ~GenericRepository()
        // {
        //     // Ändern Sie diesen Code nicht. Fügen Sie Bereinigungscode in der Methode "Dispose(bool disposing)" ein.
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Ändern Sie diesen Code nicht. Fügen Sie Bereinigungscode in der Methode "Dispose(bool disposing)" ein.
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #region Logging
        private int QueryCount { get; set; }
        private DateTime LoggingStart { get; set; }
        public void EnableLogging(bool queriesOnly = false)
        {
            QueryCount = 0;
            LoggingStart = DateTime.Now;
            _context.Database.Log = (l) =>
            {
                if (l.Contains("SELECT"))
                {
                    QueryCount++;
                    Debug.WriteLine($"[{DateTime.Now.TimeOfDay}] Database Query ({QueryCount}): {l}");
                }
                if (!queriesOnly)
                {
                    Debug.WriteLine($"[{DateTime.Now.TimeOfDay}] Database Log: {l}");
                }
            };
        }
        public void DisableLogging()
        {
            TimeSpan loggingDuration = DateTime.Now.Subtract(LoggingStart);
            Debug.WriteLine($"Stop Database-Logging ({this.GetType().Name}).\nLogging-Duration:\t{loggingDuration}");
            _context.Database.Log = null;
        }
        #endregion
    }

}
