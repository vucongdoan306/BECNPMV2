using MISA.Common;
using MISA.Common.Entities;
using MISA.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.Common.Resources;
using System.Text.RegularExpressions;
using MISA.DAL.Interface;
using MISA.BLL.Interfaces;
using MISA.Common.Handle;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using MISA.Common.Excels;
using OfficeOpenXml.Style;
using System.Drawing;

namespace MISA.BLL.Services
{
    public class EmployeeBL:BaseBL<Employee>, IEmployeeService
    {
        /// <summary>
        /// Khai báo interface của EmployeeRepository, BaseBL, DepartmentRepository
        /// </summary>
        /// CreateBy: Công Đoàn (08/08/2022)
        IEmployeeRepository _repository;
        IBaseService<Employee> _baseService;
        IDepartmentRepository _departmentRepository;

        /// <summary>
        /// Hàm khởi tạo EmployeeBL
        /// </summary>
        /// <param name="repository">Interface IEmployeeRepository</param>
        /// <param name="baseService">Interface IBaseService<Employee></param>
        /// <param name="departmentRepository">Interface IDepartmentRepository</param>
        /// CreateBy: Công Đoàn (08/08/2022)
        public EmployeeBL(IEmployeeRepository repository, IBaseService<Employee> baseService, IDepartmentRepository departmentRepository)
        {
            _repository = repository;
            _baseService = baseService;
            _departmentRepository = departmentRepository;
        }

        /// <summary>
        /// Lấy ra tất cả bản ghi trong bảng Employee
        /// </summary>
        /// <returns>Tất cả bản ghi trong bảng Employee</returns>
        /// CreateBy: Công Đoàn (08/08/2022)
        public IEnumerable<Employee> GetAll()
        {
            var data = _repository.GetAll();
            return data;
        }

        /// <summary>
        /// Lấy bản ghi ra theo Id của Employee
        /// </summary>
        /// <param name="employeeId">Id của Employee</param>
        /// <returns>bản ghi ra theo Id của Employee</returns>
        /// CreateBy: Công Đoàn (08/08/2022)
        public IEnumerable<Employee> GetById(Guid employeeId)
        {
            var data = _repository.GetById(employeeId);
            return data;
        }

        /// <summary>
        /// Thực hiện lấy ra toàn bộ mã nhân viên
        /// </summary>
        /// <returns>toàn bộ mã nhân viên</returns>
        /// CreateBy: Công Đoàn (08/08/2022)
        public IEnumerable<string> GetAllCode()
        {
            var data = _repository.GetAllCode();
            return data;
        }

        /// <summary>
        /// Thực hiện tạo ra một mã nhân viên mới không trùng
        /// </summary>
        /// <returns>Mã nhân viên mới không trùng</returns>
        /// CreateBy: Công Đoàn (08/08/2022)
        public string GetNewEmployeeCodeService()
        {

            // Lấy ra toàn bộ code của employee
            var data = _repository.GetAllCode().ToList<string>();

            // Thực hiện loại bỏ kí tự khác số trong một chuỗi
            List<string> listNumber = HandleFormat.GetListNumberFromString(data);

            // hàm thực hiện lấy ra số lớn nhất trong chuỗi
            var max = HandleFormat.GetMaxNumberFromList(listNumber);
            max++;
            string res = String.Concat("NV-", max.ToString());
            return res;
        }

        /// <summary>
        /// Thực hiện filter(phân trang, tìm kiếm) Employee
        /// </summary>
        /// <param name="searchText">Từ khóa tìm kiếm</param>
        /// <param name="pageSize">Số bản ghi trên 1 trang</param>
        /// <param name="pageNumber">Chỉ số trang</param>
        /// <returns>Tổng số bản ghi, bản ghi bắt đầu, bản ghi kết thúc</returns>
        /// CreateBy: Công Đoàn (08/08/2022)
        public object EmployeeFiler(string? searchText, int pageSize, int pageNumber)
        {
            var data = _repository.Filter(searchText, pageSize, pageNumber);
            return data;
        }

        /// <summary>
        /// Thực hiện validate Employee
        /// </summary>
        /// <param name="entity">Object Employee</param>
        /// <param name="mode">Mode (thêm hoặc sửa)</param>
        /// <returns>
        /// true - không có lỗi
        /// false - có lỗi
        /// </returns>
        /// CreateBy: Công Đoàn (08/08/2022)

