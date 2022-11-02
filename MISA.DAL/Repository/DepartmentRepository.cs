using MISA.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.DAL.BaseRepository;
using Dapper;
using MySqlConnector;
using MISA.Common.Entities;
using MISA.DAL.Interface;

namespace MISA.DAL.Repository
{
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        IBaseRepository<Department> _repository;

        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="repository">Interface của baseRepo</param>
        /// CreatedBy: Công Đoàn (07/07/2022)
        public DepartmentRepository(IBaseRepository<Department> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Hàm lấy ra Id của phòng ban từ tên phòng ban
        /// </summary>
        /// <param name="departmentName">Tên phòng ban</param>
        /// <returns>Id của phòng ban</returns>
        /// CreatedBy: Công Đoàn (07/07/2022)
        public Guid GetIdFromName(string? departmentName)
        {
            var storeName = $"Proc_SearchDepartmentName";
            var parameter = new DynamicParameters();
            parameter.Add("@DepartmentName", departmentName);
            var res = _connection.QueryFirstOrDefault<Guid>(storeName, param: parameter, commandType: CommandType.StoredProcedure);
            return res;
        }


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
        /// CreatedBy: Công Đoàn (07/07/2022)
        public bool CheckDepartmentCode(string departmentCode, Guid id, ActivityMode mode)
        {
            if(mode == ActivityMode.PutMode)
            {
                return CheckPutDepartmentCode(departmentCode, id);
            }
            else
            {
                return CheckDuplicateDepartmentCode(departmentCode);
            }

        }

        /// <summary>
        /// Thực hiện kiểm tra trùng mã khi post
        /// </summary>
        /// <param name="departmentCode">Mã phòng ban</param>
        /// <returns>
        /// true - trùng 
        /// false - không trùng
        /// </returns>
        /// CreatedBy: Công Đoàn (07/07/2022)
        public bool CheckDuplicateDepartmentCode(string departmentCode)
        {
            var sqlCheck = "SELECT DepartmentCode From Department WHERE DepartmentCode = @DepartmentCode";
            var dynamicParam = new DynamicParameters();
            dynamicParam.Add("@DepartmentCode", departmentCode);
            var code = _connection.QueryFirstOrDefault(sqlCheck, param: dynamicParam);
            Dispose();
            if (code == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        /// <summary>
        /// Thực hiện kiểm tra trùng mã khi put
        /// </summary>
        /// <param name="departmentCode">mã phòng ban</param>
        /// <param name="id">id của phòng ban</param>
        /// <returns>
        /// true - trùng 
        /// false - không trùng
        /// </returns>
        /// CreatedBy: Công Đoàn (07/07/2022)
        public bool CheckPutDepartmentCode(string departmentCode, Guid id)
        {
            var sql = $"SELECT DepartmentCode From Department WHERE DepartmentId = @DepartmentId";
            var parameter = new DynamicParameters();
            parameter.Add("@DepartmentId", id);
            var codePut = _connection.QueryFirstOrDefault<string>(sql, param: parameter);
            if (codePut == departmentCode)
            {
                return false;
            }
            return CheckDuplicateDepartmentCode(departmentCode);
        }


        /// <summary>
        /// Hàm thực hiện kiểm tra id tồn tại
        /// </summary>
        /// <param name="id">id của phòng ban</param>
        /// <returns>
        /// true - tồn tại
        ///  false - không tồn tại
        /// </returns>
        /// CreatedBy: Công Đoàn (07/07/2022)
        public bool CheckExistDepartmentId(Guid id)
        {
            var sql = $"SELECT DepartmentCode From Department WHERE DepartmentId = @DepartmentId";
            var parameter = new DynamicParameters();
            parameter.Add("@DepartmentId", id);
            var codePut = _connection.QueryFirstOrDefault<string>(sql, param: parameter);
            if(codePut != null && codePut != "")
            {
                return true;
            }
            return false;
        }
    }
}
