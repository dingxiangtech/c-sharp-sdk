using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;

namespace DxCsharpSDK
{
    public class CtuClient
    {
        public String url;           // 风险防控服务URL
        public String appKey;        // 颁发的公钥,可公开
        public String appSecret;     // 颁发的秘钥,严禁公开,请保管好,千万不要泄露!
        public int connectTimeout = 2000;
        public static  String UTF8_ENCODE = "UTF-8";
        public static  int version = 1; //client版本号  从1开始

        public CtuClient(String url, String appKey, String appSecret)
        {
            this.url = url;
            this.appKey = appKey;
            this.appSecret = appSecret;
        }

        public CtuClient(String url, String appKey, String appSecret, int connectTimeout)
        {
            this.url = url;
            this.appKey = appKey;
            this.appSecret = appSecret;
            this.connectTimeout = connectTimeout;
        }

        public CtuResponse CheckRisk(CtuRequest request) {
            
                String sign = SignUtil.Sign(appSecret, request);
                String reqUrl = String.Format("{0}?appKey={1}&sign={2}&version={3}", url, appKey, sign, version);
                JavaScriptSerializer js = new JavaScriptSerializer();
                String reqJsonString = js.Serialize(request);
                byte[] bytes = Encoding.UTF8.GetBytes(reqJsonString);
                string postDataStr = Convert.ToBase64String(bytes);
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(reqUrl);           
                httpWebRequest.Method = "POST";
                httpWebRequest.ContentType = "text/html";
                httpWebRequest.Timeout = this.connectTimeout;
               try
               {
                StreamWriter writer = new StreamWriter(httpWebRequest.GetRequestStream(), Encoding.UTF8);                
                writer.Write(postDataStr);
                writer.Flush();
                writer.Close();                  

                HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse();
                Stream stream = response.GetResponseStream();
                StreamReader streamReader = new StreamReader(stream, Encoding.GetEncoding("utf-8"));
                string retString = streamReader.ReadToEnd();
                streamReader.Close();
                stream.Close();
                response.Close();
                CtuResponse ctuResponse = js.Deserialize<CtuResponse>(retString);
                return ctuResponse;
            }
            catch (Exception e) {
                httpWebRequest.Abort();
                CtuResponse response = new CtuResponse();
                CtuResult result = new CtuResult();
                result.RiskLevel = "ACCEPT";
                Dictionary<string, Object> ExtraInfo = new Dictionary<string, object>();
                ExtraInfo.Add("error", e.Message);
                result.ExtraInfo = ExtraInfo;                    
                response.result = result;
                response.status = CtuResponseStatus.SERVICE_INTERNAL_ERROR.ToString();               
                return response;
            }
        }

    }
}
