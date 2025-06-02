namespace ERP_System;
using TECHCOOL.UI;
using Microsoft.Data.SqlClient;

// H�ndterer lagring og adgang til virksomhedsdata
public partial class Database
{
    private List<Company> companies = new(); // Intern liste over virksomheder
    private int nextCompanyId = 1;

    // Finder og returnerer en virksomhed baseret p� dens ID, eller null hvis den ikke findes
    public Company? GetCompanyById(int id)
    {
        foreach (var company in companies)
        {
            if (company.CompanyId == id)
            {
                return company; // Fundet � return�r virksomheden
            }
        }
        return null; // Ikke fundet � return�r null
    }

    // Returnerer alle virksomheder i en array
    public Company[] GetCompanies()
    {
        return companies.ToArray(); // Konverterer listen til et array
    }

    // Tilføjer en virksomhed hvis den endnu ikke har et ID
    public void AddCompany(Company company)
    {
        if (company.CompanyId == 0)
        {
            company.CompanyId = nextCompanyId++;
            company.Name = company.CompanyName; // Sørg for at Name også er sat
            companies.Add(company);
        }
        SqlConnection connection = GetConnection();
        SqlCommand command = connection.CreateCommand();
        if (company.CompanyId == 0)
        {
            command.CommandText = "INSERT INTO Companies () VALUES (@GF)";
            command.Parameters.AddWithValue("@CompanyId", nextCompanyId);
            command.ExecuteNonQuery();

            SqlCommand getScopeIdentityCommand = connection.CreateCommand();
            getScopeIdentityCommand.CommandText = "SELECT SCOPE_IDENTITY()";
            SqlDataReader scopeReader = command.ExecuteReader();
            scopeReader.Read();

            company.CompanyId = (int) scopeReader.GetInt64(0);
            
        }
        else
        {
            command.CommandText = "UPDATE Products SET Name = @Name WHERE CompanyId = @CompanyId";
            command.ExecuteNonQuery();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Company companyAdd = new();
                companyAdd.CompanyId = reader.GetInt32(0);
                companyAdd.CompanyName = reader.GetString(50);
                companyAdd.Name = reader.GetString(60);
                companyAdd.Street = reader.GetString(100);
                companyAdd.StreetNumber = reader.GetString(101);
                companyAdd.City = reader.GetString(102);
                companyAdd.Address = GetAddressById(reader.GetInt32(4)); //(Address)reader.GetInt32(4);
                companyAdd.Country = (Country)reader.GetInt32(200);
                companyAdd.Currency = (Currency)reader.GetInt32(201);
            }
        }
    }

    public Address GetAddressById(int id)
    {

    }

    // Opdaterer en eksisterende virksomhed, hvis ID findes
    public void UpdateCompany(Company company)
    {
        if (company.CompanyId == 0)
        {
            AddCompany(company);
            return; // ID ikke angivet � kan ikke opdatere
        }

        Company? oldCompany = GetCompanyById(company.CompanyId);
        if (oldCompany == null)
        {
            return; // Virksomheden findes ikke
        }

        oldCompany.CompanyName = company.CompanyName;
        oldCompany.Name = company.CompanyName; 
        oldCompany.Street = company.Street;
        oldCompany.StreetNumber = company.StreetNumber;
        oldCompany.City = company.City;
        oldCompany.Address = company.Address;
        oldCompany.Country = company.Country;
        oldCompany.Currency = company.Currency;
        
    }

    // Sletter en virksomhed baseret p� ID
    public void DeleteCompany(int id)
    {
        Company? found = GetCompanyById(id);
        if (found != null)
        {
            companies.Remove(found);
        }
    }
}