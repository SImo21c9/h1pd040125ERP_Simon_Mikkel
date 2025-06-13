namespace ERP_System;
using TECHCOOL.UI;

public class SalesOrderEdit : Screen
{
    public override string Title { get; set; } = "SalesOrder Edit ";
    private SalesOrder _salesorder;

    public SalesOrderEdit(SalesOrder salesorder)
    {
        _salesorder = salesorder;
    }

    protected override void Draw()
    {
        Form<SalesOrder> editForm = new();

        
        editForm.TextBox("Order Number", nameof(SalesOrder.OrderNumber));
        editForm.IntBox("Kunde id", nameof(SalesOrder.CustomerId));
        editForm.SelectBox("Status", nameof(SalesOrder.Status));
        editForm.AddOption("Status", "Bekrafted", SalesOrderStatus.Created);
        editForm.AddOption("Status", "Annuleret", SalesOrderStatus.Cancelled);
        editForm.AddOption("Status", "Afsluttet", SalesOrderStatus.Done);


        if (editForm.Edit(_salesorder))
        {
            Database.Instance.UpdateSalesOrder(_salesorder);
        }
        Quit();
    }
}