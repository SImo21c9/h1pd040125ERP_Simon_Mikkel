﻿using TECHCOOL.UI;
namespace ERP_System;

public class CustomerEditPage : Screen
{
    public override string Title { get; set; } = "Customer";
    private Customer _customer;
    public CustomerEditPage(Customer customer)
    {
        _customer = customer;
    }

    protected override void Draw()
    {
        Form<Customer> editForm = new();
        editForm.TextBox("First Name", nameof(Customer.FirstName));
        editForm.TextBox("Last Name", nameof(Customer.LastName));
        editForm.TextBox("Email", nameof(Customer.Email));
        editForm.TextBox("Street", nameof(Customer.Street));
        editForm.TextBox("Street Number", nameof(Customer.StreetNumber));
        editForm.TextBox("City", nameof(Customer.City));
        editForm.TextBox("PostCode", nameof(Customer.PostCode));
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
        
        editForm.IntBox("CustomerId", nameof(Customer.CustomerId));

        if (editForm.Edit(_customer))
        {
            Database.Instance.UpdateCustomer(_customer);
        }
        Quit();
    }
}