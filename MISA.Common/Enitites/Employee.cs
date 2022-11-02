

using MISA.Common.MISAAttribute;
using System.ComponentModel;

namespace MISA.Common.Entities { 
    public class Employee
    {
        /// <summary>
        /// Id của nhân viên
        /// </summary>
        [Description("Id nhân viên")]
        public Guid EmployeeId { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        [MISARequired]
        [PropertyNameDisplay("Mã nhân viên")]
        [Description("Mã nhân viên")]
        [MaxLength(20)]
        public string? EmployeeCode { get; set; }
        
        /// <summary>
        /// Tên nhân viên
        /// </summary>
        [MISARequired]
        [PropertyNameDisplay("Tên nhân viên")]
        [Description("Tên nhân viên")]
        [MaxLength(100)]
        public string? FullName { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        [Description("Ngày sinh")]
        [PropertyNameDisplay("Ngày sinh")]
        public DateTime? DateOfBirth { get; set; }


        /// <summary>
        /// Giới tính
        /// </summary>
        [Description("Giới tính")]
        [PropertyNameDisplay("Giới tính")]
        public Gender? Gender { get; set; }

        /// <summary>
        /// Số chứng thư nhân dân
        /// </summary>
        [Description("Số chứng minh thư")]
        [PropertyNameDisplay("Số chứng minh thư")]
        [MaxLength(25)]
        public string? IdentityNumber { get; set; }

        /// <summary>
        /// Ngày cấp chứng thư nhân dân
        /// </summary>
        [Description("Ngày cấp")]
        [PropertyNameDisplay("Ngày cấp")]
        public DateTime? IdentityDate { get; set; }

        /// <summary>
        /// Địa điểm cấp
        /// </summary>
        [Description("Nơi cấp")]
        [PropertyNameDisplay("Nơi cấp")]
        [MaxLength(255)]
        public string? IdentityPlace { get; set; }

        /// <summary>
        /// id của phòng ban
        /// </summary>
        [Description("Id phòng ban")]
        [PropertyNameDisplay("Id phòng ban")]
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        [Description("Tên phòng ban")]
        [PropertyNameDisplay("Tên phòng ban")]
        public string? DepartmentName { get; set; }

        /// <summary>
        /// Mã phòng ban
        /// </summary>
        [Description("Mã phòng ban")]
        [PropertyNameDisplay("Mã phòng ban")]
        public string? DepartmentCode { get; set; }

        /// <summary>
        /// Tên chức vụ
        /// </summary>
        [Description("Chức danh")]
        [PropertyNameDisplay("Chức danh")]
        [MaxLength(255)]
        public string? PositionName { get; set; }


        /// <summary>
        /// Địa chỉ
        /// </summary>
        [Description("Địa chỉ")]
        [PropertyNameDisplay("Địa chỉ")]
        [MaxLength(255)]
        public string? Address { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        [Description("Số điện thoại")]
        [PropertyNameDisplay("Số điện thoại")]
        [MaxLength(50)]
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Điện thoại bàn
        /// </summary>
        [Description("Điện thoại bàn")]
        [PropertyNameDisplay("Điện thoại bàn")]
        [MaxLength(50)]
        public string? TelephoneFax { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [Description("Email")]
        [PropertyNameDisplay("Điện thoại bàn")]
        [MaxLength(100)]
        public string? Email { get; set; }

        /// <summary>
        /// Số tài khoản
        /// </summary>
        [Description("Số tài khoản")]
        [PropertyNameDisplay("Số tài khoản")]
        [MaxLength(255)]
        public string? BankAccount { get; set; }

        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        [Description("Tên ngân hàng")]
        [PropertyNameDisplay("Tên ngân hàng")]
        [MaxLength(255)]
        public string? BankName { get; set; }

        /// <summary>
        /// Địa chỉ mở ngân hàng
        /// </summary>
        [Description("Chi nhánh")]
        [PropertyNameDisplay("Chi nhánh")]
        [MaxLength(255)]
        public string? BankBranch { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        [Description("Ngày tạo")]
        [PropertyNameDisplay("Ngày tạo")]
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        [Description("Người tạo")]
        [PropertyNameDisplay("Người tạo")]
        [MaxLength(255)]
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Ngày thay đổi 
        /// </summary>
        [Description("Ngày thay đổi")]
        [PropertyNameDisplay("Ngày thay đổi")]
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Ngày thay đổi
        /// </summary>
        [Description("Người thay đổi")]
        [PropertyNameDisplay("Người thay đổi")]
        [MaxLength(255)]
        public string? ModifiedBy { get; set; }

        #region Other
        /// <summary>
        /// Tên giới tính
        /// </summary>
        public string GenderName
        {
            get
            {
                switch (Gender)
                {
                    case Common.Entities.Gender.Male:
                        return "Nam";
                    case Common.Entities.Gender.Female:
                        return "Nữ";
                    case Common.Entities.Gender.Other:
                        return "Khác";
                    default:
                        return null;
                }
            }
        }
        #endregion


        public List<string> ErrorValidatesImport { get; set; } = new List<string>();
    }
}
