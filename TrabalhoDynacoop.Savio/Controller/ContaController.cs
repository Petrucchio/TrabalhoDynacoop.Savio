using Microsoft.Xrm.Tooling.Connector;
using System;
using TrabalhoDynacoop.Savio.Model;

namespace TrabalhoDynacoop.Savio.Controller
{
    internal class ContaController
    {
        public CrmServiceClient ServiceClient { get; set; }

        public Conta Conta { get; set; }

        public ContaController(CrmServiceClient serviceClient)
        {
            ServiceClient = serviceClient;
            this.Conta = new Conta(ServiceClient);
        }

        public Guid Create(string nameAccount, decimal companyValue, int sharesOutstading, int companyRating, string creator)
        {
            return Conta.Create(nameAccount, companyValue, sharesOutstading, companyRating, creator);
        }
    }
}
