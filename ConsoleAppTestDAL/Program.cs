// See https://aka.ms/new-console-template for more information


using DAL;
using DAL.Models;
using System.Linq.Expressions;

CreateAsync().GetAwaiter().GetResult();
RetreiveAsync().GetAwaiter().GetResult();
static async Task CreateAsync()
{ 
    //Add Customer
    Customer customer = new Customer()
    {
        FirstName = "Juan",
        LastName = "Ibañez",
        City = "Bogotá",
        Country = "Colombia",
        Phone = "3042038840"
    };

    using (var repository = RepositoryFactory.CreateRepository()) 
    {
        try
        {
            var createdCustomer = await repository.CreateAsync(customer);
            Console.WriteLine($"Added Customer: {createdCustomer.LastName} {createdCustomer.FirstName}");
        }
        catch (Exception ex)
        {

            Console.WriteLine($"Error: {ex.Message} ");
        }
    
    }
}

static async Task RetreiveAsync()
{
    using (var repository = RepositoryFactory.CreateRepository())
    {
        try
        {
            Expression<Func<Customer, bool>> criteria = c => c.FirstName == "Juan" && c.LastName == "Ibañez";
            var customer = await repository.RetreiveAsync(criteria);
            if (customer != null) 
            {
                Console.WriteLine($"Retrived customer: {customer.FirstName}\t{customer.LastName}\tCity: {customer.City}\tCountry:{customer.Country}");
            }
            else 
            {
                Console.WriteLine("Customer doesn't exist");
            }
            
        }
        catch (Exception ex)
        {

            Console.WriteLine($"Error: {ex.Message} ");
        }

    }
}