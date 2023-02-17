using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Linq;
using TrabalhoDynacoop.Savio.Controller;

namespace TrabalhoDynacoop.Savio.Model
{
    internal class Account
    {
        public CrmServiceClient ServiceClient { get; set; }

        public Account(CrmServiceClient crmServiceClient)
        {
            this.ServiceClient = crmServiceClient;
        }

        public Guid CreateAccount(string accountName, string accountCnpj, decimal marketCapitalization, int sharesOutstading, int companyRating, string creator)
        {
            //bool existingAccount = GetAccountByCnpj(accountCnpj);
            //if (existingAccount)
            //{
            //    throw new Exception("It was not possible to create the account, because the CNPJ already exists :(");
            //}

            Entity account = new Entity("account");
            account["name"] = accountName;
            account["tdc_cnpj"] = accountCnpj;
            account["marketcap"] = new Money(marketCapitalization);
            account["sharesoutstanding"] = sharesOutstading;
            account["accountclassificationcode"] = new OptionSetValue(companyRating);

            return this.ServiceClient.Create(account);
        }

        public Guid CreateAccount(string accountName, string accountCnpj, decimal marketCapitalization, int sharesOutstading, int companyRating, string creator, Guid contactId)
        {
            Entity account = new Entity("account");
            account["name"] = accountName;
            account["tdc_cnpj"] = accountCnpj;
            account["marketcap"] = new Money(marketCapitalization);
            account["sharesoutstanding"] = sharesOutstading;
            account["accountclassificationcode"] = new OptionSetValue(companyRating);
            account["primarycontactid"] = new EntityReference("account", contactId);

            return this.ServiceClient.Create(account);
        }

        public bool GetAccountByCnpj(string cnpj)
        {
            QueryExpression queryExpression = new QueryExpression("account");
            queryExpression.ColumnSet.AddColumn("name");
            queryExpression.Criteria.AddCondition("tdc_cnpj", ConditionOperator.Equal, cnpj);
            EntityCollection account = this.ServiceClient.RetrieveMultiple(queryExpression);

            if (account.Entities.Count > 0)
                return true;
            else
                return false;
        }
    }
}
