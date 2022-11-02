using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Common.MISAAttribute
{
    /// <summary>
    /// Thuộc tính bắt buộc nhập
    /// </summary>
    [AttributeUsage(AttributeTargets.Property| AttributeTargets.Field)]
    public class MISARequired :Attribute
    {

    }

    /// <summary>
    /// Thuộc tính quy định tên trường
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class PropertyNameDisplay : Attribute
    {
        /// <summary>
        /// Tên trường
        /// </summary>
        public string PropName { get; set; }

        public PropertyNameDisplay(string propName)
        {
            this.PropName = propName;
        }
    }

    /// <summary>
    /// Thuộc tính quy định độ dài kí tự của trường
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class MaxLength : Attribute
    {
        public int Length { get; set; }

        public MaxLength(int lenght)
        {
            this.Length = lenght;
        }
    }
}
