using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DxCsharpSDK
{
    public class CtuResponse
    {
        public String uuid { get; set; }                 // 服务端返回的请求标识码，供服务端排查问题
        public String status { get; set; }    // 状态码
        public CtuResult result { get; set; }           // 防控结果

        public CtuResponse()
        {
        }

        public CtuResponse(String uuid)
        {
            this.uuid = uuid;
        }

        public CtuResponse(String uuid, CtuResult ctuResult)
        {
            this.uuid = uuid;
            this.result = ctuResult;
            this.status = CtuResponseStatus.SUCCESS.ToString();           
        }

        public CtuResponse(String uuid, CtuResult ctuResult, CtuResponseStatus status)
        {
            this.uuid = uuid;
            this.result = ctuResult;
            this.status = status.ToString();
        }
    }
}
