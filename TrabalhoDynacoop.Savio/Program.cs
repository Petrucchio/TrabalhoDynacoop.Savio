using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Globalization;
using TrabalhoDynacoop.Savio.Controller;

namespace TrabalhoDynacoop.Savio
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CrmServiceClient serviceClient = Singleton.GetService();

            ContaController contaController = new ContaController(serviceClient);

            try
            {
                Console.Write("Enter account name: ");
                string nameAccount = Console.ReadLine();
                Console.Write("Enter the value of the company: ");
                decimal companyValue = decimal.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                Console.Write("Enter the number of shares outstanding: ");
                int sharesOutstading = int.Parse(Console.ReadLine());
                Console.Write("Enter the classification of the company (1 - Large 2 - Medium 3 - Small): ");
                int companyRating = int.Parse(Console.ReadLine());
                Console.Write("Created by: ");
                string creator = Console.ReadLine();

                Guid accountId = contaController.Create(nameAccount, companyValue, sharesOutstading, companyRating, creator);

                Console.WriteLine($@"https://trabalhodynamics.crm2.dynamics.com/main.aspx?appid=ee380667-3bae-ed11-9885-002248365eb3&pagetype=entityrecord&etn=account&id={accountId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
