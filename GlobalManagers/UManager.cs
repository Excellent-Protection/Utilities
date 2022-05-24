
using Microsoft.Xrm.Sdk;
using Models.Labor;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.DataAccess.CRM;
using Utilities.DataAccess.Labor;
using Utilities.GlobalManagers.CRM;
using Utilities.Helpers;

namespace Utilities.GlobalManagers
{
   public class UManager : BaseManager, IDisposable 
    {
        public UManager(RequestUtility requestUtility):base(requestUtility)
        {

        }
        public UrlShortener GetLongUrl(string token)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new DbFactory()))
            {
                var result = unitOfWork.Repository<UrlShortener>().Single(s => s.Name == token);
                return result;
            }
        }

        public int Update(UrlShortener entity)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new DbFactory()))
            {
                unitOfWork.Repository<UrlShortener>().Update(entity);
                int result = unitOfWork.SaveChanges();
                return result;
            }
        }

        public string GenerateShotUrl(string longUrl, int? noOfTrys = 0)
        {
            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork(new DbFactory()))
                {
                    var OnlineAPIUrl = new ExcSettingsManager(RequestUtility)["OnlineAPIUrl"].ToString();
                    string token = GetAlphanumericID(7);
                    string shortUrl = OnlineAPIUrl + "/" + RequestUtility.RouteLanguage + "/u/" + token;
                    var result = unitOfWork.Repository<UrlShortener>().Add(new UrlShortener()
                    {
                        ShortUrl = shortUrl.ToLower(),
                        LongUrl = longUrl,
                        Name = token.ToLower()
                    });
                    unitOfWork.SaveChanges();
                    return result.ShortUrl;
                }
            }
            catch(Exception ex)
            {
                ++noOfTrys;
                if (noOfTrys != 5)
                    GenerateShotUrl(longUrl, noOfTrys);
            }
            return longUrl;
        }


        public string GenerateShotCrmUrl(string longUrl, int? noOfTrys = 0)
        {
            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork(new DbFactory()))
                {
                    var OnlineAPIUrl = ConfigurationManager.AppSettings["PortalCRMOnline"];
                    string token = GetAlphanumericID(7);
                    string shortUrl = OnlineAPIUrl  + "/u/" + token;
                    var result = unitOfWork.Repository<UrlShortener>().Add(new UrlShortener()
                    {
                        ShortUrl = shortUrl.ToLower(),
                        LongUrl = longUrl,
                        Name = token.ToLower()
                    });
                    unitOfWork.SaveChanges();
                    return result.ShortUrl;
                }
            }
            catch (Exception ex)
            {
                ++noOfTrys;
                if (noOfTrys != 5)
                    GenerateShotCrmUrl(longUrl, noOfTrys);
            }
            return longUrl;
        }
        public static string GetAlphanumericID(int length)
        {
            var crypto = new System.Security.Cryptography.RNGCryptoServiceProvider();
            var bytes = new byte[length];
            crypto.GetBytes(bytes); // get an array of random bytes.      
            return BitConverter.ToString(bytes).Replace("-", string.Empty); // convert array to hex values.
        }

        public void Dispose()
        {
        }


        public Tuple<string, string> SetProjectTimeSheetURLS(string ProjectTimeSheetID, string UID)
        {
            var ISVLink = new ExcSettingsManager(RequestUtility)["ISVURL"];
       
            string ProjectTimeSheetURL = String.Format("{0}{1}{2}{3}{4}", ISVLink, "Emp/TimeSheet/BSCustTS.aspx?id=", ProjectTimeSheetID,
                "&UID=", UID);
           
            var KAMShortUrl = GenerateShotUrl(ProjectTimeSheetURL + "&type=kam");
            var CustomerShortUrl = GenerateShotUrl(ProjectTimeSheetURL + "&type=customer");
            try
            {
                Entity ProjectTimeSheet = new Entity("new_projecttimesheet");
                ProjectTimeSheet.Id = new Guid(ProjectTimeSheetID);
                ProjectTimeSheet["new_kamurl"] = KAMShortUrl;
                ProjectTimeSheet["new_customerurl"] = CustomerShortUrl;
                CRMService.Service.Update(ProjectTimeSheet);
            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, ("ProjectTimeSheetID", ProjectTimeSheetID));
            }
            return Tuple.Create(KAMShortUrl, CustomerShortUrl);
        }
    }
}
