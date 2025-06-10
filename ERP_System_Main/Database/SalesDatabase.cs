using System.Data;
using Microsoft.Data.SqlClient;
using Mysqlx.Crud;

namespace ERP_System;
using Microsoft.Data.SqlClient;
using TECHCOOL.UI;
using System.Collections.Generic;

public partial class Database
{
    private List<SalesOrder> salesOrders = new();
    private int nextSalesOrderId = 1;

    public SalesOrder? GetSalesOrderById(int id)
    {
        foreach (var order in salesOrders)
        {
            if (order.SalesOrderId == id)
            {
                return order;
            }
        }
        return null;
    }

    public SalesOrder[] GetSalesOrders()
    {
        SqlConnection connection = GetConnection();
        connection.Open();
        SqlCommand command = connection.CreateCommand();
        SqlDataReader reader = command.ExecuteReader();
        command.CommandText = "SELECT CustomerId,FirstName, LastName, Email, PhoneNumber, StreetNumber, Street,City, PostCode, Country FROM SalesOrders";
        command.ExecuteReader();
        while (reader.Read())
        {
            Customer salesOrderadd = new();
            salesOrderadd.CustomerId = reader.GetInt32(0);
            salesOrderadd.FirstName = reader.GetString(40);
            salesOrderadd.LastName = reader.GetString(45);
            salesOrderadd.Email = reader.GetString(70);
            salesOrderadd.PhoneNumber = reader.GetInt32(0);
            salesOrderadd.StreetNumber = reader.GetString(40);
            salesOrderadd.Street = reader.GetString(10);
            salesOrderadd.City = reader.GetString(50);
            salesOrderadd.PostCode = reader.GetString(10);
            salesOrderadd.Country = (Country) reader.GetInt32(0);
        }
        return salesOrders.ToArray();
    }

    public void AddSalesOrder(SalesOrder order)
    {
        SqlConnection connection = GetConnection();
        SqlCommand command = connection.CreateCommand();
        command.ExecuteNonQuery();
        if (order.SalesOrderId == 0)
        {
            command.CommandText = "INSERT INTO salesOrders (CustomerId, FirstName, LastName, Email, PhoneNumber, StreetNumber, Street, City, PostCode, Country ) VALUES (@CustomerId, @FirstName, @LastName, @Email, @PhoneNumber, @StreetNumber, @Street, @City, @PostCode, @Country)";
            command.Parameters.AddWithValue("@CustomerId", order.CustomerId); 
            command.Parameters.AddWithValue("@FirstName", order.FirstName);
            command.Parameters.AddWithValue("@LastName", order.LastName);
            command.Parameters.AddWithValue("@Email", order.Email);
            command.Parameters.AddWithValue("@PhoneNumber", order.PhoneNumber);
            command.Parameters.AddWithValue("@StreetNumber", order.StreetNumber);
            command.Parameters.AddWithValue("@Street", order.Street);
            command.Parameters.AddWithValue("@City", order.City);
            command.Parameters.AddWithValue("@PostCode", order.PostCode);
            command.Parameters.AddWithValue("@Country", order.Country); 
            SqlCommand getScopeIdentityCommand = connection.CreateCommand();
            getScopeIdentityCommand.CommandText = "SELECT SCOPE_IDENTITY()";
            order.SalesOrderId = nextSalesOrderId++;
            salesOrders.Add(order);
        }
        
    }
    
 
    public void UpdateSalesOrder(SalesOrder order)
    {
        SqlConnection connection = GetConnection();
        SqlCommand command = connection.CreateCommand();

        command.ExecuteNonQuery();
        command.CommandText = @"UPDATE salesOrders SET CustomerId = @CustomerId, FirstName = @FirstName, LastName = @LastName, Email = @Email, PhoneNumber = @PhoneNumber, StreetNumber = @StreetNumber, Street = @Street, City = @City, PostCode = @PostCode, Country = @Country";
        command.Parameters.AddWithValue("@CustomerId", order.CustomerId); 
        command.Parameters.AddWithValue("@FirstName", order.FirstName);
        command.Parameters.AddWithValue("@LastName", order.LastName);
        command.Parameters.AddWithValue("@Email", order.Email);
        command.Parameters.AddWithValue("@PhoneNumber", order.PhoneNumber);
        command.Parameters.AddWithValue("@StreetNumber", order.StreetNumber);
        command.Parameters.AddWithValue("@Street", order.Street);
        command.Parameters.AddWithValue("@City", order.City);
        command.Parameters.AddWithValue("@PostCode", order.PostCode);
        command.Parameters.AddWithValue("@Country", order.Country); 
    }

    public void DeleteSalesOrder(int id)
    {
        SalesOrder? found = GetSalesOrderById(id);
        if (found != null) 
        {
            salesOrders.Remove(found);
        }
    }
}
