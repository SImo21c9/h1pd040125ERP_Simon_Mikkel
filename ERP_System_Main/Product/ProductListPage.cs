using TECHCOOL.UI;
namespace ERP_System;

public class ProductListPage : Screen
{
    public override string Title { get; set; } = "Product List";
    private Product _product;

    public ProductListPage(Product product)
    {
        _product = product;
    }

    protected override void Draw()
    {
        Clear();
        ListPage<Product> lp = new();
        lp.AddColumn("ProductNumber", nameof(Product.ItemID));
        lp.AddColumn("Name", nameof(Product.Name));
        lp.AddColumn("Stock", nameof(Product.QuantityInStock));
        lp.AddColumn("Bought Price", nameof(Product.BoughtPrice));
        lp.AddColumn("Sales Price", nameof(Product.SalesPrice));
        lp.AddColumn("Profit", nameof(Product.Profit));
        lp.AddColumn("Profit %", nameof(Product.AvanceProcent));

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
}