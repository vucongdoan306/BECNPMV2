using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CNPM.BLL.Interfaces;
using CNPM.BLL.Services;
using CNPM.Common;
using CNPM.Common.Entities;
using CNPM.Common.Resources;
using CNPM.DAL.Repository;

namespace CNPM.W06.CukCuk.Controllers
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        /// <summary>
        /// Interface IEmployeeService
        /// </summary>
        IEmployeeService _service;

        /// <summary>
        /// Hàm khởi tại EmployeesController
        /// </summary>
        /// <param name="service">IEmployeeService</param>
        public EmployeesController(IEmployeeService service)
        {
            _service = service;
        }

        /// <summary>
        /// Hàm thực hiện lấy ra toàn bộ bản ghi
        /// </summary>
        /// <returns>
        /// Toàn bộ bản ghi và statuscode
        /// Error - Thông báo lỗi và statuscode
        /// </returns>
        /// CreatedBy: Nguyễn Quang Linh (30/10/2022)
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var data = _service.GetAll();
                return Ok(data);
            }catch(Exception ex)
            {
                return HandleException(ex);
            }

        }


        /// <summary>
        /// Hàm thực hiện lấy ra bản ghi theo Id
        /// </summary>
        /// <param name="employeeId">Id của Employee</param>
        /// <returns>
        /// Bản ghi được lấy ra theo Id
        /// Error - Thông báo lỗi và statuscode
        /// </returns>
        /// CreatedBy: Nguyễn Quang Linh (30/10/2022)
        [HttpGet("{employeeId}")]
        public IActionResult getById(Guid employeeId)
        {
            try
            {
                var data = _service.GetById(employeeId);
                return Ok(data);
            }
            catch(Exception ex)
            {
                return HandleException(ex);
            }
        }


        /// <summary>
        /// Hàm thực hiện thêm mới bản ghi
        /// </summary>
        /// <param name="employee">Object Employee</param>
        /// <returns>
        /// Số bản ghi thay đổi và statuscode
        /// Error - Thông báo lỗi và statuscode
        /// </returns>
        /// CreatedBy: Nguyễn Quang Linh (30/10/2022)
        [HttpPost]
        public IActionResult Insert(Employee employee)
        {
            employee.EmployeeId = new Guid();
            try
            {
                var res = _service.InsertService(employee);
                return StatusCode(201, res);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Hàm thực hiện thay đổi 1 bản ghi
        /// </summary>
        /// <param name="employee">Object employee</param>
        /// <param name="employeeId">Id của Employee</param>
        /// <returns>
        /// Số bản ghi thay đỏi và statuscode
        /// Error - Thông báo lỗi và statuscode
        /// </returns>
        /// CreatedBy: Nguyễn Quang Linh (30/10/2022)
        [HttpPut("{employeeId}")]
        public IActionResult Update(Employee employee, Guid employeeId)
        {
            try
            {
                employee.EmployeeId = employeeId;
                var res = _service.UpdateService(employee);
                return StatusCode(201, res);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }


        /// <summary>
        /// Thực hiện xóa 1 bản ghi 
        /// </summary>
        /// <param name="employeeId">ID của employee</param>
        /// <returns>
        /// Số bản ghi thay đổi và statuscode
        /// Error - Thông báo lỗi và statuscode
        /// </returns>
        /// CreatedBy: Nguyễn Quang Linh (30/10/2022)
        [HttpDelete("{employeeId}")]
        public IActionResult Delete(Guid employeeId)
        {
            try
            {
                var res = _service.DeleteService(employeeId);
                return Ok(res);
            }catch(Exception ex)
            {
                return HandleException(ex);
            }
        }


        /// <summary>
        /// Thực hiện tạo một mã nhân viên mới
        /// </summary>
        /// <returns>
        /// Mã nhân viên mới
        /// Error - Thông báo lỗi và statuscode
        /// </returns>
        /// CreatedBy: Nguyễn Quang Linh (30/10/2022)
        [HttpGet("NewEmployeeCode")]
        public IActionResult GetNewEmployeeCode()
        {
            try
            {
                var res = _service.GetNewEmployeeCodeService();
                return Ok(res);
            }catch(Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Thực hiện filter (Phân trang, tìm kiếm)
        /// </summary>
        /// <param name="searchText">Từ khóa tìm kiếm</param>
        /// <param name="pageSize">Số bản ghi trên một trang</param>
        /// <param name="pageNumber">Chỉ số trang</param>
        /// <returns>
        /// Số bản ghi, tổng số bản ghi, bản ghi bắt đầu và bản ghi kết thúc
        /// Error - Thông báo lỗi và statuscode
        /// </returns>
        /// CreatedBy: Nguyễn Quang Linh (30/10/2022)
        [HttpGet("Filter")]
        public IActionResult Filter(string? searchText, int pageSize, int pageNumber)
        {
            try
            {
                var data = _service.EmployeeFiler(searchText, pageSize, pageNumber);
                return Ok(data);
            }catch(Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Thực hiện import database từ excel
        /// </summary>
        /// <param name="fileImport">File excel</param>
        /// <returns> Số bản ghi thay đổi trong database</returns>
        /// CreatedBy: Nguyễn Quang Linh (30/10/2022)
        [HttpPost("Import")]
        public IActionResult Import(IFormFile fileImport)
        {
            try
            {
                var res = _service.ImportEmployeesService(fileImport);
                return Ok(res);
            }catch(Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Thực hiện xuất database ra excel
        /// </summary>
        /// <returns>File excel chứa database</returns>
        /// CreatedBy: Nguyễn Quang Linh (30/10/2022)
        [HttpGet("Export")]
        public IActionResult Export()
        {
            try
            {
                var res = _service.Export();
                return File(res.FileContents,res.ContentType,res.FileName);
            }catch(Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Hàm thực hiện xóa nhiều nhân viên
        /// </summary>
        /// <param name="employeesId">Danh sách mã nhân viên</param>
        /// <returns>Số bản ghi ảnh hưởng</returns>
        /// CreatedBy: Nguyễn Quang Linh (30/10/2022)
        [HttpDelete("DeleteMulti")]
        public IActionResult DeleteMulti(List<Guid> employeesId)
        {
            try
            {
                var res = _service.DeleteMultiEmployee(employeesId);
                return Ok(res);
            }catch(Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Thực hiện handle lại lỗi
        /// </summary>
        /// <param name="ex">Exception</param>
        /// <returns>
        /// Lỗi được handle và statuscode
        /// </returns>
        /// CreatedBy: Nguyễn Quang Linh (30/10/2022)
        private IActionResult HandleException(Exception ex)
        {
            var res = new ErrorService();
            res.DevMsg = ex.Message;
            res.UserMsg = ResourcesVI.Error_ValidateData;
            res.Data = ex.Data;
            if (ex is CNPMException)
            {
                return BadRequest(res);
            }
            return StatusCode(500, res);
        }
    }
}
