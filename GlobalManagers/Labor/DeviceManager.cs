using Microsoft.Xrm.Sdk.Query;
using Models.CRM;
using Models.Labor;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using Utilities.DataAccess.CRM;
using Utilities.DataAccess.Labor;
using Utilities.Defaults;
using Utilities.GlobalManagers.Labor.Identity;
using Utilities.GlobalViewModels;
using Utilities.GlobalViewModels.CRM;
using Utilities.GlobalViewModels.Labor;
using Utilities.Helpers;
using Utilities.Mappers;

namespace Utilities.GlobalManagers.Labor
{
    public class DeviceManager : BaseManager, IDisposable
    {
        public DeviceManager(RequestUtility requestUtility) : base(requestUtility)
        {
        }

        public List<DeviceVm> SelectAllDevices()
        {
            using (var _unitOfWork = new UnitOfWork(new DbFactory()))
            {
                return _unitOfWork.Repository<Device>().GetAll().ToList().ToclassList<DeviceVm, Device>().ToList();
            }
        }
        public List<DeviceVm> SelectAllAnonymousDevices()
        {
            using (var _unitOfWork = new UnitOfWork(new DbFactory()))
            {
                return _unitOfWork.Repository<Device>().Find(a => a.UserId == null).ToclassList<DeviceVm, Device>().ToList();
            }
        }
        public DeviceVm GetAnonymousByDeviceId(string deviceId)
        {
            using (var _unitOfWork = new UnitOfWork(new DbFactory()))
            {
                return _unitOfWork.Repository<Device>().Single(a => a.DeviceId == deviceId && a.UserId == null).Toclass<DeviceVm, Device>();
            }
        }
        public DeviceVm GetByDeviceIdAndUserId(string deviceId, string userId)
        {
            using (var _unitOfWork = new UnitOfWork(new DbFactory()))
            {
                return _unitOfWork.Repository<Device>().Single(a => a.DeviceId == deviceId && a.UserId == userId).Toclass<DeviceVm, Device>();
            }
        }
        public DeviceVm GetByDeviceId(string deviceId)
        {
            using (var _unitOfWork = new UnitOfWork(new DbFactory()))
            {
                return _unitOfWork.Repository<Device>().Single(a => a.DeviceId == deviceId).Toclass<DeviceVm, Device>();
            }
        }

       
        public List<string> SelectAllDevicesId()
        {
            using (var _unitOfWork = new UnitOfWork(new DbFactory()))
            {
                return _unitOfWork.Repository<Device>().GetAll().Select(a => a.DeviceId.ToString()).ToList();
            }
        }
        public void AddUserDevice(string userId, string deviceId)
        {
            if (!CheckDeviceExistForUser(deviceId, userId))
            {
                AddDevice(new DeviceVm() { UserId = userId, DeviceId = deviceId, IsOnline = false });
            }

        }

        public void AddDevice(DeviceVm deviceModel)
        {
            using (var _unitOfWork = new UnitOfWork(new DbFactory()))
            {
                _unitOfWork.Repository<Device>().Add(deviceModel.ToIEntityBase<Device, DeviceVm>());
                _unitOfWork.SaveChanges();
            }
        }
        public void UpdateDevice(DeviceVm deviceModel)
        {
            using (var _unitOfWork = new UnitOfWork(new DbFactory()))
            {
                _unitOfWork.Repository<Device>().Update(deviceModel.ToIEntityBase<Device, DeviceVm>());
                _unitOfWork.SaveChanges();
            }
        }
        public bool CheckDeviceExist(string deviceId)
        {
            using (var _unitOfWork = new UnitOfWork(new DbFactory()))
            {
                var device = _unitOfWork.Repository<Device>().Single(a => a.DeviceId == deviceId);
                return device != null;
            }
        }
        public bool CheckDeviceExistForUser(string deviceId, string userId)
        {
            using (var _unitOfWork = new UnitOfWork(new DbFactory()))
            {
                var device = _unitOfWork.Repository<Device>().Single(a => a.UserId == userId && a.DeviceId == deviceId);
                return device != null;
            }
        }


