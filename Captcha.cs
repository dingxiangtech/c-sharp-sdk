using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web.Script.Serialization;

namespace DxCsharpSDK
{
    public class Captcha
    {
        private String captchaUrl = "https://cap.dingxiang-inc.com/api/tokenVerify";
        private int timeOut = 2000;
        private String appId;
        private String appSecret;

        public Captcha(String appId, String appSecret)
        {
            this.appId = appId;
            this.appSecret = appSecret;
        }
        public Captcha(String appId, String appSecret, int timeOut)
        {
            this.appId = appId;
            this.appSecret = appSecret;
            this.timeOut = timeOut;
        }
        public void SetCaptchaUrl(String captchaUrl)
        {
            this.captchaUrl = captchaUrl;
        }
        public void SetTimeOut(int time)
        {
            this.timeOut = time;
        }
        public CaptchaResponse VerifyToken(String token)
        {
            if (String.IsNullOrEmpty(token) || String.IsNullOrEmpty(this.appSecret) || String.IsNullOrEmpty(this.appId))
            {
                return new CaptchaResponse(false, "WRONG_PARAMETER");
            }
            if (token.Length > 1024) {
                return new CaptchaResponse(false, "WRONG_PARAMETER_LENGTH");
            }
            String[] args = token.Split(':');
            String const_id = null;
            if (args.Length == 2)
            {
                const_id = args[1];
            }
            String sign = this.GetVerifySign(appSecret, args[0]);
            String reqUrl = String.Format("{0}?token={1}&constId={2}&appKey={3}&sign={4}", this.captchaUrl, args[0], const_id, appId, sign);
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(reqUrl);
                request.Method = "GET";
                request.Timeout = this.timeOut;
                request.ContentType = "text/html;charset=UTF-8";
                //接受返回来的数据
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();
                StreamReader streamReader = new StreamReader(stream, Encoding.GetEncoding("utf-8"));
                string retString = streamReader.ReadToEnd();
                streamReader.Close();
                stream.Close();
                response.Close();
                JavaScriptSerializer js = new JavaScriptSerializer();
                Result result = js.Deserialize<Result>(retString);
                CaptchaResponse captchaResponse = new CaptchaResponse();
                captchaResponse.result = result.success;
                captchaResponse.captchaStatus = "SERVER_SUCCESS";
                return captchaResponse;
            }
            catch (Exception e)
            {
                return new CaptchaResponse(true, e.Message);
            }

        }

        private String GetVerifySign(String appSecret, String token)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(appSecret).Append(token).Append(appSecret);
            MD5 md5Hash = MD5.Create();
            return this.GetMd5Hash(md5Hash, sb.ToString());
        }

        private String GetMd5Hash(MD5 md5Hash, string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

    }

    public class Result
    {
        public Boolean success { get; set; }
        public String msg { get; set; }
    }

    public class CaptchaResponse
    {

        public CaptchaResponse(Boolean result, String captchaStatus)
        {
            this.result = result;
            this.captchaStatus = captchaStatus;
        }
        public CaptchaResponse()
        {
        }
        public Boolean result { get; set; }
        public String captchaStatus { get; set; }
    }
}
