using TECHCOOL.UI;
namespace ERP_System;

public class CompanyEdit : Screen
{
    public override string Title { get; set; } = "Company";
    private Company _company;
    private bool _isNewCompany;

    public CompanyEdit(Company company)
    {
        _company = company;
        _isNewCompany = company.CompanyId == 0; // If ID is 0, it's a new company
    }
    
    protected override void Draw()
    {
        Form<Company> editForm = new();
        editForm.TextBox("Virksomhed", nameof(Company.CompanyName));
        editForm.TextBox("Name", nameof(Company.Name));
        editForm.TextBox("Street", nameof(Company.Street));
        editForm.TextBox("Street Number", nameof(Company.StreetNumber));
        editForm.TextBox("City", nameof(Company.City));
        editForm.TextBox("Post Code", nameof(Company.PostCode));
        
        editForm.SelectBox("Currency", "Currency");
        editForm.AddOption("Currency", "Dansk kroner", Currency.DKK);
        editForm.AddOption("Currency", "Svenske kroner", Currency.SEK);
        editForm.AddOption("Currency", "Euro", Currency.EUR);
        editForm.AddOption("Currency", "US Dollar", Currency.USD);
        editForm.AddOption("Currency", "Ruble", Currency.RUB);
        
        editForm.SelectBox("Country", "Country");
        Country[] topGdpCountries = new[]
        {
            Country.UnitedStates,
            Country.Denmark,
            Country.China,
            Country.Germany,
            Country.Sweden
        };
        foreach (var country in topGdpCountries)
        {
            editForm.AddOption("Country", country.ToString(), country);
        }

        if (editForm.Edit(_company))
        {
            // User pressed Enter/confirmed changes
            if (_isNewCompany)
            {
                Database.Instance.AddCompany(_company);
            }
            else
            {
                Database.Instance.UpdateCompany(_company);
            }
        }

        Quit();
    }
}