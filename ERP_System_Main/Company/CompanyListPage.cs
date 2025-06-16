using TECHCOOL.UI;

namespace ERP_System;

public partial class CompanyListPage : Screen
{
    public override string Title { get; set; } = "Company List";

    protected override void Draw()
    {
        ListPage<Company> lp = new();
        lp.AddColumn("Firmanavn", nameof(Company.CompanyName));
        lp.AddColumn("Land", nameof(Company.Country));
        lp.AddColumn("Valuta", nameof(Company.Currency));

        lp.AddKey(ConsoleKey.Escape, _ => Quit());
        lp.AddKey(ConsoleKey.F1, _ => CreateCompany());
        lp.AddKey(ConsoleKey.F2, company => EditCompany(company));
        lp.AddKey(ConsoleKey.F5, company => DeleteCompany(company));

        foreach (var company in Database.Instance.GetCompanies())
        {
            lp.Add(company);
        }

        lp.Select();
    }

    private void CreateCompany()
    {
        Company newCompany = new();
        Display(new CompanyEdit(newCompany));
    }

    private void EditCompany(Company? company)
    {
        if (company == null) return;
        Display(new CompanyEdit(company));
    }

    private void DeleteCompany(Company? company)
    {
        if (company == null) return;
        Database.Instance.DeleteCompany(company.CompanyId);
    }
}
