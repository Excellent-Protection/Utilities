using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Labor
{
    public interface IEntityBase
    {
        Guid Id { get; set; }
        string CreatedBy { get; set; }
        DateTime? CreatedOn { get; set; }
        string ModifiedBy { get; set; }
        DateTime? ModifiedOn { get; set; }
        DateTime? DeletedOn { get; set; }
        string DeletedBy { get; set; }
        bool IsDeleted { get; set; }
        bool IsDeactivated { get; set; }
        string OwnerId { get; set; }
        ApplicationUser Owner { get; set; }

        string Name { get; set; }

    }
}
