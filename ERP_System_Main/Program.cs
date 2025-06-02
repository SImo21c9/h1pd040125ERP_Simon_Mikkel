using TECHCOOL.UI;
namespace ERP_System;

public class Program
{
    public static void Main(string[] args)
    {
     
        
        
        CompanyListPage companylistpage = new();
        CustomerListPage customerListPage = new();
        ProductDetailPage productListPage = new();
        SalesOrderList salesOrderList = new();
        Menu mainMenu = new();
        mainMenu.Add(companylistpage); 
        mainMenu.Add(customerListPage);
        mainMenu.Add(productListPage);
        mainMenu.Add(salesOrderList);
        Screen.Display(mainMenu);
        //i just added this to test the workflow runs 
    }
}