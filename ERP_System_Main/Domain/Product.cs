namespace ERP_System;

public enum Enhed
{
    Amount,
    Timer,
    Meter
}

public class Product
{
    public string ItemID { get; set; } = "";         // Unikt ID
    public string Name { get; set; } = "";               // Navn på produktet
    public string Description { get; set; } = "";        // Kort beskrivelse
    public decimal SalesPrice { get; set; }               // Pris kunden betaler
    public decimal BoughtPrice { get; set; }             // Pris du har givet
    public string Location { get; set; } = "";           // F.eks. "A12B"
    public decimal QuantityInStock { get; set; }            // Lagerantal i decimal
    public  int ProductId { get; set; }
    public Enhed Unit { get; set; }                     // F.eks. styk, meter

    // Beregn fortjeneste (salgspris - indkøbspris)
    public decimal Profit => SalesPrice - BoughtPrice;

    // Beregn avance i procent
    public decimal AvanceProcent => BoughtPrice == 0 ? 0 : (Profit / BoughtPrice) * 100;
}