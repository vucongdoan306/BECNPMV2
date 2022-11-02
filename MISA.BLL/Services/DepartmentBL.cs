using MISA.BLL.Interfaces;
using MISA.Common;
using MISA.Common.Entities;
using MISA.Common.Resources;
using MISA.DAL.Interface;
using MISA.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.BLL.Services
{
    public class DepartmentBL:BaseBL<Department>,IDepartmentService
    {
        /// <summary>
        /// Khai báo interface IBaseService<Department> & IDepartmentRepository
        /// </summary>
        IBaseService<Department> _baseService;
        IDepartmentRepository _repository;

        /// <summary>
        /// Hàm khởi tại Department BL
        /// </summary>
        /// <param name="repository">IDepartmentRepository</param>
        /// <param name="baseService">IBaseService<Department></param>
        /// CreateBy: Công Đoàn (08/08/2022)
        public DepartmentBL(IDepartmentRepository repository,IBaseService<Department> baseService)
        {
            _repository = repository;
            _baseService = baseService;
        }

        /// <summary>
        /// Thực hiện lấy ra toàn bộ danh sách Department
        /// </summary>
        /// <returns>
        /// Toàn bộ danh sách department
        /// </returns>
        /// CreateBy: Công Đoàn (08/08/2022)
        public IEnumerable<Department> GetAll()
        {
            var data = _repository.GetAll();
            return data;
        }

        /// <summary>
        /// Thực hiện lấy ra 1 bản ghi theo Id 
        /// </summary>
        /// <param name="departmentId">Id của department</param>
        /// <returns>
        /// 1 bản ghi của department lấy theo Id
        /// </returns>
        /// CreateBy: Công Đoàn (08/08/2022)
        public IEnumerable<Department> GetById(Guid departmentId)
        {
            var data = _repository.GetById(departmentId);
            return data;
        }


        /// <summary>
        ///  Hàm thực hiện validate 
        /// </summary>
        /// <param name="entity">Object Department</param>
        /// <param name="mode">Mode(Thêm hoặc Sửa)</param>
        /// <returns>
        /// true - không có lỗi
        /// false - có lỗi
        /// </returns>
        /// CreateBy: Công Đoàn (08/08/2022)
        protected override bool ValidateCustom(Department entity, ActivityMode mode)
        {
            if(entity.DepartmentCode.Length > 20)
            {
                isValidCustom = false;
                listMsgErrors.Add(ResourcesVI.Error_ValidateLengthCode20);
            }
            if (_repository.CheckDepartmentCode(entity.DepartmentCode,entity.DepartmentId,mode))
            {
                isValidCustom = false;
                listMsgErrors.Add(ResourcesVI.Error_ValidateExistDepartmentCode);
            }
            return isValidCustom;
        }

        /// <summary>
        /// Thực hiện validate xóa
        /// </summary>
        /// <param name="id">id của Department</param>
        /// <param name="mode">Mode (xóa)</param>
        /// <returns>
        /// true - tồn tại bản ghi muốn xóa
        /// false - không tồn tại bản ghi muốn xóa
        /// </returns>
        /// CreateBy: Công Đoàn (08/08/2022)
        protected override bool ValidateCustom(Guid id, ActivityMode mode)
        {
            if (_repository.CheckExistDepartmentId(id) == false)
            {
                isValidCustom=false;
                listMsgErrors.Add(ResourcesVI.Error_ValidateNotExistDepartmentId);
            }
            return isValidCustom;
        }
    }
}
