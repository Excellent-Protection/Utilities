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
        public string OfferName { get; set; }
        public string OfferId { get; set; }
        public OfferSector OfferSector { get; set; }
        public string PricingId { get; set; }
        public string Image { get; set; }
        public string SliderItemId { get; set; }
        public string SliderItemName { get; set; }
        public string IndividualDiscountId { get; set; }
        public string IndividualDiscountCode { get; set; }
        public DateTime OfferDateFrom { get; set; }
        public DateTime OffersDateTo { get; set; }
        public string Description { get; set; }
    }
}
