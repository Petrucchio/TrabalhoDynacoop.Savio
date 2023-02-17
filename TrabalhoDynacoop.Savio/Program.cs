using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Globalization;
using TrabalhoDynacoop.Savio.Controller;
using TrabalhoDynacoop.Savio.Model;

namespace TrabalhoDynacoop.Savio
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CrmServiceClient serviceClient = Singleton.GetService();

            AccountController accountController = new AccountController(serviceClient);
            ContactController contactController = new ContactController(serviceClient);

            try
            {
                Console.WriteLine("ACCOUNT DETAILS");
                Console.Write("Enter account name: ");
                string accountName = Console.ReadLine();
                Console.Write("Enter the market capitalization value: ");
                decimal marketCapitalization = decimal.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                Console.Write("Enter the number of shares outstanding: ");
                int sharesOutstading = int.Parse(Console.ReadLine());
                Console.Write("Enter the classification of the company (1 - Large 2 - Medium 3 - Small): ");
                int companyRating = int.Parse(Console.ReadLine());
                Console.Write("Created by: ");
                string creator = Console.ReadLine();

                Console.Write("Do you want to create a contact for this account? (Y/N) ");
                string anwserToCreateContact = Console.ReadLine();

                switch (anwserToCreateContact.ToUpper().ToString())
                {
                    case "Y":
                        Console.WriteLine();
                        Console.WriteLine("CONTACT DETAILS");
                        Console.Write("Enter contact name: ");
                        string contactName = Console.ReadLine();
                        Console.Write("Enter job title: ");
                        string jobTitle = Console.ReadLine();
                        Console.Write("Enter contact age: ");
                        int contactAge = int.Parse(Console.ReadLine());

                        Guid contactId = contactController.CreateContact(contactName, jobTitle, contactAge);
                        Guid accountId = accountController.CreateAccount(accountName, marketCapitalization, sharesOutstading, companyRating, creator, contactId);

                        Console.WriteLine($@"https://trabalhodynamics.crm2.dynamics.com/main.aspx?appid=ee380667-3bae-ed11-9885-002248365eb3&pagetype=entityrecord&etn=account&id={accountId}");
                        
                        break;
                    case "N":
                        accountId = accountController.CreateAccount(accountName, marketCapitalization, sharesOutstading, companyRating, creator);
                        Console.WriteLine($@"https://trabalhodynamics.crm2.dynamics.com/main.aspx?appid=ee380667-3bae-ed11-9885-002248365eb3&pagetype=entityrecord&etn=account&id={accountId}");
                        
                        break;
                    default:
                        Console.WriteLine("Invalid option :(");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
