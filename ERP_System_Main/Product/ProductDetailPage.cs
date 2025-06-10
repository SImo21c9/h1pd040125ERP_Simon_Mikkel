using TECHCOOL.UI;

namespace ERP_System;

public class ProductDetailPage : Screen
{
    public override string Title { get; set; } = "Product";

    protected override void Draw()
    {
        ListPage<Product> lp = new();
        lp.AddKey(ConsoleKey.F1, createProduct);
        lp.AddKey(ConsoleKey.F2, editProduct);
        lp.AddKey(ConsoleKey.F5, deleteProduct);

        lp.AddColumn("ProductNumber", nameof(Product.ItemID));
        lp.AddColumn("Name", nameof(Product.Name));
        lp.AddColumn("Description", nameof(Product.Description));
        lp.AddColumn("SalesPrice", nameof(Product.SalesPrice));
        lp.AddColumn("BoughtPrice", nameof(Product.BoughtPrice));
        lp.AddColumn("Location", nameof(Product.Location));
        lp.AddColumn("QuantityInStock", nameof(Product.QuantityInStock));
        lp.AddColumn("Unit", nameof(Product.Unit));
        lp.AddColumn("Profit %", nameof(Product.AvanceProcent));
        lp.AddColumn("Profit", nameof(Product.Profit));

        foreach (Product product in Database.Instance.GetProducts())
        {
            lp.Add(product);
        }

        Product? selected = lp.Select();
        if (selected != null)
        {
            Display(new ProductEdit(selected));
        }

        void createProduct(Product _)
        {
            Product newProduct = new();
            Display(new ProductEdit(newProduct));
        }

        void editProduct(Product product)
        {
            Display(new ProductEdit(product));
        }

        void deleteProduct(Product product)
        {
            Database.Instance.DeleteProduct(product.ProductId);
            Display(this);
        }
    }
}