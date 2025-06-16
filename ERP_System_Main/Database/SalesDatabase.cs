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
        List<SalesOrder> orderslist = new List<SalesOrder>();
        SqlConnection connection = GetConnection();
        SqlCommand command = connection.CreateCommand();
        
        command.CommandText = "SELECT FirstName, LastName, Email, PhoneNumber, StreetNumber, Street,City, PostCode, Country FROM SalesOrders";
        SqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            Customer salesOrderadd = new();
            salesOrderadd.FirstName = reader.GetString(0);
            salesOrderadd.LastName = reader.GetString(1);
            salesOrderadd.Email = reader.GetString(2);
            salesOrderadd.PhoneNumber = reader.GetString(3);
            salesOrderadd.StreetNumber = reader.GetString(4);
            salesOrderadd.Street = reader.GetString(5);
            salesOrderadd.City = reader.GetString(6);
            salesOrderadd.PostCode = reader.GetString(7);
            salesOrderadd.Country = (Country) reader.GetInt32(8);
        }

        reader.Close();
        return orderslist.ToArray();
    }

    public void AddSalesOrder(SalesOrder order)
    {
        SqlConnection connection = GetConnection();
        SqlCommand command = connection.CreateCommand();
        command.CommandText = "INSERT INTO SalesOrders (CustomerId, FirstName, LastName, Email, PhoneNumber, StreetNumber, Street, City, PostCode, Country ) VALUES (@CustomerId, @FirstName, @LastName, @Email, @PhoneNumber, @StreetNumber, @Street, @City, @PostCode, @Country); SELECT SCOPE_IDENTITY(); ";
        command.Parameters.AddWithValue("@CustomerId", order.CustomerId);
        /*
        command.Parameters.AddWithValue("@FirstName", order.FirstName);
        command.Parameters.AddWithValue("@LastName", order.LastName);
        command.Parameters.AddWithValue("@Email", order.Email);
        command.Parameters.AddWithValue("@PhoneNumber", order.PhoneNumber);
        command.Parameters.AddWithValue("@StreetNumber", order.StreetNumber);
        command.Parameters.AddWithValue("@Street", order.Street);
        command.Parameters.AddWithValue("@City", order.City);
        command.Parameters.AddWithValue("@PostCode", order.PostCode);
        command.Parameters.AddWithValue("@Country", order.Country);
        */
        salesOrders.Add(order);
    }
    
 
    public void UpdateSalesOrder(SalesOrder order)
    {
        SqlConnection connection = GetConnection();
        SqlCommand command = connection.CreateCommand();

        command.ExecuteNonQuery();
        command.CommandText = @"UPDATE SalesOrders SET CustomerId = @CustomerId, FirstName = @FirstName, LastName = @LastName, Email = @Email, PhoneNumber = @PhoneNumber, StreetNumber = @StreetNumber, Street = @Street, City = @City, PostCode = @PostCode, Country = @Country";
        command.Parameters.AddWithValue("@CustomerId", order.CustomerId); 
        /*
        command.Parameters.AddWithValue("@FirstName", order.FirstName);
        command.Parameters.AddWithValue("@LastName", order.LastName);
        command.Parameters.AddWithValue("@Email", order.Email);
        command.Parameters.AddWithValue("@PhoneNumber", order.PhoneNumber);
        command.Parameters.AddWithValue("@StreetNumber", order.StreetNumber);
        command.Parameters.AddWithValue("@Street", order.Street);
        command.Parameters.AddWithValue("@City", order.City);
        command.Parameters.AddWithValue("@PostCode", order.PostCode);
        command.Parameters.AddWithValue("@Country", order.Country);
        */
        command.ExecuteNonQuery();
    }

    public void DeleteSalesOrder(int id)
    {
        SqlConnection connection = GetConnection();
        SqlCommand command = connection.CreateCommand();
        command.CommandText = "DELETE FROM SalesOrders WHERE SalesOrderId = @SalesOrderId";
        command.Parameters.AddWithValue("@SalesOrderId", id);
        command.ExecuteNonQuery();

        SalesOrder? found = GetSalesOrderById(id);
        if (found != null) 
        {
            salesOrders.Remove(found);
        }
    }
}
