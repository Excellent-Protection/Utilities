using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Enums;
using Utilities.GlobalViewModels;
using Utilities.GlobalViewModels.CRM;

namespace HourlySectorLib.ViewModels
{
   public class ServiceVm
    {
        public string Terms { get; set; }
        public string Description { get; set; }
        public string IconId { get; set; }
        public string IconName { get; set; }
        public int? MaxLaborNumber { get; set; }
        public int? MinLaborNumber { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string ProjectId { get; set; }
        public string ResourceGroupId { get; set; }
        public string ServiceId { get; set; }
        public int? MorningStartTime { get; set; }
        public int? EveningStartTime { get; set; }
        public int? FullDayStartTime { get; set; }
        public int? Gender { get; set; }
        public DateTime? MinStartDay { get; set; }
        public List<string> CalendarDays { get; set; }
        public List<int> ServiceHours { get; set; }
        public List<VisitShift> ServiceShifts { get; set; }
        public List<BaseQuickLookupVm> ResourceGroups { get; set; }
        public List<ExcSettingsVm> ExcSettings { get; set; }
        public List<BaseOptionSetVM> ServiceShiftsLookup { get; set; }
        public List<BaseOptionSetVM> WorkerListLookup { get; set; }
        public List<BaseOptionSetVM> ServiceHoursLookup { get; set; }
        public string ServiceNote { get; set; }
    }
}
