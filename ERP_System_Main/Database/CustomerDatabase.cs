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
        List<Customer> customers = new();
        SqlConnection connection = GetConnection();
        SqlCommand command = connection.CreateCommand();

        command.CommandText = "SELECT CustomerId, FirstName, LastName, Email, PhoneNumber, StreetNumber, Street, City, PostCode, Country FROM Customers";
        SqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            try
            {
                Customer customeradd = new();
                customeradd.CustomerId = reader.GetInt32(0);
                customeradd.FirstName = reader.GetString(1);
                customeradd.LastName = reader.GetString(2);
                customeradd.Email = reader.GetString(3);
                customeradd.PhoneNumber = reader.GetInt32(4);
                customeradd.StreetNumber = reader.GetString(5);
                customeradd.Street = reader.GetString(6);
                customeradd.City = reader.GetString(7);
                customeradd.PostCode = reader.GetString(8);
                customeradd.Country = (Country)reader.GetInt32(9);

                customers.Add(customeradd);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fejl ved indlæsning af kunde: " + ex.Message);
            }
        }
        reader.Close();
        return customers.ToArray();
    }

    // Tilføjer en kunde hvis den endnu ikke har et ID
    public void AddCustomer(Customer customer)
    {
        SqlConnection connection = GetConnection();
        SqlCommand command = connection.CreateCommand();
        command.CommandText = "INSERT INTO Customers (CustomerId, FirstName, LastName, Email, PhoneNumber, StreetNumber, Street, City, PostCode, Country ) VALUES (@CustomerId, @FirstName, @LastName, @Email, @PhoneNumber, @StreetNumber, @Street, @City, @PostCode, @Country); SELECT SCOPE_IDENTITY();";
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
            
        customers.Add(customer);

    }

    // Opdaterer en eksisterende kunde, hvis ID findes
    public void UpdateCustomer(Customer customer)
    {
        SqlConnection connection = GetConnection();
        SqlCommand command = connection.CreateCommand();
        command.CommandText =
            @"UPDATE Customer SET CustomerId = @CustomerId, FirstName = @FirstName, LastName = @LastName, Email = @Email, PhoneNumber = @PhoneNumber, StreetNumber = @StreetNumber, Street = @Street, City = @City, PostCode = @PostCode, Country = @Country";
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
