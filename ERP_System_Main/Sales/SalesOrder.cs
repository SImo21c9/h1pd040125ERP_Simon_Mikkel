namespace ERP_System;

using ERP_System_Main.Sales;
using Mysqlx.Resultset;
using System.Collections.Generic;

public enum SalesOrderStatus { Created, Cancelled, Done }

public class SalesOrder
{
    public int SalesOrderId { get; set; }           // Intern ID
    public string OrderNumber { get; set; } = "";   // Ekstern ID
    public Customer Customer { get; set; }      // Kundens navn eller ID
    public int CustomerId { get; set; }     // Kundens navn eller ID
    public DateTime OrderDate { get; set; }              // Dato for ordren
    public SalesOrderStatus Status { get; set; } = SalesOrderStatus.Created;        // Fx "Afsluttet", "Åben", "Annulleret"
    public List<Orderline> Lines { get; set; } = new(); // Liste over varelinjer
    public string Name { get; set; } = "";          // Bruges til visning i UI
    public decimal TotalAmount { get; set; }        // Totalbeløb


}