using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNPM.BLL.Interfaces
{
    public interface IBaseService<CNPMEntity>
    {
        /// <summary>
        /// Hàm thêm mới một bản ghi
        /// </summary>
        /// <param name="entity">Object</param>
        /// <returns>
        /// Số bản ghi thay đổiii
        /// </returns>
        int InsertService(CNPMEntity entity);

        /// <summary>
        /// Hàm thay đổi một bản ghi
        /// </summary>
        /// <param name="entity">Object</param>
        /// <returns>
        /// Số bản ghi được thay đổi
        /// </returns>
        int UpdateService(CNPMEntity entity);

        /// <summary>
        /// Hàm thực hiện xóa một bản ghi
        /// </summary>
        /// <param name="id">id của bản ghi</param>
        /// <returns>
        /// Số bản ghi được thay đổi
        /// </returns>
        int DeleteService(Guid id);
    }
}
