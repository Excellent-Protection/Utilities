using Models.Labor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                    var OnlinePortalUrl = new ExcSettingsManager(new RequestUtility())["OnlinePortalUrl"].ToString();
                    string token = GetAlphanumericID(7);
                    string shortUrl = OnlinePortalUrl+"/"+RequestUtility.RouteLanguage + "/api/Index/" + token;
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
    }
}
