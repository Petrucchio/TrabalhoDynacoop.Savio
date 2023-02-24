using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System;
using TrabalhoDynacoop.Savio.Model;

namespace TrabalhoDynacoop.Savio.Controller
{
    internal class AccountController
    {
        public IOrganizationService ServiceClient { get; set; }

        public Account Account { get; set; }

        public AccountController(IOrganizationService serviceClient)
        {
            ServiceClient = serviceClient;
            this.Account = new Account(ServiceClient);
        }

        public AccountController(CrmServiceClient serviceClient)
        {
            ServiceClient = serviceClient;
            this.Account = new Account(ServiceClient);
        }

        public Guid CreateAccount(string accountName, string accountCnpj, decimal marketCapitalization, int sharesOutstading, int companyRating, string creator)
        {
            return Account.CreateAccount(accountName, accountCnpj, marketCapitalization, sharesOutstading, companyRating, creator);
        }

        public Guid CreateAccount(string accountName, string accountCnpj, decimal marketCapitalization, int sharesOutstading, int companyRating, string creator, Guid contactId)
        {
            return Account.CreateAccount(accountName, accountCnpj, marketCapitalization, sharesOutstading, companyRating, creator, contactId);
        }

        public bool GetAccountByCnpj(string cnpj)
        {
            return Account.GetAccountByCnpj(cnpj);
        }

        public Entity GetAccountById(Guid id, string[] columns)
        {
            return Account.GetAccountById(id, columns);
        }

        public void IncrementOrDecrementNumberOfOpp(Entity oppAccount, bool? incrementOrDecrement)
        {
            Account.IncrementOrDecrementNumberOfOpp(oppAccount, incrementOrDecrement);
        }
    }
}
