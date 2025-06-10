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

        
        /* here is a quick summary on why the code below has turned into comments. The reason is it was useless, and we have moved into a new better system like between 
        line 23-24. remember to do this with all the other listpages and editpages */
    //     ConsoleKeyInfo keyInfo = Console.ReadKey(true);
    //     switch (keyInfo.Key)
    //     {
    //         case ConsoleKey.F2: // Rediger valgte virksomhed
    //             if (selected != null)
    //             {
    //                 Display(new CompanyEdit(selected));
    //             }
    //             break;
    //
    //         case ConsoleKey.F5: // Slet valgte virksomhed
    //             if (selected != null)
    //             {
    //                 Database.Instance.DeleteCompany(selected.CompanyId);
    //                 Display(new CompanyListPage()); // Opdater visningen
    //             }
    //             break;
    //         default:
    //             if (selected != null)
    //             {
    //                 Display(new CompanyInfo(selected));
    //             }
    //             else
    //             {
    //                 Quit();
    //             }
    //             break;
    //     }
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
