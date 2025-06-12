using Microsoft.Identity.Client.Advanced;

namespace ERP_System;
using TECHCOOL.UI;
using System.Linq;

public partial class CompanyListPage : Screen
{
    public override string Title { get; set; } = "Company";

    protected override void Draw()
    {
        ListPage<Company> lp = new(); // Opret listevisning for virksomheder
        // Tilføj kolonner til visning
        lp.AddColumn("Currency", nameof(Company .Currency));
        lp.AddColumn("Country", nameof(Company.Country));
        lp.AddColumn("Company Name", nameof(Company.CompanyName));
        lp.AddColumn("City", nameof(Company.City));
        lp.AddColumn("Street", nameof(Company.Street));
        lp.AddColumn("Street Number", nameof(Company.StreetNumber));
        lp.AddColumn("Address", nameof(Company.Address));

        lp.AddKey(ConsoleKey.Escape, quit);
        lp.AddKey(ConsoleKey.F1, createCompany);

        // Tilføj data fra databasen
        foreach (var company in Database.Instance.GetCompanies())
        {
            lp.Add(company);
        }

        Company? selected = lp.Select(); // Start interaktiv visning
    }

    void quit(Company _)
    {
        Quit();
    }
    void createCompany(Company _)
    {
        Company newCompany = new();
        Display(new CompanyEdit(newCompany));
    }
}
