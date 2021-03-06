﻿using Merp.Registry.QueryStack;
using Merp.Registry.QueryStack.Model;
using Merp.Web.Site.Areas.Registry.Models;
using Merp.Web.Site.Areas.Registry.Models.Party;
using Rebus.Bus;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Merp.Web.Site.Areas.Registry.WorkerServices
{
    public class PartyControllerWorkerServices
    {
        public IBus Bus { get; private set; }
        public IDatabase Database { get; set; }

        public PartyControllerWorkerServices(IBus bus, IDatabase database)
        {
            this.Bus = bus ?? throw new ArgumentNullException(nameof(bus));
            this.Database = database ?? throw new ArgumentNullException(nameof(database));
        }

        public string GetDetailViewModel(Guid partyId)
        {
            if(Database.Parties.OfType<Company>().Where(p => p.OriginalId == partyId).Count()==1)
            {
                return "Company";
            }
            else if (Database.Parties.OfType<Person>().Where(p => p.OriginalId == partyId).Count() == 1)
            {
                return "Person";
            }
            else
            {
                return "Unknown";
            }
        }


        public IEnumerable<GetPartiesViewModel> GetParties(string query, string partyType, string city, string orderBy, string orderDirection)
        {
            var parties = Database.Parties;
            parties = ApplyPartyTypeFilter(parties, partyType);
            parties = ApplyCityFilter(parties, city);
            parties = ApplyOrdering(parties, orderBy, orderDirection);

            var partyViewModels = parties.Select(p => new GetPartiesViewModel { id = p.Id, uid = p.OriginalId, name = p.DisplayName });
            partyViewModels = ApplyNameFilter(partyViewModels, query);
            partyViewModels = partyViewModels.Take(20);
            return partyViewModels.ToList();
        }

        #region Helper Methods

        private static IQueryable<Party> ApplyPartyTypeFilter(IQueryable<Party> parties, string partyType)
        {
            if("person".Equals(partyType, StringComparison.OrdinalIgnoreCase))
            {
                return parties.OfType<Person>();
            }
            if ("company".Equals(partyType, StringComparison.OrdinalIgnoreCase))
            {
                return parties.OfType<Company>();
            }
            return parties;
        }

        private static IQueryable<Party> ApplyOrdering(IQueryable<Party> parties, string orderBy, string orderDirection)
        {
            if ("name".Equals(orderBy, StringComparison.OrdinalIgnoreCase))
            {
                parties = "desc".Equals(orderDirection, StringComparison.OrdinalIgnoreCase)
                    ? parties.OrderByDescending(p => p.DisplayName)
                    : parties.OrderBy(p => p.DisplayName);
            }
            else
            {
                parties = parties.OrderBy(p => p.DisplayName);
            }

            return parties;
        }

        private static IQueryable<Party> ApplyCityFilter(IQueryable<Party> parties, string city)
        {
            if (!string.IsNullOrEmpty(city) && city != "undefined")
            {
                parties = parties.Where(p => p.LegalAddress.City.Contains(city));
            }

            return parties;
        }

        private static IQueryable<GetPartiesViewModel> ApplyNameFilter(IQueryable<GetPartiesViewModel> partyViewModels, string query)
        {
            if (!string.IsNullOrEmpty(query) && query != "undefined")
            {
                partyViewModels = partyViewModels.Where(p => p.name.Contains(query));
            }

            return partyViewModels;
        }

        #endregion
    }
}