﻿using Microsoft.Data.SqlClient;

namespace ERP_System;
using TECHCOOL.UI;

// Håndterer lagring og adgang til kundedata
public partial class Database
{
    //indkapsling
    List<Customer> customers = new(); // Intern liste over kunder
    int nextCustomerId = 1;

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
        SqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            Customer customeradd = new();
            customeradd.CustomerId = reader.GetInt32(0);
            customeradd.FirstName = reader.GetString(1);
            customeradd.LastName   = reader.GetString(2);
            customeradd.Email = reader.GetString(3);
            customeradd.PhoneNumber = reader.GetString(4);
            customeradd.StreetNumber = reader.GetString(5);
            customeradd.Street = reader.GetString(6);
            customeradd.City = reader.GetString(7);
            customeradd.PostCode = reader.GetString(8);
            customeradd.Country = (Country)reader.GetByte(9);

            customers.Add(customeradd);
        }
        reader.Close();
        return customers.ToArray();
    }

    // Tilføjer en kunde hvis den endnu ikke har et ID
    public Customer[] AddCustomer(Customer customer)
    {
        SqlConnection connection = GetConnection();
        List<Customer> customers = new();
        SqlCommand command = connection.CreateCommand();
        command.CommandText = "INSERT INTO Customers (FirstName, LastName, Email, PhoneNumber, StreetNumber, Street, City, PostCode, Country ) VALUES (@FirstName, @LastName, @Email, @PhoneNumber, @StreetNumber, @Street, @City, @PostCode, @Country); SELECT SCOPE_IDENTITY();";
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
        command.ExecuteNonQuery();
        customers.Add(customer);
        return customers.ToArray();

    }

    // Opdaterer en eksisterende kunde, hvis ID findes
    public void UpdateCustomer(Customer customer)
    {
        try
        {
            using (SqlConnection connection = GetConnection())
            {
                //connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText =
                        @"UPDATE Customers SET 
                            FirstName = @FirstName, 
                            LastName = @LastName, 
                            Email = @Email, 
                            PhoneNumber = @PhoneNumber, 
                            StreetNumber = @StreetNumber, 
                            Street = @Street, 
                            City = @City, 
                            PostCode = @PostCode, 
                            Country = @Country
                          WHERE CustomerId = @CustomerId";

                    command.Parameters.AddWithValue("@CustomerId", customer.CustomerId);
                    command.Parameters.AddWithValue("@FirstName", customer.FirstName ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@LastName", customer.LastName ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Email", customer.Email ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);
                    command.Parameters.AddWithValue("@StreetNumber", customer.StreetNumber ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Street", customer.Street ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@City", customer.City ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@PostCode", customer.PostCode ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Country", (int)customer.Country);

                    command.ExecuteNonQuery();
                }
            }
        }
        catch (SqlException ex) //Exception
        {
            Console.WriteLine("Fejl ved opdatering af kunde: " + ex.Message);
            Console.ReadLine();
        }
    }

    // Sletter en kunde baseret på ID
    public void DeleteCustomer(int id)
    {
        SqlConnection connection = GetConnection();
        SqlCommand command = connection.CreateCommand();
        command.CommandText = "DELETE FROM Customers WHERE CustomerId = @CustomerId";
        command.Parameters.AddWithValue("@CustomerId", id);
        command.ExecuteNonQuery();
        
        Customer? found = GetCustomerById(id);
        if (found != null)
        {
            customers.Remove(found);
        }
    }
}

