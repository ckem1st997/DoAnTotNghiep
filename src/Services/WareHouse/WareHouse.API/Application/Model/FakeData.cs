using Aspose.Cells;
using GrpcGetDataToMaster;
using Microsoft.AspNetCore.Hosting;
using Share.Base.Core.AutoDependencyInjection.InjectionAttribute;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

#nullable disable

namespace WareHouse.API.Application.Model
{
    public interface IFakeData
    {
        public Task<IEnumerable<BaseSelectDTO>> GetDepartment();
        
        public Task<IEnumerable<BaseSelectDTO>> GetEmployee();
        public Task<IEnumerable<BaseSelectDTO>> GetStation();
        public Task<IEnumerable<BaseSelectDTO>> GetProject();
        
        public Task<IEnumerable<BaseSelectDTO>> GetCustomer();
        public Task<IEnumerable<BaseSelectDTO>> GetCreateBy();
        public List<BaseSelectDTO> GetListAccountIdentifier(IWebHostEnvironment _hostingEnvironment);
    }

    [ScopedDependency]    
    
    public class FakeData: IFakeData
    {
        private readonly GrpcGetData.GrpcGetDataClient _client;
        public FakeData(GrpcGetData.GrpcGetDataClient client)
        {
            _client = client;
        }


        public async Task<IEnumerable<BaseSelectDTO>> GetDepartment()
        {

            var data = new List<BaseSelectDTO>();
            var list = await _client.GetDepartmentAsync(new Params());
            foreach (var item in list.ListGetDepartment_)
            {
                data.Add(new BaseSelectDTO()
                {
                    Id = item.Id,
                    Name =item.Name
                });
            }
            return data;
        }

        public async Task<IEnumerable<BaseSelectDTO>> GetEmployee()
        {
            var data = new List<BaseSelectDTO>();
            var list = await _client.GetEmployeeAsync(new Params());
            foreach (var item in list.ListGetEmployee_)
            {
                data.Add(new BaseSelectDTO()
                {
                    Id = item.Id,
                    Name = item.Name
                });
            }
            return data;
        }

        public async Task<IEnumerable<BaseSelectDTO>> GetStation()
        {
            var data = new List<BaseSelectDTO>();
            var list = await _client.GetStationAsync(new Params());
            foreach (var item in list.ListGetStation_)
            {
                data.Add(new BaseSelectDTO()
                {
                    Id = item.Id,
                    Name = item.Name
                });
            }
            return data;
        }

        public async Task<IEnumerable<BaseSelectDTO>> GetProject()
        {
            var data = new List<BaseSelectDTO>();
            var list = await _client.GetProjectAsync(new Params());
            foreach (var item in list.ListGetProject_)
            {
                data.Add(new BaseSelectDTO()
                {
                    Id = item.Id,
                    Name = item.Name
                });
            }
            return data;
        }

        public async Task<IEnumerable<BaseSelectDTO>> GetCustomer()
        {
            var data = new List<BaseSelectDTO>();
            var list = await _client.GetCustomerAsync(new Params());
            foreach (var item in list.ListGetCustomer_)
            {
                data.Add(new BaseSelectDTO()
                {
                    Id = item.Id,
                    Name = item.Name
                });
            }
            return data;
        }

        public async Task<IEnumerable<BaseSelectDTO>> GetCreateBy()
        {
            var data = new List<BaseSelectDTO>();
            var list = await _client.GetCreateByAsync(new Params());
            foreach (var item in list.ListCreateBy_)
            {
                data.Add(new BaseSelectDTO()
                {
                    Id = item.Id,
                    Name = item.Name
                });
            }
            return data;
        }

        public List<BaseSelectDTO> GetListAccountIdentifier(IWebHostEnvironment _hostingEnvironment)
        {
            return new List<BaseSelectDTO>();
            // var tmpPath = Path.Combine(_hostingEnvironment.WebRootPath, "Excel", "He_thong_tai_khoan kế toán.xlsx");
            // Workbook wb = new Workbook(tmpPath);
            // //Get the first worksheet.
            // Worksheet worksheet = wb.Worksheets[0];
            // //Get the cells collection.
            // Cells cells = worksheet.Cells;

            // //Define the list.
            // var list = new List<BaseSelectDTO>(); //Get the AA column index. (Since "Status" is always @ AA column.
            // int col = CellsHelper.ColumnNameToIndex("A");
            // //  int col2 = CellsHelper.ColumnNameToIndex("B");

            // //Get the last row index in AA column.
            // int last_row = worksheet.Cells.GetLastDataRow(col);

            // //Loop through the "Status" column while start collecting values from row 9
            // //to save each value to List
            // for (int i = 2; i < 259; i++)
            // {
            //     //    myList.Add(cells[i, col].Value.ToString(), cells[i, col + 1].Value.ToString());
            //     var code = cells[i, col].Value.ToString() == null ? "" : cells[i, col].Value.ToString();
            //     var name = cells[i, col + 1].Value.ToString() == null ? "" : cells[i, col + 1].Value.ToString();
            //     var tem = new BaseSelectDTO();
            //     tem.Name = $"[{code.Trim()}] {name.Trim()}";
            //     tem.Id = code.Trim();
            //     list.Add(tem);
            // }

            // return list;
        }
    }


}
