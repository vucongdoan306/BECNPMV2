using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNPM.Common.Excels
{
    public class ExcelExport
    {
        /// <summary>
        /// Data của File
        /// </summary>
        public byte[] FileContents { get; set; }

        /// <summary>
        /// Loại File
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// Tên File
        /// </summary>
        public string FileName { get; set; }
    }
}
