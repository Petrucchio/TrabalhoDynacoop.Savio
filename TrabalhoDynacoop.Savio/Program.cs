using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabalhoDynacoop.Savio
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CrmServiceClient serviceClient = Singleton.getService();
        }
    }
}
