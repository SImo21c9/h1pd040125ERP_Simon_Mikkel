namespace ERP_System;
using TECHCOOL.UI;

public class CustomerListPage : Screen
{
    public override string Title { get; set; } = "Customer List";

    protected override void Draw()
    {
        ListPage<Customer> lp = new();

        lp.AddColumn("Customer ID", nameof(Customer.CustomerId));
        lp.AddColumn("Phone", nameof(Customer.PhoneNumber));
        lp.AddColumn("CompanyName", nameof(Customer.CompanyName));
        lp.AddColumn("LastPurchase", nameof(Customer.LastPurchase));
        lp.AddColumn("Email", nameof(Customer.Email));
        lp.AddColumn("First Name", nameof(Customer.FirstName));
        lp.AddColumn("Last Name", nameof(Customer.LastName));
        lp.AddColumn("Email", nameof(Customer.Email));
        lp.AddColumn("Street", nameof(Customer.Street));
        lp.AddColumn("Street Number", nameof(Customer.StreetNumber));
        lp.AddColumn("City", nameof(Customer.City));
        lp.AddColumn("PostCode", nameof(Customer.PostCode));
        lp.AddColumn("Country", nameof(Customer.Country));

        lp.AddKey(ConsoleKey.Escape, _ => Quit());
        lp.AddKey(ConsoleKey.F1, _ => CreateCustomer());
        lp.AddKey(ConsoleKey.F2, customer => EditCustomer(customer));
        lp.AddKey(ConsoleKey.Enter, customer => ShowCustomerDetails(customer));
        lp.AddKey(ConsoleKey.F5, customer => DeleteCustomer(customer));
   
        foreach (Customer customer in Database.Instance.GetCustomers())
        {
            lp.Add(customer);
        }
        
        //It will call CustomerEditpage and make a new customer 
        void CreateCustomer()
        {
            Customer newCustomer = new();
            Display(new CustomerEditPage(newCustomer));
            lp.Add(newCustomer);
        }
        
        
        void DeleteCustomer(Customer customer)
        {
            lp.Remove(customer);
            Database.Instance.DeleteCustomer(customer.CustomerId);
        }

        lp.Select();
    }
    
    
    private void EditCustomer(Customer? customer)
    {
        if (customer == null) return;
        Display(new CustomerEditPage(customer));
    }
    
    private void ShowCustomerDetails(Customer? customer)
    {
        if (customer == null) return;
        Display(new CustomerDetailsPage(customer));
    }
}