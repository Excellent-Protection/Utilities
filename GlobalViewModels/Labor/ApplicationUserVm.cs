using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Enums;

namespace AuthonticationLib.ViewModels
{
  public  class ApplicationUserVm
    {
        public string UserName { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public UserAccountType AccountType { get; set; }
        public string CrmUserId { get; set; }
        public string Id { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Image { get; set; }

        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsDeactivated { get; set; }
        public string Name { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string DeletedBy { get; set; }
        public string OwnerId { get; set; }
        public ApplicationUserVm Owner { get; set; }
        public string SecurityStamp { get; set; }
    }
}

