using MISA.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.DAL.Interface
{
    public interface IEmployeeRepository
    {
        /// <summary>
        /// Thực hiện lấy ra toàn bộ nhân viên
        /// </summary>
        /// <returns>
        /// Toàn bộ nhân viên
        /// </returns>
        /// CreatedBy: Công Đoàn (08/08/2022)
        IEnumerable<Employee> GetAll();

        /// <summary>
        /// Thực hiện lấy ra bản ghi theo Id
        /// </summary>
        /// <param name="employeeId">Id của nhân viên</param>
        /// <returns>
        /// Bản ghi được lấy ra theo Id
        /// </returns>
        /// CreatedBy: Công Đoàn (08/08/2022)
        IEnumerable<Employee> GetById(Guid employeeId);

        /// <summary>
        /// Thực hiện thêm mới 1 bản ghi 
        /// </summary>
        /// <param name="employee">Object</param>
        /// <returns>
        /// Số bản ghi phải thay đổi
        /// </returns>
        /// CreatedBy: Công Đoàn (08/08/2022)
        int Insert(Employee employee);

        /// <summary>
        /// Thực hiện thay đổi 1 bản ghi
        /// </summary>
        /// <param name="employee">Object</param>
        /// <returns>
        /// Số bản ghi được thay đổi
        /// </returns>
        /// CreatedBy: Công Đoàn (08/08/2022)
        int Update(Employee employee);

        /// <summary>
        /// Thực hiện xóa 1 bản ghi
        /// </summary>
        /// <param name="employeeId">Id của employee</param>
        /// <returns>
        /// Số bản ghi được thay đổi
        /// </returns>
        /// CreatedBy: Công Đoàn (08/08/2022)
        int Delete(Guid employeeId);

        /// <summary>
        /// Thục hiện lấy ra toàn bộ Code của Employee
        /// </summary>
        /// <returns>
        /// Toàn bộ code của employee
        /// </returns>
        /// CreatedBy: Công Đoàn (08/08/2022)
        IEnumerable<string> GetAllCode();

        /// <summary>
        /// Thực hiện filter (Phân trang, tìm kiếm)
        /// </summary>
        /// <param name="searchText">Từ khóa tìm kiếm</param>
        /// <param name="pageSize">Số bản ghi trên một trang</param>
        /// <param name="pageNumber">Chỉ số trang</param>
        /// <returns>Object(Tổng số bản ghi, tổng số trang, data phân trang)</returns>
        /// CreatedBy: Công Đoàn (08/08/2022)
        object Filter(string? searchText, int pageSize, int pageNumber);

        /// <summary>
        /// Thực hiện kiểm tra mã nhân viên
        /// </summary>
        /// <param name="employeeCode">Mã nhận viên</param>
        /// <param name="id">id của Employee</param>
        /// <param name="mode">thêm hoặc sửa</param>
        /// <returns>
        /// false - nếu không tồn tại
        /// true - nếu tồn tại
        /// </returns>
        /// CreatedBy: Công Đoàn (08/08/2022)
        bool CheckEmployeeCode(string employeeCode, Guid id, ActivityMode mode);

        /// <summary>
        /// Thực hiện kiểm tra trùng mã
        /// </summary>
        /// <param name="employeeCode">Mã nhân viên</param>
        /// <returns>
        /// true - trùng
        /// false - không trùng
        /// </returns>
        /// CreatedBy: Công Đoàn (08/08/2022)
        bool CheckDuplicateEmployeeCode(string employeeCode);

        /// <summary>
        /// Thực hiện kiểm tra trùng mã khi update
        /// </summary>
        /// <param name="employeeCode">Mã nhân viên</param>
        /// <param name="id">id nhân viên</param>
        /// <returns>
        /// true - trùng
        /// false - không trùng
        /// </returns>
        /// CreatedBy: Công Đoàn (08/08/2022)
        bool CheckPutEmployeeCode(string employeeCode, Guid id);

        /// <summary>
        ///  Kiểm tra id nhân viên có tồn tại
        /// </summary>
        /// <param name="id">id nhân viên</param>
        /// <returns>
        /// true - tồn tại
        /// false - không tồn tại
        /// </returns>
        /// CreatedBy: Công Đoàn (08/08/2022)
        bool CheckExistEmployeeId(Guid id);

        /// <summary>
        /// Hàm thực hiện nhập data từ excel
        /// </summary>
        /// <param name="employees">Object Employee</param>
        /// <returns>Số bản ghi được thêm vào database</returns>
        /// CreatedBy: Công Đoàn (08/08/2022)
        int ImportEmployee(List<Employee> employees);

        /// <summary>
        /// Hàm thực hiện xóa nhiều nhân viên 
        /// </summary>
        /// <param name="employeesId">List id nhân viên cần xóa</param>
        /// <returns>Số bản ghi được xóa</returns>
        /// CreatedBy: Công Đoàn (08/08/2022)
        int DeleteMultiEmployee(List<Guid> employeesId);
    }
}
