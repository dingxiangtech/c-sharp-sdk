using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DxCsharpSDK
{
    public enum CtuResponseStatus
    {
        SUCCESS,
        INVALID_REQUEST_PARAMS,
        INVALID_REQUEST_BODY,
        INVALID_REQUEST_NO_EVENT_DATA,
        INVALID_REQUEST_SIGN,
        INVALID_APP_KEY ,
        INVALID_EVENT_CODE,
        INVALID_APP_EVENT_RELATION ,
        EVENT_GRAY_SCALE ,
        NO_POLICY_FOUND,
        POLICY_HAS_ERROR,
        NOT_SUPPORTED_POLICY_OPERATOR,
        QPS_EXCEEDING_MAXIMUM_THRESHOLD ,
        SERVICE_INTERNAL_ERROR
    }
}
