using MISA.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.DAL.Interface
{
    public interface IDepartmentRepository
    {
        /// <summary>
        /// Thực hiện lấy ra toàn bộ dữ liệu
        /// </summary>
        /// <returns>Toàn bộ dữ liệu</returns>
        IEnumerable<Department> GetAll();

        /// <summary>
        /// Hàm lấy ra bản ghi theo id
        /// </summary>
        /// <param name="departmentId">id của department</param>
        /// <returns>bản ghi theo id</returns>
        IEnumerable<Department> GetById(Guid departmentId);

        /// <summary>
        /// Thực hiện thêm mới 1 bản ghi
        /// </summary>
        /// <param name="department">Object</param>
        /// <returns>Trả về số bản ghi thay đổi</returns>
        int Insert(Department department);


        /// <summary>
        /// Thực hiện thay đỏi 1 bản ghi
        /// </summary>
        /// <param name="department">Object</param>
        /// <returns>Trả về số bản ghi thay đổi</returns>
        int Update(Department department);

        /// <summary>
        /// Thực hiện xóa 1 bản ghi
        /// </summary>
        /// <param name="departmentId">id của department</param>
        /// <returns>Số bản ghi thay đổi</returns>
        int Delete(Guid departmentId);

        /// <summary>
        /// Thực hiện lấy ra toàn bộ id của department
        /// </summary>
        /// <returns> toàn bộ id của department</returns>
        IEnumerable<Guid> GetAllId();

        /// <summary>
        /// Thực hiện kiểm tra trùng mã theo chế độ
        /// </summary>
        /// <param name="departmentCode">mã phòng ban</param>
        /// <param name="id">id của department</param>
        /// <param name="mode">thêm hoặc sửa</param>
        /// <returns>
        /// true - tồn tại
        /// false - không tồn tại
        /// </returns>
        bool CheckDepartmentCode(string departmentCode, Guid id, ActivityMode mode);

        /// <summary>
        /// Thực hiện kiểm tra trùng mã khi post
        /// </summary>
        /// <param name="departmentCode">Mã phòng ban</param>
        /// <returns>
        /// true - trùng 
        /// false - không trùng
        /// </returns>
        bool CheckDuplicateDepartmentCode(string departmentCode);

        /// <summary>
        /// Thực hiện kiểm tra trùng mã khi put
        /// </summary>
        /// <param name="departmentCode">mã phòng ban</param>
        /// <param name="id">id của phòng ban</param>
        /// <returns>
        /// true - trùng 
        /// false - không trùng
        /// </returns>
        bool CheckPutDepartmentCode(string departmentCode, Guid id);

        /// <summary>
        /// Hàm thực hiện kiểm tra id tồn tại
        /// </summary>
        /// <param name="id">id của phòng ban</param>
        /// <returns>
        /// true - tồn tại
        ///  false - không tồn tại
        /// </returns>
        bool CheckExistDepartmentId(Guid id);

        /// <summary>
        /// Hàm lấy ra id của phòng ban từ tên phòng ban
        /// </summary>
        /// <param name="departmentName">Tên phòng ban</param>
        /// <returns>Id của phòng ban</returns>
        Guid GetIdFromName(string departmentName);
    }
}
