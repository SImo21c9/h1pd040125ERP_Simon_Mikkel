using Microsoft.Data.SqlClient;

namespace ERP_System;
using TECHCOOL.UI;

// Håndterer lagring og adgang til kundedata
public partial class Database
{
    private List<Customer> customers = new(); // Intern liste over kunder
    private int nextCustomerId = 1;

    // Finder og returnerer en kunde baseret på dens ID, eller null hvis den ikke findes
    public Customer? GetCustomerById(int id)
    {
        foreach (var customer in customers)
        {
            if (customer.CustomerId == id)
            {
                return customer; // Fundet – returnér kunden
            }
        }
        return null; // Ikke fundet – returnér null
    }

    // Returnerer alle kunder i en array
    public Customer[] GetCustomers()
    {
        return customers.ToArray(); // Konverterer listen til et array
    }

    // Tilføjer en kunde hvis den endnu ikke har et ID
    public void AddCustomer(Customer customer)
    {
        SqlConnection connection = GetConnection();
        SqlCommand command = connection.CreateCommand();
        if (customer.CustomerId == 0)
        {
            command.CommandText = "INSERT INTO Customers () VALUES (@GF)";
            command.Parameters.AddWithValue("@CustomerId", nextCustomerId);
            customer.CustomerId = nextCustomerId++;
            customers.Add(customer);
        }

    }

    // Opdaterer en eksisterende kunde, hvis ID findes
    public void UpdateCustomer(Customer customer)
    {
        if (customer.CustomerId == 0)
        {
            AddCustomer(customer);
            return; // ID ikke angivet – kan ikke opdatere
        }

        Customer? oldCustomer = GetCustomerById(customer.CustomerId);
        if (oldCustomer == null)
        {
            return; // Kunden findes ikke
        }

        oldCustomer.CompanyName = customer.CompanyName;
        oldCustomer.Street = customer.Street;
        oldCustomer.StreetNumber = customer.StreetNumber;
        oldCustomer.City = customer.City;
        oldCustomer.Address = customer.Address;
        oldCustomer.Country = customer.Country;
    }

    // Sletter en kunde baseret på ID
    public void DeleteCustomer(int id)
    {
        Customer? found = GetCustomerById(id);
        if (found != null)
        {
            customers.Remove(found);
        }
    }
}
        // else
        // {
        //     command.CommandText = "update Customers set CustomerId = @CustomerId where CustomerId = @CustomerId";
        //     SqlDataReader reader = command.ExecuteNonQuery();
        //     while (reader.Read())
        //     {
        //         Customer customeradd = new();
        //         customeradd.CustomerId = reader.GetInt32(0);
        //         customeradd.FirstName = reader.GetString(40);
        //         customeradd.LastName = reader.GetString(45);
        //         customeradd.Email = reader.GetString(70);
        //         customeradd.PhoneNumber = reader.GetInt32(0);
        //         customeradd.StreetNumber = reader.GetString(40);
        //         customeradd.Street = reader.GetString(10);
        //         customeradd.City = reader.GetString(50);
        //         customeradd.PostCode = reader.GetString(10);
        //         customeradd.Country = (Country) reader.GetInt32(0);
        //         
        //     }
        // }
