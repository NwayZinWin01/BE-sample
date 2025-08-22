using Data_Access.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDBContext dBContext;

        public UnitOfWork(IDBContext _Context)
        {
            dBContext = _Context;
        }

        public void Commit()
        {
            dBContext.SaveChanges();
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (dBContext != null)
                {
                    dBContext.Dispose();
                    dBContext = null;
                }
            }

        }

    }
}
