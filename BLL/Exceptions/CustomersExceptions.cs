using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BLL
{
    public class CustomerExceptions : Exception
    {


        private CustomerExceptions(string message) : base(message)
        {
        }
        public static void ThrowCustomerAlreadyException(string firstName, string lastName)
        {
            throw new CustomerExceptions($"A client with the name already exist{firstName}{lastName}.");
        }

        public static void ThrowInvalidCustomerDataException(string message)
        {
            throw new CustomerExceptions(message);
        }

        internal static void ThrowCustomerAlreadyExistsException(object firstName, object lastName)
        {
            throw new NotImplementedException();
        }

        internal static void ThrowCustomerAlreadyExitsException(object firstName, object lastName)
        {
            throw new NotImplementedException();
        }

        internal static void ThrowInvalidCustomerDataException()
        {
            throw new NotImplementedException();
        }

        internal static void ThrowInvalidCustomerIdException(List<Customer> customer)
        {
            throw new NotImplementedException();
        }

        internal static void ThrowInvalidCustomerIdException(int id)
        {
            throw new NotImplementedException();
        }
    }
}

