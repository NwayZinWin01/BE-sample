﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access
{
    public static class InterfaceModule
    {
        public static IEnumerable<Type> GetInterfaces()
        {
            Assembly ass = Assembly.Load("Data Access");
            // Get all types from the assembly
            Type[] types = ass.GetTypes();

            // Filter types to get only interfaces
            var interfaces = types.Where(t => t.IsInterface && !t.Name.Equals("IReadWriteRepositoryBase`1"));
            //Repository Configuration
            return interfaces;
        }
    }
}
