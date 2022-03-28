using Aspose.Cells;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;

#nullable disable

namespace WareHouse.API.Application.Model
{
    
    public static class FakeData
    {
        public static IEnumerable<BaseSelectDTO> GetDepartment()
        {
            var data = new List<BaseSelectDTO>();
            for (int i = 0; i < 10; i++)
            {
                data.Add(new BaseSelectDTO()
                {
                    Id = i.ToString(),
                    Name = $"Department {i}"
                });
            }
            return data;
        }

        public static IEnumerable<BaseSelectDTO> GetEmployee()
        {
            var data = new List<BaseSelectDTO>();
            for (int i = 0; i < 10; i++)
            {
                data.Add(new BaseSelectDTO()
                {
                    Id = i.ToString(),
                    Name = $"Employee {i}"
                });
            }
            return data;
        }

        public static IEnumerable<BaseSelectDTO> GetStation()
        {
            var data = new List<BaseSelectDTO>();
            for (int i = 0; i < 10; i++)
            {
                data.Add(new BaseSelectDTO()
                {
                    Id = i.ToString(),
                    Name = $"Station {i}"
                });
            }
            return data;
        }

        public static IEnumerable<BaseSelectDTO> GetProject()
        {
            var data = new List<BaseSelectDTO>();
            for (int i = 0; i < 10; i++)
            {
                data.Add(new BaseSelectDTO()
                {
                    Id = i.ToString(),
                    Name = $"Project {i}"
                });
            }
            return data;
        }

        public static IEnumerable<BaseSelectDTO> GetCustomer()
        {
            var data = new List<BaseSelectDTO>();
            for (int i = 0; i < 10; i++)
            {
                data.Add(new BaseSelectDTO()
                {
                    Id = i.ToString(),
                    Name = $"Customer {i}"
                });
            }
            return data;
        }

         public static IEnumerable<BaseSelectDTO> GetCreateBy()
        {
            var data = new List<BaseSelectDTO>();
            for (int i = 0; i < 10; i++)
            {
                data.Add(new BaseSelectDTO()
                {
                    Id = i.ToString(),
                    Name = $"CreateBy {i}"
                });
            }
            return data;
        }

        public static List<BaseSelectDTO> GetListAccountIdentifier(IWebHostEnvironment _hostingEnvironment)
        {
            var tmpPath = Path.Combine(_hostingEnvironment.WebRootPath, "Excel", "He_thong_tai_khoan kế toán.xlsx");
            Workbook wb = new Workbook(tmpPath);
            //Get the first worksheet.
            Worksheet worksheet = wb.Worksheets[0];
            //Get the cells collection.
            Cells cells = worksheet.Cells;

            //Define the list.
            var list = new List<BaseSelectDTO>(); //Get the AA column index. (Since "Status" is always @ AA column.
            int col = CellsHelper.ColumnNameToIndex("A");
            //  int col2 = CellsHelper.ColumnNameToIndex("B");

            //Get the last row index in AA column.
            int last_row = worksheet.Cells.GetLastDataRow(col);

            //Loop through the "Status" column while start collecting values from row 9
            //to save each value to List
            for (int i = 2; i < 259; i++)
            {
                //    myList.Add(cells[i, col].Value.ToString(), cells[i, col + 1].Value.ToString());
                var code = cells[i, col].Value.ToString() == null ? "" : cells[i, col].Value.ToString();
                var name = cells[i, col + 1].Value.ToString() == null ? "" : cells[i, col + 1].Value.ToString();
                var tem = new BaseSelectDTO();
                tem.Name = $"[{code.Trim()}] {name.Trim()}";
                tem.Id = code.Trim();
                list.Add(tem);
            }

            return list;
        }
    }
}
