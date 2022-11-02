using MISA.Common.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.Common.Handle
{
    public static class HandleFormat
    {
        public static DateTime FormatToDateTime(string date)
        {
            DateTime dt = DateTime.ParseExact("19/02/2017", "dd/MM/yyyy", CultureInfo.InvariantCulture);
            return dt;
        }

        /// <summary>
        /// Hàm thực hiện loại bỏ chữ ra khỏi list string
        /// </summary>
        /// <param name="listText">list text</param>
        /// <returns>
        /// list string gồm số
        /// </returns>
        /// CreatedBy: Công Đoàn (10/08/2022)
        public static List<string> GetListNumberFromString(List<string> listText)
        {
            List<string> listNumber = new List<string>();
            foreach (var item in listText)
            {
                string temp;
                temp = Regex.Replace(item, "[^0-9]", ""); 
                if (string.IsNullOrEmpty(temp) == false)
                {
                    listNumber.Add(temp);
                }
            }
            return listNumber;
        }

        /// <summary>
        /// Hàm thực hiện lấy ra số lớn nhất từ chuỗi
        /// </summary>
        /// <param name="listNumber">list number</param>
        /// <returns>
        /// số lớn nhất
        /// </returns>
        /// CreatedBy: Công Đoàn (10/08/2022)
        public static double GetMaxNumberFromList(List<string> listNumber)
        {
            var max = Double.Parse(listNumber[0]);
            foreach (var item in listNumber)
            {
                if (max < double.Parse(item))
                {
                    max = double.Parse(item);
                }
            }
            return max;
        }

        /// <summary>
        /// Hàm thực hiện Format gender từ string
        /// </summary>
        /// <param name="text">Tên giới tính</param>
        /// <returns>Giới tính</returns>
        /// CreatedBy: Công Đoàn (10/08/2022)
        public static Gender? FormatGerderFromString(string? text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return null;
            };
            switch (text.ToLower())
            {
                case "nam":
                    return Gender.Male;
                case "nữ":
                    return Gender.Female;
                default:
                    return Gender.Other;
            }
        }

        /// <summary>
        /// Hàm thực hiện đổi object về chuỗi
        /// </summary>
        /// <param name="text">Object</param>
        /// <returns>Chuỗi được convert từ object</returns>
        /// CreatedBy: Công Đoàn (10/08/2022)
        public static string? FormatObjectToString(object? text)
        {
            return text == null ? String.Empty : text.ToString();
        }

        /// <summary>
        /// Hàm thực hiện conver về datatime từ object
        /// </summary>
        /// <param name="value">Object</param>
        /// <returns>DAtetime</returns>
        /// CreatedBy: Công Đoàn (10/08/2022)
        public static DateTime? FormatObjectToDateTime(object? value)
        {
            if(value == null)
            {
                return null;
            }
            else
            {
                DateTime date;
                if(DateTime.TryParse(value.ToString(),out date))
                {
                    return date;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
