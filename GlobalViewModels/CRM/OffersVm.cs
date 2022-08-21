using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Enums;

namespace Utilities.GlobalViewModels.CRM
{
    public class OffersVm
    {
        public string OfferId { get; set; }
        public OfferSector OfferSector { get; set; }
        public string PricingId { get; set; }
        public string Image { get; set; }
    }
}
