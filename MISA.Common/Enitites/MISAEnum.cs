namespace MISA.Common.Entities
{
    /// <summary>
    /// Enum giới tính
    /// </summary>
    public enum Gender
    {
        /// <summary>
        /// Giới tính Nam
        /// </summary>
        Male = 0,

        /// <summary>
        /// Giới tính Nữ
        /// </summary>
        Female = 1,

        /// <summary>
        /// Giới tính khác, chưa xác định
        /// </summary>
        Other = 2,
    }

    public enum ActivityMode
    {
        /// <summary>
        /// Lấy ra bản ghi
        /// </summary>
        GetMode = 0,

        /// <summary>
        /// Tạo bản ghi mới
        /// </summary>
        PostMode = 1,

        /// <summary>
        /// Thay đổi bản ghi
        /// </summary>
        PutMode = 2,

        /// <summary>
        /// Xóa bản ghi
        /// </summary>
        DeleteMode = 3,
    }
}
