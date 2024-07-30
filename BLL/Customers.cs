using DAL;
using DAL.Models;
using System.Linq.Expressions;
using DAL;

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

        public async Task<List<Customer>> RetrieveByIDAsync(int id) 
        {
            Customer result = null;

            using (var repository = RepositoryFactory.CreateRepository())
            {
                Customer customer = await repository.RetreiveAsync<Customer>(c => c.Id == id);

                //Checkc if cuustomer was found
                if (customer == null) 
                {
                    CustomerExceptions.ThrowInvalidCustomerDataException();
                }
                return customer;
            }
        }

        public async Task<List<Customer>> RetrieveAllAsync() 
        {
            List<Customer> Result = null;

            using (var r = RepositoryFactory.CreateRepository()) 
            {
                Expression<Func<Customer, bool>> allCustomersCriteria = X => true;
                Result = await r.FilterAsync<Customer>(allCustomersCriteria);
            }
            return Result;
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

        public async Task<bool> DeleteAsync(int id) 
        {
            bool Result = false;
            //buscar un cliente para ver si tinene Orders (orden de compra)
            var customer = await RetrieveByIDAsync(id);
            if (customer == null)
            {
                using (var repository = RepositoryFactory.CreateRepository())
                {
                    Result = await repository.DeleteAsync(customer);
                }
            }
            else 
            {
                CustomerExceptions.ThrowInvalidCustomerIdException(customer);
            }
            return Result;
        }
    }
}