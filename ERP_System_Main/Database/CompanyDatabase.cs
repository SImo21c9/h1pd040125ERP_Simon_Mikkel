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
        SqlConnection connection = GetConnection();
        connection.Open();
        SqlCommand command = connection.CreateCommand();
        command.CommandText = "SELECT CompanyId, Street, StreetNumber, PostCode, Country, Currency, Address, CompanyName";
        command.ExecuteReader();
        SqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            Company companyAdd = new();
            companyAdd.CompanyId = reader.GetInt32(0);
            companyAdd.CompanyName = reader.GetString(50);
            companyAdd.Street = reader.GetString(100);
            companyAdd.StreetNumber = reader.GetString(101);
            companyAdd.City = reader.GetString(102);
            companyAdd.Address = GetAddressById(reader.GetInt32(4)); //(Address)reader.GetInt32(4);
            companyAdd.Country = (Country)reader.GetInt32(200);
            companyAdd.Currency = (Currency)reader.GetInt32(201);
        }
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
            command.CommandText = "INSERT INTO Companies (CompanyId, CompanyName, Street, StreetNumber, City, Address, Country, Currency) VALUES (@CompanyId, @CompanyName, @Street, @StreetNumber, @City, @Address, @Country, @Currency)";
            command.Parameters.AddWithValue("@CompanyId", nextCompanyId);
            command.ExecuteReader();

            SqlCommand getScopeIdentityCommand = connection.CreateCommand();
            getScopeIdentityCommand.CommandText = "SELECT SCOPE_IDENTITY()";
            SqlDataReader scopeReader = command.ExecuteReader();
            scopeReader.Read();

            company.CompanyId = (int) scopeReader.GetInt64(0);
         
            
        }
    }

    public Address GetAddressById(int id) //shit does not work 
    {
        throw new NotImplementedException(); // this is not finished 
    }

    // Opdaterer en eksisterende virksomhed, hvis ID findes
    public void UpdateCompany(Company company)
    {
        SqlConnection connection = GetConnection();
        SqlCommand command = connection.CreateCommand();

        connection.Open();
        command.ExecuteNonQuery();
        command.CommandText = @"UPDATE Companies SET CompanyName = @CompanyName, 
                     Name = @Name, Street = @Street, StreetNumber = @StreetNumber,
                     City = @City, PostCode = @PostCode, Country = @Country,
                     Currency = @Currency";
        command.Parameters.AddWithValue("@CompanyName", company.CompanyName);
        command.Parameters.AddWithValue("@Name", company.Name);
        command.Parameters.AddWithValue("@Street", company.Street);
        command.Parameters.AddWithValue("@StreetNumber", company.StreetNumber);
        command.Parameters.AddWithValue("@City", company.City);
        command.Parameters.AddWithValue("@PostCode", company.PostCode);
        command.Parameters.AddWithValue("@Country", company.Country);
        command.Parameters.AddWithValue(@"Currency", company.Currency);
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