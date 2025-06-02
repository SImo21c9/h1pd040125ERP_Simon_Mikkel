using System.Data;
using Microsoft.Data.SqlClient;

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
        return salesOrders.ToArray();
    }

    public void AddSalesOrder(SalesOrder order)
    {
        SqlConnection connection = GetConnection();
        SqlCommand command = connection.CreateCommand();
        if (order.SalesOrderId == 0)
        {
            command.CommandText = "INSERT INTO Products () VALUES (@GF)";
            command.Parameters.AddWithValue("@SalesOrderId", nextSalesOrderId);
            
            order.SalesOrderId = nextSalesOrderId++;
            order.Name = order.OrderNumber;
            salesOrders.Add(order);
            connection.Open();
        }
        else
        {
            command.CommandText = "GET ALL ";
            SqlDataReader reader = command.ExecuteReader();
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
                connection.Open();
            }
        }
    }
    
 
    public void UpdateSalesOrder(SalesOrder order)
    {
        if (order.SalesOrderId == 0)
        {
            AddSalesOrder(order);
            return;
        }

        SalesOrder? oldOrder = GetSalesOrderById(order.SalesOrderId);
        if (oldOrder == null)
        {
            return;
        }

        oldOrder.OrderNumber = order.OrderNumber;
        oldOrder.Name = order.OrderNumber;
        oldOrder.Customer = order.Customer;
        oldOrder.Date = order.Date;
        oldOrder.TotalAmount = order.TotalAmount;
        oldOrder.Status = order.Status;
        oldOrder.Products = order.Products;
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
