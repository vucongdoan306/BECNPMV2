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
    public class DepartmentsController : ControllerBase
    {
        /// <summary>
        /// Khai báo Interface IDepartmentService
        /// </summary>
        IDepartmentService _service;

        /// <summary>
        /// Hàm khởi tạo của DepartmentsController
        /// </summary>
        /// <param name="service">Interface IDepartmentService</param>
        public DepartmentsController(IDepartmentService service)
        {
            _service = service;
        }

        /// <summary>
        /// Thực hiện lấy ra tất cả bản ghi
        /// </summary>
        /// <returns>
        /// Tất cả bản ghi trong department và statuscode
        /// Error - Trả về thông báo lỗi
        /// </returns>
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
        /// Thục hiện lấy ra bản ghi theo Id
        /// </summary>
        /// <param name="departmentId">Id của department</param>
        /// <returns>
        /// Bản ghi được lấy ra theo ID và statuscode
        /// Error - Trả về thông báo lỗi
        /// </returns>
        [HttpGet("{departmentId}")]
        public IActionResult GetById(Guid departmentId)
        {
            try
            {
                var data = _service.GetById(departmentId);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Thực hiện thêm mới 1 bản ghi
        /// </summary>
        /// <param name="department">Object Department</param>
        /// <returns>
        /// Số bản ghi thay đổi và statuscode
        /// Error - Trả về thông báo lỗi
        /// </returns>
        [HttpPost]
        public IActionResult Post(Department department)
        {
            department.DepartmentId = new Guid();
            try
            {
                var res = _service.InsertService(department);
                return StatusCode(201,res);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Thay đổi 1 bản ghi
        /// </summary>
        /// <param name="department">Object Department</param>
        /// <param name="departmentId">Id của department</param>
        /// <returns>
        /// Số bản ghi thay đổi và statuscode
        /// Error - Trả về thông báo lỗi
        /// </returns>
        [HttpPut("{departmentId}")]
        public IActionResult Put(Department department, Guid departmentId)
        {
            department.DepartmentId = departmentId;
            try
            {
                var res = _service.UpdateService(department);
                return StatusCode(201, res);
            }catch(Exception ex)
            {
                return HandleException(ex);
            }
        }


        /// <summary>
        /// Thực hiện xóa 1 bản ghi
        /// </summary>
        /// <param name="departmentId">Id của department</param>
        /// <returns>
        /// Số bản ghi thay đổi và statuscode
        /// Error - Trả về thông báo lỗi
        /// </returns>
        [HttpDelete("{departmentId}")]
        public IActionResult Delete(Guid departmentId)
        {
            try
            {
                var res = _service.DeleteService(departmentId);
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
        /// Lỗi sau khi được handle và statuscode
        /// </returns>
        private IActionResult HandleException(Exception ex)
        {
            var res = new ErrorService();
            res.DevMsg = ex.Message;
            res.UserMsg = ResourcesVI.Error_ValidateData;
            res.Data = ex.Data;
            if (ex is MISAException)
            {
                return BadRequest(res);
            }
            return StatusCode(500, res);
        }

    }
}
