using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabalhoDynacoop.Savio.Model
{
    internal class Contact
    {
        public CrmServiceClient ServiceClient { get; set; }

        public Contact(CrmServiceClient crmServiceClient)
        {
            this.ServiceClient = crmServiceClient;
        }

        public Guid CreateContact(string contactName, string contactCpf, string jobTitle, int contactAge)
        {
            VerifyContact(contactCpf);

            Entity contact = new Entity("contact");
            contact["firstname"] = contactName;
            contact["tdc_cpf"] = contactCpf;
            contact["jobtitle"] = jobTitle;
            contact["tdc_idade"] = contactAge;

            return this.ServiceClient.Create(contact);
        }

        private void VerifyContact(string contactCpf)
        {
            bool existingContact = GetContactByCpf(contactCpf);
            if (existingContact)
            {
                throw new Exception("It was not possible to create the contact, because the CNPJ or CPF already exists :(");
            }
        }

        public bool GetContactByCpf(string cpf)
        {
            QueryExpression queryExpression = new QueryExpression("contact");
            queryExpression.ColumnSet.AddColumn("firstname");
            queryExpression.Criteria.AddCondition("tdc_cpf", ConditionOperator.Equal, cpf);
            EntityCollection contact = this.ServiceClient.RetrieveMultiple(queryExpression);

            if (contact.Entities.Count > 0)
                return true;
            else
                return false;
        }
    }
}
