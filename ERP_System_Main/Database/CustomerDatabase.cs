﻿using Microsoft.Data.SqlClient;

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
        
        List<Customer> customers = new();
        SqlConnection connection = GetConnection();
        SqlCommand command = connection.CreateCommand();
        command.CommandText = "SELECT CustomerId, FirstName, LastName, Email, PhoneNumber, StreetNumber, Street, City, PostCode, Country FROM Customers";
        command.ExecuteNonQuery();
        SqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            Customer customeradd = new();
            customeradd.CustomerId = reader.GetInt32(0);
            customeradd.FirstName = reader.GetString(40);
            customeradd.LastName = reader.GetString(45);
            customeradd.Email = reader.GetString(70);
            customeradd.PhoneNumber = reader.GetInt32(0);
            customeradd.StreetNumber = reader.GetString(40);
            customeradd.Street = reader.GetString(10);
            customeradd.City = reader.GetString(50);
            customeradd.PostCode = reader.GetString(10);
            customeradd.Country = (Country)reader.GetInt32(0);

        }
        return customers.ToArray(); // Konverterer listen til et array
    }

    // Tilføjer en kunde hvis den endnu ikke har et ID
    public void AddCustomer(Customer customer)
    {
        SqlConnection connection = GetConnection();
        SqlCommand command = connection.CreateCommand();
        command.ExecuteReader();
        if (customer.CustomerId == 0)
        {
            command.CommandText = "INSERT INTO Customers (CustomerId, FirstName, LastName, Email, PhoneNumber, StreetNumber, Street, City, PostCode, Country ) VALUES (@CustomerId, @FirstName, @LastName, @Email, @PhoneNumber, @StreetNumber, @Street, @City, @PostCode, @Country)";
            command.Parameters.AddWithValue("@CustomerId", customer.CustomerId); 
            command.Parameters.AddWithValue("@FirstName", customer.FirstName);
            command.Parameters.AddWithValue("@LastName", customer.LastName);
            command.Parameters.AddWithValue("@Email", customer.Email);
            command.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);
            command.Parameters.AddWithValue("@StreetNumber", customer.StreetNumber);
            command.Parameters.AddWithValue("@Street", customer.Street);
            command.Parameters.AddWithValue("@City", customer.City);
            command.Parameters.AddWithValue("@PostCode", customer.PostCode);
            command.Parameters.AddWithValue("@Country", customer.Country); 
            SqlCommand getScopeIdentityCommand = connection.CreateCommand();
            getScopeIdentityCommand.CommandText = "SELECT SCOPE_IDENTITY()";
            customer.CustomerId = nextCustomerId++;
            customers.Add(customer);
        }

    }

    // Opdaterer en eksisterende kunde, hvis ID findes
    public void UpdateCustomer(Customer customer)
    {
        SqlConnection connection = GetConnection();
        SqlCommand command = connection.CreateCommand();

        command.ExecuteNonQuery();
        command.CommandText = @"UPDATE Customer SET CustomerId = @CustomerId, FirstName = @FirstName, LastName = @LastName, Email = @Email, PhoneNumber = @PhoneNumber, StreetNumber = @StreetNumber, Street = @Street, City = @City, PostCode = @PostCode, Country = @Country";
        command.Parameters.AddWithValue("@CustomerId", customer.CustomerId); 
        command.Parameters.AddWithValue("@FirstName", customer.FirstName);
        command.Parameters.AddWithValue("@LastName", customer.LastName);
        command.Parameters.AddWithValue("@Email", customer.Email);
        command.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);
        command.Parameters.AddWithValue("@StreetNumber", customer.StreetNumber);
        command.Parameters.AddWithValue("@Street", customer.Street);
        command.Parameters.AddWithValue("@City", customer.City);
        command.Parameters.AddWithValue("@PostCode", customer.PostCode);
        command.Parameters.AddWithValue("@Country", customer.Country); 
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
        //    /* command.CommandText = "update Customers set CustomerId = @CustomerId where CustomerId = @CustomerId"; */
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
// how about you add a delete and update the database classes (i mean for the sql, just i like i did with the add using c# and sql combo)?