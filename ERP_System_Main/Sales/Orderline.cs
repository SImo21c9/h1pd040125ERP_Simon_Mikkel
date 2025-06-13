using ERP_System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_System_Main.Sales
{
    public class Orderline
    {
        public string OrderLineId { get; set; }         // Unikt ID
        public int SalesOrderId { get; set; }
        public string Name { get; set; } = "";               // Navn på produktet
        public string Description { get; set; } = "";        // Kort beskrivelse
        public decimal SalesPrice { get; set; }               // Pris kunden betaler
        public decimal BoughtPrice { get; set; }             // Pris du har givet
        public string Location { get; set; } = "";           // F.eks. "A12B"
        public decimal Quantity { get; set; }            // Lagerantal i decimal
        public int ProductId { get; set; }
        public Enhed Unit { get; set; }                     // F.eks. styk, meter

        // Beregn fortjeneste (salgspris - indkøbspris)
        public decimal Profit => SalesPrice - BoughtPrice;

        // Beregn avance i procent
        public decimal AvanceProcent => BoughtPrice == 0 ? 0 : (Profit / BoughtPrice) * 100;
    }
}
