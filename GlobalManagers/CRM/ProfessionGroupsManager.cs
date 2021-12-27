using Models.CRM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Defaults;
using Utilities.Enums;
using Utilities.GlobalRepositories.CRM;
using Utilities.GlobalViewModels;
using Utilities.GlobalViewModels.Custom;
using Utilities.Helpers;
using Utilities.Mappers;

namespace Utilities.GlobalManagers.CRM
{
  public  class ProfessionGroupsManager :BaseManager , IDisposable
    {
        ProfessionGroupsRepository _rep;

        ExcSettingsManager _excSettingMngr;
        public ProfessionGroupsManager(RequestUtility requestUtility) : base(requestUtility)
        {
            _rep = new ProfessionGroupsRepository(RequestUtility);
            _excSettingMngr = new ExcSettingsManager(RequestUtility);
        }

        public void Dispose()
        {
            
        }

        public ResponseVm < List<BaseQuickLookupWithImageVm>> GetProfessionGroups(ServiceType? serviceType = null)
        {
            try
            {
                var getProfessionsFromPackages =_excSettingMngr[DefaultValues.SelectProfessionsFromPackagesName];
                var selectProfFromPackages = DefaultValues.SelectProfessionsFromPackages;
                if (getProfessionsFromPackages!=null)
                {
                    selectProfFromPackages = bool.Parse( selectProfFromPackages.ToString());
                }
                if(selectProfFromPackages)
                {
                    return  new ResponseVm<List<BaseQuickLookupWithImageVm>> { Status = HttpStatusCodeEnum.Ok, Data = _rep.GetProfessionGroupsFromPackages(serviceType).ToModelListData<BaseQuickLookupWithImageVm, ProfessionGroups>().ToList() };
                }
                return new ResponseVm<List<BaseQuickLookupWithImageVm>> { Status = HttpStatusCodeEnum.Ok, Data = _rep.GetProfessionGroups(serviceType).ToModelListData<BaseQuickLookupWithImageVm, ProfessionGroups>().ToList() };
          
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

        public int GetProfessionGender(string professionGroupId)
        {
            try
            {
                var gender= _rep.GetProfessionGender(professionGroupId).Gender?.Value;
                return gender != null ? gender.Value : 0;
            }
            catch(Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return 0;
            }

        }


        public List<string> GetProfessionsId(string ProfGroupId)
        {
            try
            {
                return _rep.GetProfessionsId(ProfGroupId);
            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, ("ProfGroupId", ProfGroupId));
                return null;
            }
        }


        public  string GetRequiredAttchmentsByProfessionGroup(string profGroupId)
        {
            try
            {
                return  _rep.GetRequiredAttchmentsByProfessionGroup(profGroupId) ;
                
            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, ("ProfGroupId", profGroupId));
                return null;
            }
        }
    }
}
