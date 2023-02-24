using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Linq;
using System.Security.Principal;
using TrabalhoDynacoop.Savio.Controller;

namespace TrabalhoDynacoop.Savio.Model
{
    internal class Account
    {
        public IOrganizationService ServiceClient { get; set; }

        public Account(IOrganizationService crmServiceClient)
        {
            this.ServiceClient = crmServiceClient;
        }

        public Account(CrmServiceClient crmServiceClient)
        {
            this.ServiceClient = crmServiceClient;
        }

        public Guid CreateAccount(string accountName, string accountCnpj, decimal marketCapitalization, int sharesOutstading, int companyRating, string creator)
        {
            VerifyAccount(accountCnpj);
            Guid position = CreatePosition(creator);

            Entity account = new Entity("account");
            account["name"] = accountName;
            account["tdc_cnpj"] = accountCnpj;
            account["marketcap"] = new Money(marketCapitalization);
            account["sharesoutstanding"] = sharesOutstading;
            account["accountclassificationcode"] = new OptionSetValue(companyRating);
            account["tdc_criado_pelo"] = new EntityReference("position", position);

            return this.ServiceClient.Create(account);
        }

        public Guid CreateAccount(string accountName, string accountCnpj, decimal marketCapitalization, int sharesOutstading, int companyRating, string creator, Guid contactId)
        {
            VerifyAccount(accountCnpj);
            Guid position = CreatePosition(creator);

            Entity account = new Entity("account");
            account["name"] = accountName;
            account["tdc_cnpj"] = accountCnpj;
            account["marketcap"] = new Money(marketCapitalization);
            account["sharesoutstanding"] = sharesOutstading;
            account["accountclassificationcode"] = new OptionSetValue(companyRating);
            account["tdc_criado_pelo"] = new EntityReference("position", position);
            account["primarycontactid"] = new EntityReference("contact", contactId);

            return this.ServiceClient.Create(account);
        }

        private Guid CreatePosition(string creator)
        {
            Entity position = new Entity("position");
            position["name"] = creator;
            return this.ServiceClient.Create(position);
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

        private void VerifyAccount(string accountCnpj)
        {
            bool existingAccount = GetAccountByCnpj(accountCnpj);
            if (existingAccount)
            {
                throw new Exception("It was not possible to create the account, because the CNPJ already exists :(");
            }
        }

        public Entity GetAccountById(Guid id, string[] columns)
        {
            return ServiceClient.Retrieve("account", id, new ColumnSet(columns));
        }

        public void IncrementOrDecrementNumberOfOpp(Entity oppAccount, bool? incrementOrDecrement)
        {
            int numberOfOpp = oppAccount.Contains("tdc_nmr_total_opp") ? (int)oppAccount["tdc_nmr_total_opp"] : 0;

            if (Convert.ToBoolean(incrementOrDecrement))
                numberOfOpp += 1;
            else
                numberOfOpp -= 1;

            oppAccount["tdc_nmr_total_opp"] = numberOfOpp;
            ServiceClient.Update(oppAccount);
        }
    }
}
