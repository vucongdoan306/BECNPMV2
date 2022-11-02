using MISA.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MySqlConnector;
using MISA.DAL.BaseRepository;
using MISA.Common.Handle;
using MISA.DAL.Interface;

namespace MISA.DAL.Repository
{
    public class EmployeeRepository : BaseRepository<Employee>,IEmployeeRepository
    {
        IBaseRepository<Employee> _repository;
        /// <summary>
        /// hàm khởi tạo
        /// </summary>
        /// <param name="repository"></param>
        public EmployeeRepository(IBaseRepository<Employee> repository)
        {
            _repository = repository;
        }

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
        public bool CheckEmployeeCode(string employeeCode, Guid id, ActivityMode mode)
        {
            if (mode == ActivityMode.PutMode)
            {
                return CheckPutEmployeeCode(employeeCode, id);
            }
            else
            {
                return CheckDuplicateEmployeeCode(employeeCode);
            }
        }

        /// <summary>
        /// Thực hiện kiểm tra trùng mã
        /// </summary>
        /// <param name="employeeCode">Mã nhân viên</param>
        /// <returns>
        /// true - trùng
        /// false - không trùng
        /// </returns>
        /// CreatedBy: Công Đoàn (08/08/2022)
        public bool CheckDuplicateEmployeeCode(string employeeCode)
        {
            var sqlCheck = "SELECT EmployeeCode From Employee WHERE EmployeeCode = @EmployeeCode";
            var dynamicParam = new DynamicParameters();
            dynamicParam.Add("@EmployeeCode", employeeCode);
            var code = _connection.QueryFirstOrDefault(sqlCheck, param: dynamicParam);
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
        /// Thực hiện kiểm tra trùng mã khi update
        /// </summary>
        /// <param name="employeeCode">Mã nhân viên</param>
        /// <param name="id">id nhân viên</param>
        /// <returns>
        /// true - trùng
        /// false - không trùng
        /// </returns>
        /// CreatedBy: Công Đoàn (08/08/2022)
        public bool CheckPutEmployeeCode(string employeeCode, Guid id)
        {
            var sql = $"SELECT EmployeeCode From Employee WHERE EmployeeId = @EmployeeId";
            var parameter = new DynamicParameters();
            parameter.Add("@EmployeeId", id);
            var codePut = _connection.QueryFirstOrDefault<string>(sql, param: parameter);
            if (codePut == employeeCode)
            {
                return false;
            }
            return CheckDuplicateEmployeeCode(employeeCode);
        }

        /// <summary>
        ///  Kiểm tra id nhân viên có tồn tại
        /// </summary>
        /// <param name="id">id nhân viên</param>
        /// <returns>
        /// true - tồn tại
        /// false - không tồn tại
        /// </returns>
        /// CreatedBy: Công Đoàn (08/08/2022)
        public bool CheckExistEmployeeId(Guid id)
        {
            var sql = $"SELECT EmployeeCode From Employee WHERE EmployeeId = @EmployeeId";
            var parameter = new DynamicParameters();
            parameter.Add("@EmployeeId", id);
            var codePut = _connection.QueryFirstOrDefault<string>(sql, param: parameter);
            if (codePut != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Hàm thực hiện nhập data từ excel
        /// </summary>
        /// <param name="employees">List Object Employee</param>
        /// <returns>Số bản ghi thay đổi</returns>
        /// CreatedBy: Công Đoàn (08/08/2022)
        public int ImportEmployee(List<Employee> employees)
        {
            var rowInserted = 0;
            using(var transaction = _connection.BeginTransaction())
            {
                var storeName = $"Proc_InsertEmployee";
                foreach(var emp in employees)
                {
                    rowInserted += _connection.Execute(storeName,param: emp,transaction: transaction,commandType: System.Data.CommandType.StoredProcedure);
                }
                if(rowInserted == 0)
                {
                    transaction.Rollback();
                }
                else
                {
                    transaction.Commit();
                }
            }

            return rowInserted;
        }

        /// <summary>
        /// Thực hiện xóa nhiều nhân viên
        /// </summary>
        /// <param name="employeesId">List id của nhân viên</param>
        /// <returns>Số bản ghi thay đổi</returns>
        /// CreatedBy: Công Đoàn (08/08/2022)
        public int DeleteMultiEmployee(List<Guid> employeesId)
        {
            using(var transaction = _connection.BeginTransaction())
            {
                var storeName = $"Proc_DeleteMultiEmployee";
                var values = "";
                foreach(var employeeId in employeesId)
                {
                    values += $"'{employeeId}',";
                }
                values = values[..^1];

                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@EmployeesId", values);
                var rowEffect = _connection.Execute(storeName, param: parameter, transaction: transaction, commandType: System.Data.CommandType.StoredProcedure);
                if(rowEffect < employeesId.Count)
                {
                    transaction.Rollback();
                }
                else
                {
                    transaction.Commit();
                }
                return rowEffect;
            }
        }
    }
}
