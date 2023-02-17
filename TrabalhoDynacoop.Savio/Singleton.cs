using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabalhoDynacoop.Savio
{
    internal class Singleton
    {
        public static CrmServiceClient getService()
        {
            string url = "trabalhodynamics";
            string clientId = "fe50b9a5-6c1a-4bf3-804f-89ec0b2103c0";
            string clientSecret = "FYA8Q~3QP9LQmKEvFbq8_PD2mWygsvnqdiZDGbff";

            return new CrmServiceClient($"AuthType=ClientSecret;Url=https://{url}.crm2.dynamics.com/;AppId={clientId};ClientSecret={clientSecret};");
        }
    }
}
