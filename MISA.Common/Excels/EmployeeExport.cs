using MISA.Common.Entities;
using MISA.Common.MISAAttribute;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Common.Excels
{
    public class EmployeeExport
    {
        /// <summary>
        /// Mã nhân viên
        /// </summary>
        [MISARequired]
        [PropertyNameDisplay("Mã nhân viên")]
        [Description("Mã nhân viên")]
        public string? EmployeeCode { get; set; }

        /// <summary>
        /// Tên nhân viên
        /// </summary>
        [MISARequired]
        [PropertyNameDisplay("Tên nhân viên")]
        [Description("Tên nhân viên")]
        public string? FullName { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        [Description("Giới tính")]
        public string? GenderName { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        [Description("Ngày sinh")]
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Tên chức vụ
        /// </summary>
        [Description("Chức danh")]
        public string? PositionName { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        [Description("Tên phòng ban")]
        public string? DepartmentName { get; set; }

        /// <summary>
        /// Số tài khoản
        /// </summary>
        [Description("Số tài khoản")]
        public string? BankAccount { get; set; }

        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        [Description("Tên ngân hàng")]
        public string? BankName { get; set; }
    }
}
