using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrabalhoDynacoop.Savio.Model;

namespace TrabalhoDynacoop.Savio.Controller
{
    internal class ContactController
    {
        public CrmServiceClient ServiceClient { get; set; }

        public Contact Contact { get; set; }

        public ContactController(CrmServiceClient serviceClient)
        {
            ServiceClient = serviceClient;
            this.Contact = new Contact(ServiceClient);
        }

        public Guid CreateContact(string contactName, string contactCpf, string jobTitle, int contactAge)
        {
            return Contact.CreateContact(contactName, contactCpf, jobTitle, contactAge);
        }

        public bool GetContactByCpf(string cpf)
        {
            return Contact.GetContactByCpf(cpf);
        }
    }
}
