using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using Dapper;
using System.Data;
using MISA.DAL.Interface;

namespace MISA.DAL.BaseRepository
{
    public class BaseRepository<MISAEntity> :IDisposable,IBaseRepository<MISAEntity>
    {
        protected readonly string _connectionString = "";
        protected MySqlConnection _connection;
        protected readonly string className = typeof(MISAEntity).Name;

        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// CreatedBy: Công Đoàn (07/07/2022)
        public BaseRepository()
        {
            //_connectionString = @"Host=3.0.89.182; " +
            //                            "Port=3306; " +
            //                            "Database=MISA.WEB06.VCDOAN; " +
            //                            "User Id=dev; " +
            //                            "Password=12345678;" + "TransparentNetworkIPResolution=False";

            _connectionString = @"Host=localhost; " +
                            "Port=3306; " +
                            "Database=MISA.WEB06.VCDOAN; " +
                            "User Id=root; " +
                            "Password=123456";
            _connection = new MySqlConnection(_connectionString);
            if(_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
        }

        /// <summary>
        /// Hàm hủy kết nối 
        /// </summary>
        /// CreatedBy: Công Đoàn (07/07/2022)
        public void Dispose()
        {
            _connection.Close();
            _connection.Dispose();
        }

        /// <summary>
        /// Hàm lấy ra toàn bộ bản ghi
        /// </summary>
        /// <returns>
        /// Toàn bộ bản ghi
        /// </returns>
        /// CreatedBy: Công Đoàn (07/07/2022)
        public IEnumerable<MISAEntity> GetAll()
        {

            // Khai báo sqlCommand
            var sql = $"SELECT * FROM view_{className} ORDER BY view_{className}.CreatedDate DESC";

            // Thực hiện lấy dữ liệu
            var data = _connection.Query<MISAEntity>(sql);
            Dispose();
            return data;
        }

        /// <summary>
        /// Hàm lấy ra 1 bản ghi theo id
        /// </summary>
        /// <param name="id">id của bản ghi</param>
        /// <returns>
        /// Bản ghi theo id
        /// </returns>
        /// CreatedBy: Công Đoàn (07/07/2022)
        public IEnumerable<MISAEntity> GetById(Guid id)
        {  
            var sql = $"SELECT * FROM View_{className} WHERE {className}Id = @Id";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            var data = _connection.Query<MISAEntity>(sql, parameters);
            Dispose();
            return data;
        }

        /// <summary>
        /// Thực hiện thêm mới bản ghi
        /// </summary>
        /// <param name="entity">Object</param>
        /// <returns>Số bản ghi thay đổi</returns>
        /// CreatedBy: Công Đoàn (07/07/2022)
        public int Insert(MISAEntity entity)
        {
            using (var transaction = _connection.BeginTransaction())
            {
                // Tên thủ tục
                var storeName = $"Proc_Insert{className}";

                var res = _connection.Execute(storeName, param: entity, transaction: transaction, commandType: System.Data.CommandType.StoredProcedure);
                if(res == 0)
                {
                    transaction.Rollback();
                }
                transaction.Commit();
                Dispose();
                return res;
            }
        }


        /// <summary>
        /// Hàm thực hiện thay đổi 1 bản ghi
        /// </summary>
        /// <param name="entity">Object</param>
        /// <returns>Số bản ghi thay đổi</returns>
        /// CreatedBy: Công Đoàn (07/07/2022)
        public int Update(MISAEntity entity)
        {
            using (var transaction = _connection.BeginTransaction())
            {
                // Tên thủ tục
                var storeName = $"Proc_Update{className}";

                var res = _connection.Execute(storeName, param: entity, transaction: transaction, commandType: System.Data.CommandType.StoredProcedure);
                if (res == 0)
                {
                    transaction.Rollback();
                }
                transaction.Commit();
                Dispose();
                return res;
            }
        }

        /// <summary>
        /// Hàm thực hiện xóa 1 bản ghi
        /// </summary>
        /// <param name="id">id của bản ghi</param>
        /// <returns>Số bản ghi thay đổi</returns>
        /// CreatedBy: Công Đoàn (07/07/2022)
        public int Delete(Guid id)
        {
            using(var transaction = _connection.BeginTransaction()) { 
                var storeName = $"Proc_Delete{className}";
                DynamicParameters dynamicParam = new DynamicParameters();
                dynamicParam.Add($"@{className}Id", id);
                var res = _connection.Execute(storeName, param: dynamicParam,transaction: transaction, commandType: System.Data.CommandType.StoredProcedure);
                if(res == 0)
                {
                    transaction.Rollback();
                }
                transaction.Commit();
                Dispose();
                return res;

            }
        }

        /// <summary>
        /// Hàm lấy ra toàn bộ id
        /// </summary>
        /// <returns>toàn bộ id</returns>
        /// CreatedBy: Công Đoàn (07/07/2022)
        public IEnumerable<Guid> GetAllId()
        {
            var sql = $"SELECT {className}Id From {className}";
            var data = _connection.Query<Guid>(sql);
            Dispose();
            return data;
        }

        /// <summary>
        /// Hàm lấy ra toàn bộ code
        /// </summary>
        /// <returns>toàn bộ code</returns>
        /// CreatedBy: Công Đoàn (07/07/2022)
        public IEnumerable<string> GetAllCode()
        {
            var sql = $"SELECT {className}Code FROM {className}";
            var data = _connection.Query<string>(sql);
            Dispose();
            return data;
        }

        /// <summary>
        /// Hàm thực hiện filter(Phân trang, tìm kiếm)
        /// </summary>
        /// <param name="searchText">Từ khóa tìm kiếm</param>
        /// <param name="pageSize">Số bản ghi trên một trang</param>
        /// <param name="pageNumber">Chỉ số trang</param>
        /// <returns>Class chứ data phân trang</returns>
        /// CreatedBy: Công Đoàn (07/07/2022)
        public object Filter(string? searchText, int pageSize, int pageNumber)
        {
            
            var storeName = $"Proc_Filter{className}";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@searchText", searchText);
            parameters.Add("@pageSize", pageSize);
            parameters.Add("@pageNumber", pageNumber);
            parameters.Add("@TotalRecord", DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@TotalPage", DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@RecordStart", DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@RecordEnd", DbType.Int32, direction: ParameterDirection.Output);

            var data = _connection.Query<MISAEntity>(storeName,param: parameters,commandType: CommandType.StoredProcedure);
            int totalRecord = parameters.Get<int>("@TotalRecord");
            int totalPage = parameters.Get<int>("@TotalPage");
            int recordStart = parameters.Get<int>("@RecordStart");
            int recordEnd = parameters.Get<int>(@"RecordEnd");
            Dispose();
            return new { TotalPage = totalPage, TotalRecord = totalRecord, RecordStart = recordStart, RecordEnd = recordEnd, Data = data };

        }
    }
}
