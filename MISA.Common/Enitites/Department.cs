using MISA.Common.MISAAttribute;


namespace MISA.Common.Entities
{
    public class Department
    {
        /// <summary>
        /// Id của department
        /// </summary>
        public Guid DepartmentId { get; set; }


        /// <summary>
        /// Mã phòng ban
        /// </summary>
        [MISARequired]
        [PropertyNameDisplay("Mã phòng ban")]
        [MaxLength(20)]
        public string? DepartmentCode { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        [MISARequired]
        [PropertyNameDisplay("Tên phòng ban")]
        [MaxLength(255)]
        public string? DepartmentName { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        [PropertyNameDisplay("Ngày tạo")]
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        [PropertyNameDisplay("Người tạo")]
        [MaxLength(255)]

        public string? CreatedBy { get; set; }

        /// <summary>
        /// Ngày thay đổi
        /// </summary>
        [PropertyNameDisplay("Ngày thay đổi")]
        public DateTime? ModifiedDate { get; set; }


        /// <summary>
        /// Người thay đổi
        /// </summary>
        [PropertyNameDisplay("Người thay đổi")]
        [MaxLength(255)]
        public string? ModifiedBy { get; set; }

    }
}
