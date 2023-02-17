using Microsoft.Xrm.Sdk;
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

        public Guid CreateContact(string contactName, string jobTitle, int contactAge)
        {
            Entity contact = new Entity("contact");
            contact["firstname"] = contactName;
            contact["jobtitle"] = jobTitle;
            contact["tdc_idade"] = contactAge;

            return this.ServiceClient.Create(contact);
        }
    }
}
