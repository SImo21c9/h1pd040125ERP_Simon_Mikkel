using Microsoft.Data.SqlClient;

namespace ERP_System;
using TECHCOOL.UI;

// H�ndterer lagring og adgang til virksomhedsdata
public partial class Database
{
    private List<Product> products = new(); // Intern liste over virksomheder
    private int nextProductId = 1;

    // Finder og returnerer en virksomhed baseret p� dens ID, eller null hvis den ikke findes
    public Product? GetProductById(int id)
    {
        foreach (var product in products)
        {
            if (product.ProductId == id)
            {
                return product; // Fundet � return�r virksomheden
            }
        }
        return null; // Ikke fundet � return�r null
    }

    // Returnerer alle virksomheder i en array
    public Product[] GetProducts()
    {
        SqlConnection connection = GetConnection();
        connection.Open();
        SqlCommand command = connection.CreateCommand();
        command.CommandText = "SELECT ProductId, ItemId, Name, Description, SalesPrice, BoughtPrice, Location, QuantityInStock, Unit";
        command.ExecuteReader();
        SqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            Product product = new();
            product.ProductId = reader.GetInt32(0);
            
        }
    }

    // Tilføjer en virksomhed hvis den endnu ikke har et ID
    public void AddProduct(Product product)
    {
        
        SqlConnection connection = GetConnection();
        SqlCommand command = connection.CreateCommand();
        command.CommandText = @"INSERT INTO Products (Name, Description, BoughtPrice, 
        SalesPrice, QuantityInStock, Location, Unit) VALUES (@Name,@Description, @BoughttPrice, @SalesPrice, @QuantityInStock, @Location, @Unit)";
        command.Parameters.AddWithValue("@Name", product.Name); 
        command.Parameters.AddWithValue("@Description", product.Description); 
        command.Parameters.AddWithValue("@BoughtPrice", product.BoughtPrice); 
        command.Parameters.AddWithValue("SalesPrice", product.SalesPrice); 
        command.Parameters.AddWithValue("QuantityInStock", product.QuantityInStock); 
        command.Parameters.AddWithValue("Location", product.Location);
        command.Parameters.AddWithValue("Unit", product.Unit);
    }

    // Opdaterer en eksisterende virksomhed, hvis ID findes
    public void UpdateProduct(Product product)
    {
        SqlConnection connection = GetConnection();
        SqlCommand command = connection.CreateCommand();
        connection.Open();
        command.ExecuteNonQuery();
        command.CommandText = @"Update Product SET Name = @Name, Description = @Description, 
BoughtPrice = @BoughtPrice, SalesPrice = @SalesPrice, QuantityInStock = @QuantityInStock, 
Location = @Location, Unit = @Unit";

        command.Parameters.AddWithValue("@Name", product.Name);
        command.Parameters.AddWithValue("@Description", product.Description);
        command.Parameters.AddWithValue("@BoughtPrice", product.BoughtPrice);
        command.Parameters.AddWithValue("SalesPrice", product.SalesPrice);
        command.Parameters.AddWithValue("QuantityInStock", product.QuantityInStock);
        command.Parameters.AddWithValue("Location", product.Location);
        command.Parameters.AddWithValue("Unit", product.Unit);
    }

    // Sletter en virksomhed baseret p� ID
    public void DeleteProduct(int id)
    {
        Product? found = GetProductById(id);
        if (found != null)
        {
            products.Remove(found);
        }
    }
}




