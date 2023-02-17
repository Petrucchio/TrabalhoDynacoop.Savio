using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Tooling.Connector;
using System;

namespace TrabalhoDynacoop.Savio.Model
{
    internal class Account
    {
        public CrmServiceClient ServiceClient { get; set; }

        public Account(CrmServiceClient crmServiceClient)
        {
            this.ServiceClient = crmServiceClient;
        }

        public Guid CreateAccount(string accountName, decimal marketCapitalization, int sharesOutstading, int companyRating, string creator)
        {
            Entity account = new Entity("account");
            account["name"] = accountName;
            account["marketcap"] = new Money(marketCapitalization);
            account["sharesoutstanding"] = sharesOutstading;
            account["accountclassificationcode"] = new OptionSetValue(companyRating);

            return this.ServiceClient.Create(account);
        }

        public Guid CreateAccount(string accountName, decimal marketCapitalization, int sharesOutstading, int companyRating, string creator, Guid contactId)
        {
            Entity account = new Entity("account");
            account["name"] = accountName;
            account["marketcap"] = new Money(marketCapitalization);
            account["sharesoutstanding"] = sharesOutstading;
            account["accountclassificationcode"] = new OptionSetValue(companyRating);
            account["primarycontactid"] = new EntityReference("account", contactId);

            return this.ServiceClient.Create(account);
        }
    }
}
