using Microsoft.AspNetCore.Http;
using MISA.Common.Entities;
using MISA.Common.Excels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.BLL.Interfaces
{
    public interface IEmployeeService
    {
        /// <summary>
        /// Thực hiện lấy ra tất cả số bản ghi
        /// </summary>
        /// <returns>
        /// Tất cả số bản ghi
        /// </returns>
        /// CreatedBy: Công Đoàn (08/08/2022)
        IEnumerable<Employee> GetAll();

        /// <summary>
        /// Thực hiện lấy ra 1 bản ghi theo id
        /// </summary>
        /// <param name="employeeId">id của employee</param>
        /// <returns>bản ghi được lấy ra theo id</returns>
        /// CreatedBy: Công Đoàn (08/08/2022)
        IEnumerable<Employee> GetById(Guid employeeId);

        /// <summary>
        /// Thực hiện thêm mới 1 bản ghi
        /// </summary>
        /// <param name="employee">Object</param>
        /// <returns>Số bản ghi thay đổi</returns>
        /// CreatedBy: Công Đoàn (08/08/2022)

        int InsertService(Employee employee);

        /// <summary>
        /// Thực hiện update 1 bản ghi
        /// </summary>
        /// <param name="employee">Object</param>
        /// <param name="employeeId">Id của employee</param>
        /// <returns>
        /// Số bản ghi thay đổi
        /// </returns>
        /// CreatedBy: Công Đoàn (08/08/2022)
        int UpdateService(Employee employee);

        /// <summary>
        /// Hàm thực hiện xóa 1 bản ghi
        /// </summary>
        /// <param name="employeetId">Id của employee</param>
        /// <returns>
        /// Số bản ghi thay đổi
        /// </returns>
        /// CreatedBy: Công Đoàn (08/08/2022)
        int DeleteService(Guid employeetId);

        /// <summary>
        /// Thực hiện lấy ra toàn bộ mã nhân viên
        /// </summary>
        /// <returns>
        /// Toàn bộ mã nhân viên
        /// </returns>
        /// CreatedBy: Công Đoàn (08/08/2022)
        IEnumerable<string> GetAllCode();

        /// <summary>
        /// Tạo một mã nhân viên mới
        /// </summary>
        /// <returns>Mã nhân viên mới</returns>
        /// CreatedBy: Công Đoàn (08/08/2022)
        string GetNewEmployeeCodeService();

        /// <summary>
        /// Thực hiện filter(Phân trang, tìm kiếm)
        /// </summary>
        /// <param name="searchText">Từ khóa tìm kiếm</param>
        /// <param name="pageSize">Số bản ghi trên một trang</param>
        /// <param name="pageNumber">Chỉ số trang</param>
        /// <returns>
        /// Object(Tổng số bản ghi, tổng số trang, bản ghi)
        /// </returns>
        /// CreatedBy: Công Đoàn (08/08/2022)
        object EmployeeFiler(string? searchText, int pageSize, int pageNumber);

        /// <summary>
        /// Thực hiện import dữ liệu từ excel
        /// </summary>
        /// <param name="fileImport">File excel</param>
        /// <returns>
        /// Số bản ghi thay đổi trong database
        /// </returns>
        /// CreatedBy: Công Đoàn (08/08/2022)
        int ImportEmployeesService(IFormFile fileImport);

        /// <summary>
        /// Thực hiện xuất database thành excel
        /// </summary>
        /// <returns>
        /// File excel chứa data
        /// </returns>
        /// CreatedBy: Công Đoàn (08/08/2022)
        ExcelExport Export();

        /// <summary>
        /// Thực hiện xóa nhiều nhân viên
        /// </summary>
        /// <param name="employeesId">List id của nhân viên</param>
        /// <returns>Số bản ghi thay đổi</returns>
        /// CreatedBy: Công Đoàn (08/08/2022)
        int DeleteMultiEmployee(List<Guid> employeesId);
    }
}