        protected override bool ValidateCustom(Employee entity, ActivityMode mode)
        {
            var listDepartmentId = _departmentRepository.GetAllId();
            bool isExistDepartmentId = false;
            foreach (var item in listDepartmentId)
            {
                if (entity.DepartmentId == item)
                {
                    isExistDepartmentId = true;
                }
            }


            // Validate mã phòng ban không tồn tại
            if (isExistDepartmentId != true)
            {
                isValidCustom = false;
                listMsgErrors.Add(ResourcesVI.Error_ValidateNotExistDepartmentId);
            }

            // Validate mã nhân viên không được vượt quá 20
            if (entity.EmployeeCode.Length > 20)
            {
                isValidCustom = false;
                listMsgErrors.Add(ResourcesVI.Error_ValidateLengthCode20);
            }

            // Validate ngày lớn hơn ngày hiện tại
            if(entity.DateOfBirth > DateTime.Now)
            {
                isValidCustom = false;
                listMsgErrors.Add(ResourcesVI.Error_ValidateInputDateGreaterNow);
            }

            // Validate mã phòng ban trống
            if (string.IsNullOrEmpty(entity.DepartmentId.ToString()))
            {
                isValidCustom = false;
                listMsgErrors.Add(ResourcesVI.Error_ValidateDepartmentIdEmpty);
            }

            // Kiểm tra email đúng định dạng
            if(entity.Email != null && entity.Email != "")
            {
                if (ValidateEmail(entity.Email) == false)
                {
                    isValidCustom = false;
                    listMsgErrors.Add(ResourcesVI.Error_ValidateEmail);
                }
            }

            // Kiểm tra trùng mã
            if (_repository.CheckEmployeeCode(entity.EmployeeCode,entity.EmployeeId,mode))
            {
                isValidCustom = false;
                listMsgErrors.Add(ResourcesVI.Error_ValidateExistEmployeeCode);
            }
            return isValidCustom;
        }


        /// <summary>
        /// Thực hiện validate Employee khi xóa
        /// </summary>
        /// <param name="id">Id của Employee</param>
        /// <param name="mode">Mode Delete</param>
        /// <returns>
        /// true - Tồn tại bản ghi trong bảng
        /// false - Không tồn tại bản ghi trong bảng
        /// </returns>
        /// CreateBy: Công Đoàn (08/08/2022)
        protected override bool ValidateCustom(Guid id, ActivityMode mode)
        {
            if (_repository.CheckExistEmployeeId(id) == false)
            {
                isValidCustom = false;
                listMsgErrors.Add(ResourcesVI.Error_ValidateNotExistEmployeeId);
            }
            return isValidCustom;
        }

        /// <summary>
        /// Hàm thực hiện validate Email
        /// </summary>
        /// <param name="email">Email của Employee</param>
        /// <returns>
        /// true - Email đúng định dạng
        /// false - Email sai định dạng
        /// </returns>
        /// CreateBy: Công Đoàn (08/08/2022)

