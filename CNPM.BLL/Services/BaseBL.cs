using CNPM.BLL.Interfaces;
using CNPM.Common;
using CNPM.Common.Entities;
using CNPM.Common.CNPMAttribute;
using CNPM.Common.Resources;
using CNPM.DAL.BaseRepository;
using CNPM.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNPM.BLL.Services
{
    public class BaseBL<CNPMEntity> : IBaseService<CNPMEntity>
    {
        // Biến lưu giá trị validate
        protected bool isValidCustom = true;

        // List lưu thông báo lỗi
        protected List<string> listMsgErrors = new List<string>();

        // Khởi tạo chuỗi kết nối và chức năng
        protected BaseRepository<CNPMEntity> repository = new BaseRepository<CNPMEntity>();


        /// <summary>
        /// Thực hiện thêm mới bản ghi
        /// </summary>
        /// <param name="entity">Object</param>
        /// <returns>
        /// Số bản ghi được thay đổi
        /// </returns>
        /// <exception cref="CNPMException">Lỗi được hanlde</exception>
        public int InsertService(CNPMEntity entity)
        {
            // Xử lý nghiệp vụ
            var isValid = Validate(entity);
            var isValidCustom = ValidateCustom(entity, ActivityMode.PostMode);
            if(isValid == true && isValidCustom == true)
            {
                var res = repository.Insert(entity);
                return res;
            }
            else
            {
                throw new CNPMException(ResourcesVI.Error_ValidateData, listMsgErrors);
            }
        }

        /// <summary>
        /// Thực hiện sửa một bản ghi
        /// </summary>
        /// <param name="entity">Object</param>
        /// <returns>Số bản ghi được thay đổi</returns>
        /// <exception cref="CNPMException">Lỗi được handle</exception>
        public int UpdateService(CNPMEntity entity)
        {
            // Xử lý nghiệp vụ
            var isValid = Validate(entity);
            var isValidCustom = ValidateCustom(entity, ActivityMode.PutMode);
            if (isValid == true && isValidCustom == true)
            {
                var res = repository.Update(entity);
                return res;
            }
            else
            {
                throw new CNPMException(ResourcesVI.Error_ValidateData, listMsgErrors);
            }
        }

        /// <summary>
        /// Thực hiện xóa 1 bản ghi
        /// </summary>
        /// <param name="id">id của bản ghi</param>
        /// <returns>Số bản ghi thay đổi</returns>
        /// <exception cref="CNPMException">Lỗi được handle</exception>
        public int DeleteService(Guid id)
        {
            // Xử lý nghiệp vụ
            var isValidCustom = ValidateCustom(id, ActivityMode.DeleteMode);
            if(isValidCustom == true)
            {
                var res = repository.Delete(id);
                return res;
            }
            else
            {
                throw new CNPMException(ResourcesVI.Error_ValidateData, listMsgErrors);
            }
        }

        /// <summary>
        /// Thực hiện validate lỗi
        /// </summary>
        /// <param name="entity">Object</param>
        /// <returns>
        /// true - Không có lỗi
        /// false - có lỗi 
        /// </returns>
        public bool Validate(CNPMEntity entity)
        {
            var isValid = true;
            // validate chung
            // Kiểm tra các thông tin bắt buộc nhập:
            var properties = entity.GetType().GetProperties();
            foreach (var property in properties)
            {
                var propName = property.Name;
                var value = property.GetValue(entity);
                var cnpmRequired = property.IsDefined(typeof(CNPMRequired), false);
                var maxLength = property.IsDefined(typeof(MaxLength), false);
                // Nếu dữ liệu trống hoặc không có giá trị
                if(cnpmRequired == true && (value == null || value.ToString() == String.Empty))
                {
                    isValid = false;
                    var arrayPropNameDisplay = property.GetCustomAttributes(typeof(PropertyNameDisplay), false).FirstOrDefault();
                    propName = (arrayPropNameDisplay as PropertyNameDisplay).PropName;
                    listMsgErrors.Add(String.Format(ResourcesVI.Error_ValidateEmptyRequired,propName));
                }
                // Nếu dữ liệu đầu vào có độ dài vượt quá độ dài tối đa
                else if(maxLength == true && (value!= null))
                {
                    var length = (property.GetCustomAttributes(typeof(MaxLength), false).FirstOrDefault() as MaxLength).Length;
                    if(value.ToString().Length > length)
                    {
                        isValid = false;
                        var arrayPropNameDisplay = property.GetCustomAttributes(typeof(PropertyNameDisplay), false).FirstOrDefault();
                        propName = (arrayPropNameDisplay as PropertyNameDisplay).PropName;
                        listMsgErrors.Add(String.Format(ResourcesVI.Error_ValidateMaxLength,propName,length));
                    }
                }
            }
            return isValid;
        }

        /// <summary>
        /// Thực hiên custom lỗi
        /// </summary>
        /// <param name="entity">Object</param>
        /// <param name="mode">Mode (thêm hoặc sửa)</param>
        /// <returns>
        /// true - không có lỗi
        /// false - có lỗi
        /// </returns>
        protected virtual bool ValidateCustom(CNPMEntity entity,ActivityMode mode)
        {
            return true;
        }

        /// <summary>
        /// Thực hiện custom lỗi
        /// </summary>
        /// <param name="id">id của bản ghi</param>
        /// <param name="mode">Xóa</param>
        /// <returns>
        /// true - không có lỗi 
        /// false - có lỗi
        /// </returns>
        protected virtual bool ValidateCustom(Guid id,ActivityMode mode)
        {
            return true;
        }
    }
}
