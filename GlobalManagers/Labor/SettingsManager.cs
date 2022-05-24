
using System;
using System.Collections.Generic;
using System.Linq;
using Models.CRM;
using Utilities.DataAccess.Labor;
using Models.Labor;
using Utilities.Mappers;
using Utilities.Helpers;
using Utilities.GlobalViewModels.Labor;
using Utilities.Enums;

namespace Utilities.GlobalManagers.Labor
{
    public class SettingsManager:IDisposable,IComparable
    {

       
        static SettingsManager()
        {
        }
        public SettingVM this[string key]
        {
            get
            {
                using (UnitOfWork unitOfWork = new UnitOfWork(new DbFactory()))
                {
                    var setting = unitOfWork.Repository<Setting>().Single(s => s.Name.ToLower() == key);
                     return setting.Toclass<SettingVM,Setting>();
                }
            }
        }
      

        public List<SettingVM> GetAll()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new DbFactory()))
            {
                var setting = unitOfWork.Repository<Setting>().GetAll().ToList();
                return setting.ToclassList<SettingVM, Setting>().ToList();
            }
        }

        public SettingVM AddSetting(SettingVM entity)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new DbFactory()))
            {
                var setting = unitOfWork.Repository<Setting>().Add(entity.ToIEntityBase<Setting, SettingVM>());
                unitOfWork.SaveChanges();
                return setting.Toclass<SettingVM, Setting>();
            }
        }
        public  bool checkVersion(MobilePhoneSource mobilePhoneSource,string routingVersion)
        {
          
            if(mobilePhoneSource== MobilePhoneSource.Android)
            {
                using (UnitOfWork unitOfWork = new UnitOfWork(new DbFactory()))
                {
                    var setting = unitOfWork.Repository<Setting>().Find(s=>s.Name.ToLower()=="androidversion").FirstOrDefault().Value;
                    return  routingVersion.CompareTo(setting)>0 ? true : false;
                } 

            } 
              else
            {
               
                using (UnitOfWork unitOfWork = new UnitOfWork(new DbFactory()))
                {
                    var setting = unitOfWork.Repository<Setting>().Find(s => s.Name.ToLower() == "iosversion").FirstOrDefault().Value;
                    return routingVersion.CompareTo(setting) > 0 ? true : false;
                }
            }

        }
        public SettingVM GetById(string id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new DbFactory()))
            {
                var setting = unitOfWork.Repository<Setting>().Single(s => s.Id == new Guid(id));
                return setting.Toclass<SettingVM, Setting>();
            }
        }

        public void UpdateSetting(SettingVM entity)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new DbFactory()))
            {
                unitOfWork.Repository<Setting>().Update(entity.ToIEntityBase<Setting, SettingVM>());
                unitOfWork.SaveChanges();

            }
        }

        public void Dispose()
        {

        }

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }

        public List<SettingVM> this[List<string> keys]
        {
            get
            {
                using (UnitOfWork unitOfWork = new UnitOfWork(new DbFactory()))
                {
                    try
                    {
                        var setting = unitOfWork.Repository<Setting>().Find(s => keys.Contains(s.Name)).ToList();
                        return setting.ToclassList<SettingVM, Setting>().ToList();
                    }
                    catch (Exception ex)
                    {
                        LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                    }
                    return null;
                }

            }
        }
     
        //public ModelResult<ModelData> GetSettingByName(string Name)
        //{
        //    var result = this[Name];
        //    if (result != null)
        //    {
        //        var Parse = bool.TryParse(result.Value.ToLower(), out bool settingboolean);
        //        if (Parse)
        //        {
        //            return new ModelResult<ModelData>()
        //            {
        //                State = true,
        //                Data = new ModelDataBool { Value = settingboolean }
        //            };
        //        }
        //        Parse = int.TryParse(result.Value, out int settinginteger);
        //        if (Parse)
        //        {
        //            return new ModelResult<ModelData>()
        //            {
        //                State = true,
        //                Data = new ModelDataInt { Value = settinginteger }
        //            };
        //        }
        //        return new ModelResult<ModelData>()
        //        {
        //            State = true,
        //            Data = new ModelDataString { Value = result.Value }
        //        };
        //    }
        //    else
        //    {
        //        return new ModelResult<ModelData>() { State = false };
        //    }
        //}


    }
}
