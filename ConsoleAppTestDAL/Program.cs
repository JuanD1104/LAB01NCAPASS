﻿// See https://aka.ms/new-console-template for more information


using DAL;
using DAL.Models;
using System.Linq.Expressions;

CreateAsync().GetAwaiter().GetResult();
RetrieveAsync().GetAwaiter().GetResult();
UpdateAsync().GetAwaiter().GetResult();
FilterAsync().GetAwaiter().GetResult();
DeleteAsync().GetAwaiter().GetResult();
Console.ReadKey();

//Method to Create Data Customer
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
//Method to Retreive Data Customer
static async Task RetrieveAsync()
{
    using (var repository = RepositoryFactory.CreateRepository())
    {
        try
        {
            Expression<Func<Customer, bool>> criteria = c => c.FirstName == "Juan" && c.LastName == "Ibañez";
            var customer = await repository.RetrieveAsync(criteria);
            if (customer != null) 
            {
                Console.WriteLine($"Retrieved customer: {customer.FirstName}\t{customer.LastName}\tCity: {customer.City}\tCountry:{customer.Country}");
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

//Method to Update Data Customer
static async Task UpdateAsync()
{
    //Supuesto: Existe el objeto a modificar

    using (var repository = RepositoryFactory.CreateRepository()) 
    {
        var customerToUpdate = await repository.RetrieveAsync<Customer>(c => c.Id == 78);

        if (customerToUpdate != null) 
        {
            customerToUpdate.FirstName = "Liu";
            customerToUpdate.LastName = "Wong";
            customerToUpdate.City = "Toronto";
            customerToUpdate.Country = "Canada";
            customerToUpdate.Phone = "+14337-1233-3435";


        }

        try
        {
            bool update = await repository.UpdateAsync(customerToUpdate);
            if (update) 
            {
                Console.WriteLine("Customer Upddate Successfully");
            }
            else 
            {
                Console.WriteLine("Customer Update Failed");
            }
        }
        catch (Exception ex)
        {

            Console.WriteLine($"Error: {ex.Message}");
        }
    }


}

//Method to Filter Data

static async Task FilterAsync()
{
    using (var repository = RepositoryFactory.CreateRepository()) 
    {
        Expression<Func<Customer, bool>> criteria = c => c.Country == "USA";

        var customers = await repository.FilterAsync(criteria);

        foreach (var customer in customers)
        {
            Console.WriteLine($"(Customer:  { customer.FirstName}\t            {customer.LastName}\t from {customer.City}");
        }
    }
}

//Method to Delete Data

static async Task DeleteAsync() 
{
    using (var repository = RepositoryFactory.CreateRepository()) 
    {
        Expression<Func<Customer, bool>> criteria = customer => customer.Id == 93;
        var customerToDelete = await repository.RetrieveAsync(criteria);

        if (customerToDelete != null)
        {
            bool deleted = await repository.DeleteAsync(customerToDelete);
            Console.WriteLine(deleted ? "Customer Has Deleted" : "Error, Customer Not Deleted");
        }
    }
}
