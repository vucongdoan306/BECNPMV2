using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.DAL.Interface
{
    public interface IBaseRepository<MISAEntity>
    {
        /// <summary>
        /// Hàm lấy ra toàn bộ bản ghi
        /// </summary>
        /// <returns>
        /// Toàn bộ bản ghi
        /// </returns>
        /// CreatedBy: Công Đoàn (07/07/2022)
        IEnumerable<MISAEntity> GetAll();

        /// <summary>
        /// Hàm lấy ra 1 bản ghi theo id
        /// </summary>
        /// <param name="id">id của đối tượng cần lấy ra</param>
        /// <returns>
        /// Bản ghi theo id
        /// </returns>
        /// CreatedBy: Công Đoàn (07/07/2022)
        IEnumerable<MISAEntity> GetById(Guid id);

        /// <summary>
        /// Thực hiện thêm mới bản ghi
        /// </summary>
        /// <param name="entity">Object</param>
        /// <returns>Số bản ghi thay đổi</returns>
        /// CreatedBy: Công Đoàn (07/07/2022)
        int Insert(MISAEntity entity);

        /// <summary>
        /// Hàm thực hiện thay đổi 1 bản ghi
        /// </summary>
        /// <param name="entity">Object</param>
        /// <returns>Số bản ghi thay đổi</returns>
        /// CreatedBy: Công Đoàn (07/07/2022)
        int Update(MISAEntity entity);

        /// <summary>
        /// Hàm thực hiện xóa 1 bản ghi
        /// </summary>
        /// <param name="id">id của bản ghi</param>
        /// <returns>Số bản ghi thay đổi</returns>
        /// CreatedBy: Công Đoàn (07/07/2022)
        int Delete(Guid id);

        /// <summary>
        /// Hàm lấy ra toàn bộ id
        /// </summary>
        /// <returns>toàn bộ id</returns>
        /// CreatedBy: Công Đoàn (07/07/2022)
        IEnumerable<Guid> GetAllId();

        /// <summary>
        /// Hàm lấy ra toàn bộ code
        /// </summary>
        /// <returns>toàn bộ code</returns>
        /// CreatedBy: Công Đoàn (07/07/2022)
        IEnumerable<string> GetAllCode();

        /// <summary>
        /// Hàm thực hiện filter(Phân trang, tìm kiếm)
        /// </summary>
        /// <param name="searchText">Từ khóa tìm kiếm</param>
        /// <param name="pageSize">Số bản ghi trên một trang</param>
        /// <param name="pageNumber">Chỉ số trang</param>
        /// <returns>Trả về data, số trang, phân trang</returns>
        /// CreatedBy: Công Đoàn (07/07/2022)
        object Filter(string? searchText, int pageSize, int pageNumber);
    }
}
