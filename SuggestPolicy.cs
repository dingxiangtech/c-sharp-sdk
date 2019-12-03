using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DxCsharpSDK
{
    public class SuggestPolicy
    {
        public String code { get; set; }
        public String name { get; set; }

        public SuggestPolicy()
        {
        }

        public SuggestPolicy(String code, String name)
        {
            this.code = code;
            this.name = name;
        }

    }
}
