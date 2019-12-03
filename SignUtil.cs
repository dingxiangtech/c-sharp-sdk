using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace DxCsharpSDK
{
    public class SignUtil
    {
        private static  String EVENT_CODE = "eventCode";
        private static  String FLAG = "flag"; 

        public static String SortedParams(CtuRequest request)
        {
            String eventCode = request.eventCode;
            String flag = request.flag;
            Dictionary<string, string> data = request.data;

            StringBuilder sb = new StringBuilder();
            sb.Append(EVENT_CODE).Append(eventCode).Append(FLAG).Append(flag);
           // var dicSort = from objDic in data orderby objDic.Value ascending select objDic;
            Dictionary<string, string> dicSort = data.OrderBy(p => p.Key).ToDictionary(p => p.Key, o => o.Value);
            foreach (KeyValuePair<string, string> kvp in dicSort) {
                if (kvp.Value == null)
                {
                    sb.Append(kvp.Key).Append("null");
                }
                else {
                    sb.Append(kvp.Key).Append(kvp.Value);
                }
                
            }   
            
            return sb.ToString();
        }

        public static String Sign(String appSecret, CtuRequest ctuRequest)
        {
            String sortedParams = SortedParams(ctuRequest);

          //  MD5 md5Hash = MD5.Create();
           
            return GetMd5Hex(new StringBuilder(appSecret).Append(sortedParams)
                    .Append(appSecret).ToString());
         
        }

        private static String GetMd5Hash(MD5 md5Hash, string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        private static String GetMd5Hex(string data)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] dataHash = md5.ComputeHash(Encoding.UTF8.GetBytes(data));
            StringBuilder sb = new StringBuilder();
            foreach (byte b in dataHash)
            {
                sb.Append(b.ToString("x2").ToLower());
            }
           return sb.ToString();
        }

    }
}
