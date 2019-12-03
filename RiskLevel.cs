using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DxCsharpSDK
{
    public enum RiskLevel
    {
        ACCEPT, // 无风险,建议放过
        REVIEW, // 不确定,需要进一步审核
        REJECT // 有风险,建议拒绝
    }
}
