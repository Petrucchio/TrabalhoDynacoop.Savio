using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System;

namespace TrabalhoDynacoop.Savio.Model
{
    internal class Conta
    {
        public CrmServiceClient ServiceClient { get; set; }

        public Conta(CrmServiceClient crmServiceClient)
        {
            this.ServiceClient = crmServiceClient;
        }

        public Guid Create(string nameAccount, decimal companyValue, int sharesOutstading, int companyRating, string creator)
        {
            Entity conta = new Entity("account");

            conta["name"] = nameAccount;
            conta["tdc_valor_empresa"] = companyValue;
            conta["sharesoutstanding"] = sharesOutstading;
            conta["accountclassificationcode"] = new OptionSetValue(companyRating);
            conta["createdby"] = creator;

            Guid accountId = this.ServiceClient.Create(conta);
            return accountId;
        }
    }
}
