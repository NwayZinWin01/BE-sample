using Data_Access.Entity;
using Data_Access.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Repository
{
    public class EmployeeRepository : ReadWriteRepositoryBase<Employee>, IEmployeeRepository
    {
        private readonly IDBContext _context;
        public EmployeeRepository(IDBContext context) : base(context)
        {
            _context = context;
        }
    }
}
