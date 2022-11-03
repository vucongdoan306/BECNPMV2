using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNPM.Common
{
    public class CNPMException : Exception
    {
        string CNPMMessage;
        IDictionary errors;
        public CNPMException(string msg = "", List<string> listMsgs = null)
        {
            this.CNPMMessage = msg;
            errors = new Dictionary<string, List<string>>();
            errors.Add("validateError", listMsgs);
        }

        public override string Message => this.CNPMMessage;

        public override IDictionary Data => this.errors;
    }
}
