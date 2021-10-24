using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.GlobalViewModels.Custom
{
   public class ContactMainSubPreviouseLocationsVm
    {

        public SavedLocationVm MainLocations { get; set; }
        public List<SavedLocationVm> SubLocation { get; set; }
    }
}
