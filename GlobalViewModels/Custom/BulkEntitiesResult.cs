using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HourlySectorLib.ViewModels.Custom
{
    public class BulkEntitiesResult
    {
        public List<string> UpdatedEntityId { get; set; }
        public List<string> NotUpdatedEntityId { get; set; }
    }
}
