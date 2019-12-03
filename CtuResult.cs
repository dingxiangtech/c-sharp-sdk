using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DxCsharpSDK
{
    public class CtuResult
    {
        public String RiskLevel { get; set; }                     // 请求的风险级别
        public String RiskType { get; set; }                       // 风险类型,
        public String HitPolicyCode { get; set; }                   // 命中策略code
        public String HitPolicyName { get; set; }                  // 命中策略标题
        public List<HitRule> HitRules { get; set; }                 // 命中规则
        public List<SuggestPolicy> SuggestPolicies { get; set; }    // 建议防控策略
        public List<Suggestion> suggestion { get; set; }    // 处置建议
        public String Flag { get; set; }                           // 客户端请求带上来的标记
        public Dictionary<string, Object> ExtraInfo { get; set; }           // 附加信息

        public CtuResult()
        {
        }

        public CtuResult(String riskLevel)
        {
            this.RiskLevel = riskLevel;
        }
    }
}