        public List<DeviceVm> GetUsersDeviceId(List<string> users)
        {
            using (var _unitOfWork = new UnitOfWork(new DbFactory()))
            {

                List<DeviceVm> devicesId = _unitOfWork.Repository<Device>().Find(d => users.Contains(d.UserId)).ToclassList<DeviceVm, Device>().ToList();
                return devicesId;
            }
        }
        public PagingVm<DeviceVm> SelectDevicesByCondition(string FieldName, string FieldValue, int page, int pageSize)
        {
            try
            {
                var query = new QueryExpression(CrmEntityNamesMapping.ContactPreviousLocation);
                query.ColumnSet = new ColumnSet("new_deviceid", "new_contact");
                query.Distinct = true;
                query.Criteria.AddCondition(FieldName, ConditionOperator.Equal, FieldValue);
                query.Criteria.AddCondition("new_deviceid", ConditionOperator.NotNull);
                query.PageInfo.PageNumber = page;
                query.PageInfo.Count = pageSize;
                query.PageInfo.ReturnTotalRecordCount = true;
                var _service = CRMService.Service;
                var result = _service.RetrieveMultiple(query);
                return new PagingVm<DeviceVm>()
                {
                    TotalCount = result.TotalRecordCount,
                    Model = result.Entities.Select(a => a.ToEntity<ContactPreviousLocation>()).Select(a => new DeviceVm() { DeviceId = a.DeviceId, CrmUserId = a.Contact.Id.ToString() }).ToList()
                };
            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, ("FieldName", FieldName));
                return null;
            }
        }
        public PagingVm<DeviceVm> SelectDevicesByConditionV2(string FieldName, string FieldValue)
        {
            try
            {
                var connection = ConfigurationManager.ConnectionStrings["LaborDbContext"].ConnectionString;
                var connectionString =new System.Data.SqlClient.SqlConnectionStringBuilder(connection);
                var DBName = connectionString.InitialCatalog;
                string sql = string.Format(@";WITH cte AS
(
select Contact.ContactId,DBName.dbo.Devices.DeviceId,DBName.dbo.Devices.CreatedOn,
         ROW_NUMBER() OVER (PARTITION BY Contact.ContactId ORDER BY DBName.dbo.Devices.CreatedOn DESC) AS rn from 
Contact 
left join DBName.dbo.AspNetUsers on DBName.dbo.AspNetUsers.CrmUserId = Contact.ContactId
left join DBName.dbo.Devices on DBName.dbo.Devices.UserId = DBName.dbo.AspNetUsers.Id
where DBName.dbo.Devices.UserId is not null
{0}
)
SELECT *
FROM cte
WHERE rn = 1", ((!string.IsNullOrEmpty(FieldName) && !string.IsNullOrEmpty(FieldValue)) ? string.Format("and {0} = '{1}'", FieldName, FieldValue) : ""));
                sql=sql.Replace("DBName", DBName);
                DataTable dt = CRMAccessDB.SelectQ(sql).Tables[0];

                List<DeviceVm> devices = new List<DeviceVm>();
                foreach (DataRow item in dt.Rows)
                {
                    devices.Add(new DeviceVm() { DeviceId = item["DeviceId"].ToString() });
                }
                return new PagingVm<DeviceVm>()
                {
                    TotalCount = devices.Count,
                    Model = devices
                };
            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, ("FieldName", FieldName));
                return null;
            }
        }
        public PagingVm<DeviceVm> SelectDevicesWithCompleteProfile()
        {
            try
            {
                var connection = ConfigurationManager.ConnectionStrings["LaborDbContext"].ConnectionString;
                var connectionString = new System.Data.SqlClient.SqlConnectionStringBuilder(connection);
                var DBName = connectionString.InitialCatalog;
                string sql = @";WITH cte AS
(
select Contact.ContactId,DBName.dbo.Devices.DeviceId,DBName.dbo.Devices.CreatedOn,
         ROW_NUMBER() OVER (PARTITION BY Contact.ContactId ORDER BY DBName.dbo.Devices.CreatedOn DESC) AS rn from 
Contact 
left join DBName.dbo.AspNetUsers on DBName.dbo.AspNetUsers.CrmUserId = Contact.ContactId
left join DBName.dbo.Devices on DBName.dbo.Devices.UserId = DBName.dbo.AspNetUsers.Id
where DBName.dbo.Devices.UserId is not null
and Contact.new_contactcity is not null and  Contact.new_contdistid is not null 
)
SELECT *
FROM cte
WHERE rn = 1";
                sql = sql.Replace("DBName", DBName);
                DataTable dt = CRMAccessDB.SelectQ(sql).Tables[0];

                List<DeviceVm> devices = new List<DeviceVm>();
                foreach (DataRow item in dt.Rows)
                {
                    devices.Add(new DeviceVm() { DeviceId = item["DeviceId"].ToString() });
                }
                return new PagingVm<DeviceVm>()
                {
                    TotalCount = devices.Count,
                    Model = devices
                };
            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return null;
            }
        }
        public PagingVm<DeviceVm> SelectDevicesWithoutCompleteProfile()
        {
            try
            {
                var connection = ConfigurationManager.ConnectionStrings["LaborDbContext"].ConnectionString;
                var connectionString = new System.Data.SqlClient.SqlConnectionStringBuilder(connection);
                var DBName = connectionString.InitialCatalog;
                string sql = @";WITH cte AS
(
select Contact.ContactId,DBName.dbo.Devices.DeviceId,DBName.dbo.Devices.CreatedOn,
         ROW_NUMBER() OVER (PARTITION BY Contact.ContactId ORDER BY DBName.dbo.Devices.CreatedOn DESC) AS rn from 
Contact 
left join DBName.dbo.AspNetUsers on DBName.dbo.AspNetUsers.CrmUserId = Contact.ContactId
left join DBName.dbo.Devices on DBName.dbo.Devices.UserId = DBName.dbo.AspNetUsers.Id
where DBName.dbo.Devices.UserId is not null
and Contact.new_contactcity is null and  Contact.new_contdistid is null 
)
SELECT *
FROM cte
WHERE rn = 1";
                sql = sql.Replace("DBName", DBName);
                DataTable dt = CRMAccessDB.SelectQ(sql).Tables[0];

                List<DeviceVm> devices = new List<DeviceVm>();
                foreach (DataRow item in dt.Rows)
                {
                    devices.Add(new DeviceVm() { DeviceId = item["DeviceId"].ToString() });
                }
                return new PagingVm<DeviceVm>()
                {
                    TotalCount = devices.Count,
                    Model = devices
                };
            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return null;
            }
        }
        
        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
