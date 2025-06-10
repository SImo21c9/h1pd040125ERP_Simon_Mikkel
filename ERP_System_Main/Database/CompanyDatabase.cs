namespace ERP_System;
using TECHCOOL.UI;
using Microsoft.Data.SqlClient;

// Håndterer lagring og adgang til virksomhedsdata
public partial class Database
{
    private List<Company> companies = new(); // Intern liste over virksomheder
    private int nextCompanyId = 1;

    // Finder og returnerer en virksomhed baseret på dens ID, eller null hvis den ikke findes
    public Company? GetCompanyById(int id)
    {
        foreach (var company in companies)
        {
            if (company.CompanyId == id)
            {
                return company; 
            }
        }
        return null; 
    }


    public Company[] GetCompanies()
    {
        List<Company> companiesList = new List<Company>();
        SqlConnection connection = GetConnection();
        SqlCommand command = connection.CreateCommand();
        
        command.CommandText = "SELECT CompanyId, CompanyName, Name, Street, StreetNumber, City, PostCode, Country, Currency FROM Companies";
        SqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            Company company = new Company();
            company.CompanyId = reader.GetInt32(0);
            company.CompanyName = reader.GetString(1);
            company.Name = reader.GetString(2);
            company.Street = reader.GetString(3);
            company.StreetNumber = reader.GetString(4);
            company.City = reader.GetString(5);
            company.PostCode = reader.GetString(6);
            company.Country = (Country)Enum.Parse(typeof(Country), reader.GetString(7));
            company.Currency = (Currency)Enum.Parse(typeof(Currency), reader.GetString(8));
            companiesList.Add(company);
        }
        reader.Close();
        return companiesList.ToArray();
    }


    public void AddCompany(Company company)
    {
        SqlConnection connection = GetConnection();
        SqlCommand command = connection.CreateCommand();
        command.CommandText = @"INSERT INTO Companies (CompanyName, Name, Street, StreetNumber, City, PostCode, Country, Currency) 
                                   VALUES (@CompanyName, @Name, @Street, @StreetNumber, @City, @PostCode, @Country, @Currency);
                                   SELECT SCOPE_IDENTITY();";
            
        command.Parameters.AddWithValue("@CompanyName", company.CompanyName);
        command.Parameters.AddWithValue("@Name", company.Name );
        command.Parameters.AddWithValue("@Street", company.Street );
        command.Parameters.AddWithValue("@StreetNumber", company.StreetNumber );
        command.Parameters.AddWithValue("@City", company.City);
        command.Parameters.AddWithValue("@PostCode", company.PostCode);
        command.Parameters.AddWithValue("@Country", (int)company.Country);
        command.Parameters.AddWithValue("@Currency", (int)company.Currency);
        
        companies.Add(company);
    }


    public void UpdateCompany(Company company)
    {
        SqlConnection connection = GetConnection();
        try
        {
            SqlCommand command = connection.CreateCommand();
            command.CommandText = @"UPDATE Companies SET 
                                   CompanyName = @CompanyName, 
                                   Name = @Name, 
                                   Street = @Street, 
                                   StreetNumber = @StreetNumber,
                                   City = @City, 
                                   PostCode = @PostCode, 
                                   Country = @Country,
                                   Currency = @Currency
                                   WHERE CompanyId = @CompanyId";

            command.Parameters.AddWithValue("@CompanyId", company.CompanyId);
            command.Parameters.AddWithValue("@CompanyName", company.CompanyName);
            command.Parameters.AddWithValue("@Name", company.Name);
            command.Parameters.AddWithValue("@Street", company.Street);
            command.Parameters.AddWithValue("@StreetNumber", company.StreetNumber );
            command.Parameters.AddWithValue("@City", company.City);
            command.Parameters.AddWithValue("@PostCode", company.PostCode);
            command.Parameters.AddWithValue("@Country", (int)company.Country);
            command.Parameters.AddWithValue("@Currency", (int)company.Currency);

            command.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    // Sletter en virksomhed baseret på ID
    public void DeleteCompany(int id)
    {
        SqlConnection connection = GetConnection();
        SqlCommand command = connection.CreateCommand();
        command.CommandText = "DELETE FROM Companies WHERE CompanyId = @CompanyId";
        command.Parameters.AddWithValue("@CompanyId", id);
        command.ExecuteNonQuery();
            
          
        Company? found = GetCompanyById(id);
        if (found != null)
        {
            companies.Remove(found);
        }
    }

    public Address GetAddressById(int id)
    {
    
        throw new NotImplementedException("hasn't been implemented yet");
    }
}