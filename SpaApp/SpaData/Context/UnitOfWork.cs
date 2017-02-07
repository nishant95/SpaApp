using SpaData.Context;
using SpaData.Models;
using SpaData.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaData
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        #region Privates
        private readonly SpaContext _context = new SpaContext();

        private RepositoryBase<SpaContext, Person> _persons;
        #endregion

        #region Constructors
        /// <summary>
        /// Default Constructor for UnitOfWork
        /// </summary>
        public UnitOfWork() { }
        #endregion

        #region Repositories
        public IRepository<Person> Persons
        {
            get
            {
                if (this._persons == null)
                {
                    this._persons = new RepositoryBase<SpaContext, Person>(_context);
                }

                return this._persons;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Saves the changes by calling "SaveChanges()" on DbContext
        /// </summary>
        /// <returns>Status</returns>
        public int Complete()
        {
            //try
            //{
                return _context.SaveChanges();
            //}
            //catch (DbEntityValidationException e)
            //{
            //    foreach (var eve in e.EntityValidationErrors)
            //    {
            //        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
            //            eve.Entry.Entity.GetType().Name, eve.Entry.State);
            //        foreach (var ve in eve.ValidationErrors)
            //        {
            //            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
            //                ve.PropertyName, ve.ErrorMessage);
            //        }
            //    }
            //    throw;
            //}

        }
        #endregion

        #region Disposing Mechanism
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        /// <summary>
        /// Disposes the unitofwork and DbContext
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
