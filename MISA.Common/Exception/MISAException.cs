using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Common
{
    public class MISAException : Exception
    {
        string MISAMessenger;
        IDictionary errors;
        public MISAException(string msg = "", List<string> listMsgs = null)
        {
            this.MISAMessenger = msg;
            errors = new Dictionary<string, List<string>>();
            errors.Add("validateError", listMsgs);
        }

        public override string Message => this.MISAMessenger;

        public override IDictionary Data => this.errors;
    }
}
