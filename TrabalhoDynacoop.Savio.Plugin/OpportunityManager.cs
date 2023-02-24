using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Description;
using TrabalhoDynacoop.Savio.Controller;

namespace TrabalhoDynacoop.Savio.Plugin
{
    public class OpportunityManager : IPlugin
    {
        public IOrganizationService Service { get; set; }
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            Service = serviceFactory.CreateOrganizationService(context.UserId);

            Entity opportunity = new Entity();
            bool ? incrementOrDecrement = null;

            if (context.MessageName == "Update")
            {
                Entity oppPreImage = (Entity)context.PreEntityImages["PreImage"];
                EntityReference preAccountReference = (EntityReference)oppPreImage["parentaccountid"];
                UpdateAccount(false, preAccountReference);

                opportunity = (Entity)context.InputParameters["Target"];
                incrementOrDecrement = true;
            }
            else if(context.MessageName == "Delete")
            {
                opportunity = (Entity)context.PreEntityImages["PreImage"];
                incrementOrDecrement = false;
            }

            EntityReference accountRefernce = opportunity.Contains("parentaccountid") ? (EntityReference)opportunity["parentaccountid"] : null;

            if (accountRefernce != null)
            {
                UpdateAccount(incrementOrDecrement, accountRefernce);
            }
        }

        private void UpdateAccount(bool? incrementOrDecrement, EntityReference accountRefernce)
        {
            AccountController accountController = new AccountController(this.Service);
            Entity oppAccount = accountController.GetAccountById(accountRefernce.Id, new string[] { "tdc_nmr_total_opp" });
            accountController.IncrementOrDecrementNumberOfOpp(oppAccount, incrementOrDecrement);
        }
    }
}
