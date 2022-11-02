using MISA.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.BLL.Interfaces
{
    public interface IDepartmentService
    {

        /// <summary>
        /// Thực hiện lấy ra tất cả ban ghi
        /// </summary>
        /// <returns>
        /// Tất cả bản ghi
        /// </returns>
        IEnumerable<Department> GetAll();

        /// <summary>
        /// Thực hiện lấy ra 1 bản ghi theo id
        /// </summary>
        /// <param name="departmentId">id của department</param>
        /// <returns>
        /// Bản ghi được lấy ra theo id
        /// </returns>
        IEnumerable<Department> GetById(Guid departmentId);

        /// <summary>
        ///  Thực hiện thêm mới 1 bản ghi
        /// </summary>
        /// <param name="department">Object</param>
        /// <returns>
        /// Số bản ghi được thay đổi
        /// </returns>
        int InsertService(Department department);


        /// <summary>
        /// Thực hiện thay đỏi 1 bản ghi
        /// </summary>
        /// <param name="department">Object</param>
        /// <param name="departmentId">id của department</param>
        /// <returns>
        /// Số bản ghi được thay đổi
        /// </returns>
        int UpdateService(Department department);

        /// <summary>
        /// Thực hiện xóa 1 bản ghi
        /// </summary>
        /// <param name="departmentId">id của department</param>
        /// <returns>
        /// Số bản ghi được thay đổi
        /// </returns>
        int DeleteService(Guid departmentId);

    }
}
