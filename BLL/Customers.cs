using DAL;
using DAL.Models;
using Entities;
using BLL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Customers
    {
        public async Task<Customer> CreateAsync(Customer customer)
        {
            Customer customerResult = null;
            using (var repository = RepositoryFactory.CreateRepository())
            {
                //Buscar si el nombre del cliente existe
                Customers customerSearch = await repository.RetreiveAsync<Customers>(c => c.FirstName == customer.FirstName);
                if (customerSearch != null)
                {
                    //No existre, podemos Crearlo
                    customerResult = await repository.CreateAsync(customer);
                }
                else 
                {
                    CustomerExceptions.ThrowCustomerAlreadyExistsException(customerSearch.FirstName, customerSearch.LastName);
                }
            }
            return customerResult;

        }

        public async Task<List<Customer>> RetreiveByIDAsync(int id) 
        {
            Customer result = null;

            using (var repository = RepositoryFactory.CreateRepository())
            {
                Customer customer = await repository.RetreiveAsync<Customer>(c => c.Id == id);

                //Checkc if cuustomer was found
                if (customer == null) 
                {
                    CustomerExceptions.ThrowInvalidCustomerDataException(id);
                }
                return customer;
            }
        }

        public async Task<bool> UpdateAsync(Customer customer) 
        {

            bool Result = false;
            using (var repository = RepositoryFactory.CreateRepository()) 
            {
                Customer customerSearch = await RepositoryFactory.RetrieveAsync<Customer>(c => c.FirstName == customer.FirstName && c.Id != customer.Id);
                if (customerSearch == null) 
                {
                    //No existe
                    Result = await repository.UpdateAsync(customer);
                }
                else
                {
                    CustomerExceptions.ThrowCustomerAlreadyExistsException(customerSearch.FirstName, customerSearch.LastName);
                }
            }
            return Result;
               
        }
    }
}