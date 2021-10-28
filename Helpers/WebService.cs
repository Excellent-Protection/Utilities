using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Utilities.Helpers
{
    public class WebService
    {
        public string Call(string URL,string MethodName,Dictionary<string,string>Params=null)
        {
            var url = URL; //'Web service URL'
            var action = "http://tempuri.org/" + MethodName; //the SOAP method/action name  
            var soapEnvelopeXml = CreateSoapEnvelope(MethodName,Params);
            var soapRequest = CreateSoapRequest(url, action);
            InsertSoapEnvelopeIntoSoapRequest(soapEnvelopeXml, soapRequest);

            using (var stringWriter = new StringWriter())
            {
                using (var xmlWriter = XmlWriter.Create(stringWriter))
                {
                    soapEnvelopeXml.WriteTo(xmlWriter);
                    xmlWriter.Flush();
                }
            }

            // begin async call to web request.
            var asyncResult = soapRequest.BeginGetResponse(null, null);

            // suspend this thread until call is complete. You might want to
            // do something usefull here like update your UI.
            var success = asyncResult.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(5));

            if (!success) return null;

            // get the response from the completed web request.
            using (var webResponse = soapRequest.EndGetResponse(asyncResult))
            {
                string soapResult;
                var responseStream = webResponse.GetResponseStream();
                if (responseStream == null)
                {
                    return null;
                }
                using (var reader = new StreamReader(responseStream))
                {
                    soapResult = reader.ReadToEnd();
                }
                return soapResult;
            }

        }
        private static HttpWebRequest CreateSoapRequest(string url, string action)
        {
            var webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Headers.Add("SOAPAction", action);
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            return webRequest;
        }

        private static XmlDocument CreateSoapEnvelope(string MethodName,Dictionary<string,string> Params)
        {
            string postValues = "";
            if (Params != null)
            {
                foreach (var param in Params)
                {
                    postValues += string.Format("<{0}>{1}</{0}>", param.Key, param.Value);
                }
            }
            string SOAPEnvelope =
            @"<?xml version=""1.0"" encoding=""utf-8""?>
                <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""
                   xmlns:xsd=""http://www.w3.org/2001/XMLSchema""
                   xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                  <soap:Body>
                    <{0} xmlns=""http://tempuri.org/"">
                      {1}
                    </{0}>
                  </soap:Body>
                </soap:Envelope>";
            SOAPEnvelope = string.Format(SOAPEnvelope, MethodName, postValues);

            var soapEnvelope = new XmlDocument();
            soapEnvelope.LoadXml(SOAPEnvelope); //the SOAP envelope to send
            return soapEnvelope;
        }

        private static void InsertSoapEnvelopeIntoSoapRequest(XmlDocument soapEnvelopeXml, HttpWebRequest webRequest)
        {
            using (Stream stream = webRequest.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }
        }
    }
}
