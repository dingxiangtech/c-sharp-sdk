using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DxCsharpSDK
{
    public class Suggestion
    {
        public String code { get; set; }
        public String message { get; set; }

        public Suggestion()
        {
        }

        public Suggestion(String code, String message)
        {
            this.code = code;
            this.message = message;
        }

    }
}
