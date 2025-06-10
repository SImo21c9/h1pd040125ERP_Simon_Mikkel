using Microsoft.Data.SqlClient;

namespace ERP_System;
using TECHCOOL.UI;

// H�ndterer lagring og adgang til virksomhedsdata
public partial class Database
{
    private List<Product> products = new(); // Intern liste over virksomheder
    private int nextProductId = 1;

    // Hent produkt ud fra id
    public Product? GetProductById(int id)
    {
        foreach (var product in products)
        {
            if (product.ProductId == id)
            {
                return product;
            }
        }

        return null;
    }

    // Hent alle produkter
    public Product[] GetProducts()
    {
        List<Product> productslist = new List<Product>();
        SqlConnection connection = GetConnection();
        SqlCommand command = connection.CreateCommand();
        
        command.CommandText = "SELECT ProductId, ItemId, Name, Description, SalesPrice, BoughtPrice, Location, QuantityInStock, Unit FROM Products";
        SqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            Product product = new Product();
            product.ProductId = reader.GetInt32(0);
            product.ItemID = reader.GetString(1);
            product.Name = reader.GetString(2);
            product.Description = reader.GetString(3);
            product.SalesPrice = reader.GetDecimal(4);
            product.BoughtPrice = reader.GetDecimal(5);
            product.Location = reader.GetString(6);
            product.QuantityInStock = reader.GetDecimal(7);
            product.Unit = (Enhed)reader.GetInt32(8);
            productslist.Add(product);
        }
        reader.Close();
        return productslist.ToArray();
    }

    // Indsæt produkt
    public void AddProduct(Product product)
    {
        SqlConnection connection = GetConnection();
        SqlCommand command = connection.CreateCommand();
        command.CommandText = @"INSERT INTO Products (ItemId, Name, Description, BoughtPrice, SalesPrice, QuantityInStock, Location, Unit)
                                VALUES (@ItemId, @Name, @Description, @BoughtPrice, @SalesPrice, @QuantityInStock, @Location, @Unit); SELECT SCOPE_IDENTITY();";
        command.Parameters.AddWithValue("@ItemId", product.ItemID);
        command.Parameters.AddWithValue("@Name", product.Name);
        command.Parameters.AddWithValue("@Description", product.Description);
        command.Parameters.AddWithValue("@BoughtPrice", product.BoughtPrice);
        command.Parameters.AddWithValue("@SalesPrice", product.SalesPrice);
        command.Parameters.AddWithValue("@QuantityInStock", product.QuantityInStock);
        command.Parameters.AddWithValue("@Location", product.Location);
        command.Parameters.AddWithValue("@Unit", (int)product.Unit);

        products.Add(product);
    }

    // Opdater produkt
    public void UpdateProduct(Product product)
    {
        SqlConnection connection = GetConnection();
        connection.Open();
        SqlCommand command = connection.CreateCommand();
        command.CommandText = @"UPDATE Products SET ItemId = @ItemId, Name = @Name, Description = @Description, BoughtPrice = @BoughtPrice, SalesPrice = @SalesPrice, QuantityInStock = @QuantityInStock, Location = @Location, Unit = @Unit WHERE ProductId = @ProductId";
        command.Parameters.AddWithValue("@ItemId", product.ItemID);
        command.Parameters.AddWithValue("@Name", product.Name);
        command.Parameters.AddWithValue("@Description", product.Description);
        command.Parameters.AddWithValue("@BoughtPrice", product.BoughtPrice);
        command.Parameters.AddWithValue("@SalesPrice", product.SalesPrice);
        command.Parameters.AddWithValue("@QuantityInStock", product.QuantityInStock);
        command.Parameters.AddWithValue("@Location", product.Location);
        command.Parameters.AddWithValue("@Unit", (int)product.Unit);
        command.Parameters.AddWithValue("@ProductId", product.ProductId);

        command.ExecuteNonQuery();
    }

    // Slet produkt
    public void DeleteProduct(int id)
    {
        SqlConnection connection = GetConnection();
        connection.Open();
        SqlCommand command = connection.CreateCommand();
        command.CommandText = "DELETE FROM Products WHERE ProductId = @ProductId";
        command.Parameters.AddWithValue("@ProductId", id);
        command.ExecuteNonQuery();

        Product? found = GetProductById(id);
        if (found != null)
        {
            products.Remove(found);
        }
    }
}