        public bool ValidateEmail(string email)
        {
            Regex regex = new Regex(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$");
            Match match = regex.Match(email);
            if (match.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Thực hiện import dữ liệu từ excel
        /// </summary>
        /// <param name="fileImport">File excel</param>
        /// <returns>
        /// Số bản ghi thay đổi trong database
        /// </returns>
        /// CreatedBy: Công Đoàn (10/08/2022)
        public int ImportEmployeesService(IFormFile formFile)
        {
            // Kiểm tra tệp có đúng định dạng không (.xls, .xlsx)

            // Nếu tệp dữ liệu hơp thì đọc dữ liệu từ tệp
            if(formFile == null || formFile.Length <= 0)
            {
                listMsgErrors.Add(ResourcesVI.Error_ValidateFileNotExist);
            }


            if (!Path.GetExtension(formFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                listMsgErrors.Add(ResourcesVI.Error_ValidateMalformedDataFile);
            }

            var employees = new List<Employee>();


            using (var stream = new MemoryStream())
            {
                formFile.CopyToAsync(stream);

                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;

                    // Duyệt vòng for từng hàng 1 và gán dữ liệu vào object
                    for (int row = 4; row <= rowCount; row++)
                    {
                        var employeeCode = worksheet.Cells[row, 2].Value;
                        var fullName = worksheet.Cells[row, 3].Value;
                        var gender = worksheet.Cells[row, 4].Value;
                        var dateOfBirth = worksheet.Cells[row, 5].Value;
                        var positionName = worksheet.Cells[row, 6].Value;
                        var departmentName = worksheet.Cells[row, 7].Value;
                        var bankAccount = worksheet.Cells[row, 8].Value;
                        var bankName = worksheet.Cells[row, 9].Value;

                        // Format lại định dạng và data rồi gắn vào object 
                        var emp = new Employee()
                        {
                            EmployeeCode = HandleFormat.FormatObjectToString(employeeCode),
                            FullName = HandleFormat.FormatObjectToString(fullName),
                            Gender = HandleFormat.FormatGerderFromString(gender.ToString()),
                            DateOfBirth = HandleFormat.FormatObjectToDateTime(dateOfBirth),
                            PositionName = HandleFormat.FormatObjectToString(positionName),
                            DepartmentName = HandleFormat.FormatObjectToString(departmentName),
                            BankAccount = HandleFormat.FormatObjectToString(bankName),
                            BankName = HandleFormat.FormatObjectToString(bankName),
                            DepartmentId = _departmentRepository.GetIdFromName(HandleFormat.FormatObjectToString(departmentName))
                        };

                        // Kiểm tra lỗi 
                        base.listMsgErrors.Clear();

                        base.Validate(emp);
                        ValidateCustom(emp,mode: ActivityMode.PostMode);
                        emp.ErrorValidatesImport.AddRange(base.listMsgErrors);

                        employees.Add(emp);
                    }

                    // Lọc những data không lỗi và thực hiện thêm vào database
                    var employeeValids = employees.Where(e => { return e.ErrorValidatesImport.Count() == 0; }).ToList();
                    var res = _repository.ImportEmployee(employeeValids);
                    return res;
                }
            }

        }


        /// <summary>
        /// Thực hiện xuất database thành excel
        /// </summary>
        /// <returns>
        /// File excel chứa data
        /// </returns>
        /// CreatedBy: Công Đoàn (10/08/2022)
        public ExcelExport Export()
        {
            List<Employee> employees = _repository.GetAll().ToList();
            List<EmployeeExport> employeesExport = new List<EmployeeExport>();
            foreach (var employee in employees.Select((value, stt) => new { stt, value }))
            {
                EmployeeExport employeeExport = new EmployeeExport();
                employeeExport.EmployeeCode = employee.value.EmployeeCode;
                employeeExport.FullName = employee.value.FullName;
                employeeExport.PositionName = employee.value.PositionName;
                employeeExport.DepartmentName = employee.value.DepartmentName;
                employeeExport.DateOfBirth = employee.value.DateOfBirth;
                employeeExport.BankAccount = employee.value.BankAccount;
                employeeExport.BankName = employee.value.BankName;
                employeeExport.GenderName = employee.value.GenderName;
                employeesExport.Add(employeeExport);
            }
            var data = new ExcelExport();
            var stream = new MemoryStream();
            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet1");

                workSheet.Cells["B3"].LoadFromCollection(employeesExport, true);

                workSheet.DefaultRowHeight = 20;
                
                workSheet.Row(3).Height = 20;
                workSheet.Row(3).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Row(3).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                workSheet.Row(3).Style.Font.Bold = true;
                Color colFromHex = System.Drawing.ColorTranslator.FromHtml("#D8D8D8");
                workSheet.Cells["A3:I3"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells["A3:I3"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                workSheet.Column(1).Width = 10;
                workSheet.Column(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Column(5).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells["C1:F1"].Merge = true;
                workSheet.Cells["C1"].Value = "DANH SÁCH NHÂN VIÊN";
                workSheet.Cells["C1"].Style.Font.Size = 32;
                workSheet.Cells["C1"].Style.Font.Bold = true;
                workSheet.Cells["A3"].Value = "STT";
                for (int i = 2;i<= 27; i++)
                {
                    workSheet.Column(i).AutoFit(20);
                    workSheet.Column(i).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                }

                int index = 3;
                foreach(var item in employees)
                {
                    index++;
                    workSheet.Cells[$"A{index}"].Value = index - 3;
                }

                string DateCellFormat = "dd/mm/yyyy";
                workSheet.Column(5).Style.Numberformat.Format = DateCellFormat;
                workSheet.Column(8).Style.Numberformat.Format = DateCellFormat;
                workSheet.Column(21).Style.Numberformat.Format = DateCellFormat;
                workSheet.Column(23).Style.Numberformat.Format = DateCellFormat;

                var totalCol = typeof(EmployeeExport).GetProperties().Length;
                //int row = 3;
                for(int row = 3; row < employeesExport.Count + 4; row++)
                {
                    for (var indexCol = 1; indexCol <= totalCol+1; indexCol++)
                    {
                        workSheet.Cells[row, indexCol].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        workSheet.Cells[row, indexCol].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        workSheet.Cells[row, indexCol].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        workSheet.Cells[row, indexCol].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    }
                }
                package.Save();
            }
            stream.Position = 0;
            string excelName = $"UserList-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";
            data.FileContents = stream.ToArray();
            data.FileName = excelName;
            data.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //return File(stream, "application/octet-stream", excelName);  
            return data;
        }


        /// <summary>
        /// Thực hiện xóa nhiều nhân viên
        /// </summary>
        /// <param name="employeesId">List id của nhân viên</param>
        /// <returns>Số bản ghi thay đổi</returns>
        /// CreatedBy: Công Đoàn (08/08/2022)
        public int DeleteMultiEmployee(List<Guid> employeesId)
        {
            if(employeesId == null)
            {
                listMsgErrors.Add(ResourcesVI.Error_ValidateEmployeeCodeEmpty);
                throw new MISAException(ResourcesVI.Error_ValidateData, listMsgErrors);
            }

            var res = _repository.DeleteMultiEmployee(employeesId);
            return res;
        }
    }
}
