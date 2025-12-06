using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopMaster.Application.Common
{
    public class CustomerAlreadyExistsException : Exception
    {
        public CustomerAlreadyExistsException(string email)
            : base($"A customer with email '{email}' already exists.")
        {
        }
    }
}
