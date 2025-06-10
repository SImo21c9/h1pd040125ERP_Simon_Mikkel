using TECHCOOL.UI;
namespace ERP_System;

public class Program
{
    public static void Main(string[] args)
    {
        new Database();

        CompanyListPage companyListPage = new();
        CustomerListPage customerListPage = new();
        ProductDetailPage productListPage = new();
        SalesOrderList salesOrderList = new();


        Menu mainMenu = new();
        Console.WriteLine("Main Menu");



        mainMenu.Add(companyListPage);
        mainMenu.Add(customerListPage);
        mainMenu.Add(productListPage);
        mainMenu.Add(salesOrderList);



        Screen.Display(mainMenu);

        Console.ReadLine();
    }
}