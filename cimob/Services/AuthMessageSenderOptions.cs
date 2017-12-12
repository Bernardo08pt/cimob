using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cimob.Services
{
    public class AuthMessageSenderOptions
    {
        public string SendGridUser { get; set; }
        public string SendGridKey { get; set; }


        public AuthMessageSenderOptions()
        {
            SendGridUser = "Bernardo08pt";
            SendGridKey = "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";
        }
    }
}
