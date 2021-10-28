using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.GlobalManagers.CRM
{
   public class ContactVm
    {
        public Guid? Id { get; set; }
        public string FName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }
        public string MobilePhone { get; set; }
        public string OtherMobilePhone { get; set; }
        public string JobTitle { get; set; }
        public string CityId { get; set; }
        public string TempNationalityId { get; set; }
        public string NationalityId { get; set; }
        public string NationalityName { get; set; }
        public string IdentificationNo { get; set; }
        public string RegionId { get; set; }
        public int? GenderId { get; set; }
        public string GenderName { get; set; }
        public int? WorkSector { get; set; }
        public string WorkSectorName { get; set; }
        public int? PlatformSource { get; set; }
        public bool? IsIdNoExist { get; set; }
        public bool? BlackList { get; set; }
        public string BlackListReason { get; set; }
        public int? BlackListStatus { get; set; }
        public bool? HadRenewDiscount { get; set; }
        public string CityName { get; set; }
        public System.Nullable<decimal> longitude { get; set; }
        public System.Nullable<decimal> Latitude { get; set; }
        public string ContactAddress { get; set; }
        public int? CompanyKnownBy { get; set; }
        public string WorkAddress { get; set; }
        public string WorkPlace { get; set; }
    
        public decimal? WalletAmount { get; set; }
        public bool ShowLoyality { get; set; }
        public bool ShowWallet { get; set; }
        public decimal ValidPoints { get; set; }
    
    }
}
