namespace MISA.BLL.Services
{
    public class ErrorService
    {
        /// <summary>
        /// Lỗi hiện thị cho Dev
        /// </summary>
        public string DevMsg { get; set; }

        /// <summary>
        /// Lỗi hiển thị cho người dùng
        /// </summary>
        public string UserMsg { get; set; }

        /// <summary>
        /// Các thông báo lỗi 
        /// </summary>
        public object Data { get; set; }
    }
}
