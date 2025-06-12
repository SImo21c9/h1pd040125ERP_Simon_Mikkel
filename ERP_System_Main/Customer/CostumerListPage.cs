namespace ERP_System;
using TECHCOOL.UI;

public class CustomerListPage : Screen
{
    public override string Title { get; set; } = "Customer List";

    protected override void Draw()
    {
        ListPage<Customer> lp = new();

        lp.AddColumn("Customer ID", nameof(Customer.CustomerId));
        lp.AddColumn("Name", nameof(Customer.FullName));
        lp.AddColumn("Phone", nameof(Customer.PhoneNumber));
        lp.AddColumn("Email", nameof(Customer.Email));

        lp.AddKey(ConsoleKey.Escape, _ => Quit());
        lp.AddKey(ConsoleKey.F1, _ => CreateCustomer());
        lp.AddKey(ConsoleKey.F2, customer => EditCustomer(customer));
        lp.AddKey(ConsoleKey.Enter, customer => ShowCustomerDetails(customer));

        foreach (Customer customer in Database.Instance.GetCustomers())
        {
            lp.Add(customer);
        }

        lp.Select();
    }

    private void CreateCustomer()
    {
        Customer newCustomer = new();
        Display(new CustomerEditPage(newCustomer));
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