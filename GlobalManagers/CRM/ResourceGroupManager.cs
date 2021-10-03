using Models.CRM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Defaults;
using Utilities.GlobalRepositories.CRM;
using Utilities.GlobalViewModels;
using Utilities.GlobalViewModels.Custome;
using Utilities.Helpers;
using Utilities.Mappers;

namespace Utilities.GlobalManagers.CRM
{
  public  class ResourceGroupManager  :BaseManager , IDisposable
    {

        ExcSettingsManager _excSettingMngr;
        ResourceGroupRepository _repo;
        public ResourceGroupManager(RequestUtility requestUtility):base(requestUtility)
        {
            _excSettingMngr = new ExcSettingsManager(RequestUtility);
            _repo = new ResourceGroupRepository(RequestUtility);
        }

        public void Dispose()
        {
        }

        public ResponseVm<List<BaseQuickLookupWithImageVm>> GetResourceGroup(string professiongroupId)
        {
            try
            {
                var getProfessionsFromPackages = _excSettingMngr[DefaultValues.SelectNationalitiesFromPackagesName];
                var selectProfFromPackages = DefaultValues.SelectNationalitiesFromPackages;
                if (getProfessionsFromPackages != null)
                {
                    selectProfFromPackages = bool.Parse(selectProfFromPackages.ToString());
                }
                if (selectProfFromPackages)
                {
                    return new ResponseVm<List<BaseQuickLookupWithImageVm>> { Status = HttpStatusCodeEnum.Ok, Data = _repo.GetResourceGroupsFromIndividualPackages(professiongroupId).ToModelListData<BaseQuickLookupWithImageVm, ResourceGroup>().ToList() };
                }
                return new ResponseVm<List<BaseQuickLookupWithImageVm>> { Status = HttpStatusCodeEnum.Ok, Data = _repo.GetResourceGroups(professiongroupId).ToModelListData<BaseQuickLookupWithImageVm, ResourceGroup>().ToList() };

            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return new ResponseVm<List<BaseQuickLookupWithImageVm>>
                {
                    Status = HttpStatusCodeEnum.IneternalServerError,
                    Message = "An Error Occurred"
                };
            }
        }

    }
}
