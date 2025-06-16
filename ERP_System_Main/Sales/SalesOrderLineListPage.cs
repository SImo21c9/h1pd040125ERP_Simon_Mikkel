using System;
using TECHCOOL.UI;
using System.Collections.Generic;
using ERP_System;

namespace ERP_System_Main.Sales
{
    public class SalesOrderLineListPage : Screen
    {
        private readonly SalesOrder _salesOrder;

        public override string Title { get; set; } = "Order Lines";

        public SalesOrderLineListPage(SalesOrder salesOrder)
        {
            _salesOrder = salesOrder;
        }

        protected override void Draw()
        {
            ListPage<Orderline> lp = new();

            lp.AddColumn("Line ID", nameof(Orderline.OrderLineId));
            lp.AddColumn("Product Name", nameof(Orderline.Name));
            lp.AddColumn("Description", nameof(Orderline.Description));
            lp.AddColumn("Quantity", nameof(Orderline.Quantity));
            lp.AddColumn("Unit", nameof(Orderline.Unit));
            lp.AddColumn("Sales Price", nameof(Orderline.SalesPrice));
            lp.AddColumn("Bought Price", nameof(Orderline.BoughtPrice));
            lp.AddColumn("Profit", nameof(Orderline.Profit));
            lp.AddColumn("Profit %", nameof(Orderline.AvanceProcent));
            lp.AddColumn("Location", nameof(Orderline.Location));

            lp.AddKey(ConsoleKey.Escape, _ => Quit());
            lp.AddKey(ConsoleKey.F1, _ => AddOrderLine());
            lp.AddKey(ConsoleKey.F2, line => EditOrderLine(line));
            lp.AddKey(ConsoleKey.F5, line => DeleteOrderLine(line));

            foreach (var line in _salesOrder.Lines)
            {
                lp.Add(line);
            }

            lp.Select();
        }

        private void AddOrderLine()
        {
            var newLine = new Orderline
            {
                SalesOrderId = _salesOrder.SalesOrderId
            };
            // You may want to implement an OrderlineEdit screen for editing/creating lines
            // Display(new OrderlineEdit(newLine));
            _salesOrder.Lines.Add(newLine);
        }

        private void EditOrderLine(Orderline? line)
        {
            if (line == null) return;
            // Display(new OrderlineEdit(line));
        }

        private void DeleteOrderLine(Orderline? line)
        {
            if (line == null) return;
            _salesOrder.Lines.Remove(line);
        }
    }
}
