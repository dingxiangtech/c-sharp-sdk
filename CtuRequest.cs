using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DxCsharpSDK
{
    public class CtuRequest
    {
        public String eventCode { get; set; }          // 事件code
        public String flag { get; set; }               // 客户端请求标记,用来标识该次请求
        public Dictionary<string, string> data { get; set; }

        public override string ToString()
        {
            return "CtuRequest{" +
               "eventCode='" + eventCode + '\'' +
               ", data=" + data +
               ", flag='" + flag + '\'' +
               '}';
        }
    }
}
