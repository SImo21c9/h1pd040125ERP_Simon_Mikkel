using TECHCOOL.UI;
using TECHCOOL.UI;
namespace ERP_System;

public class ProductListPage : Screen
{
    public override string Title { get; set; } = "Product List";
    private Product _product;

    protected override void Draw()
    {
        Clear();
        ListPage<Product> lp = new();
        lp.AddColumn("ProductNumber", nameof(Product.ItemID));
        lp.AddColumn("Name", nameof(Product.Name));
        lp.AddColumn("Stock", nameof(Product.QuantityInStock));
        lp.AddColumn("Bought Price", nameof(Product.BoughtPrice));
        lp.AddColumn("Sales Price", nameof(Product.SalesPrice));
        lp.AddColumn("ProfitMargin", nameof(Product.Profit));
        lp.AddColumn("Profit %", nameof(Product.AvanceProcent));

        lp.AddKey(ConsoleKey.Escape, quit);
        lp.AddKey(ConsoleKey.F1, createProduct);

        foreach (var product in Database.Instance.GetProducts())
        {
            lp.Add(product);
        }

        Product? selected = lp.Select();
        if (selected != null)
        {
            Display(new ProductEdit(selected));
        }
    }

    void quit(Product _)
    {
        Quit();
    }
    void createProduct(Product _)
    {
        Product newProduct = new();
        Display(new ProductEdit(newProduct));
    }
}