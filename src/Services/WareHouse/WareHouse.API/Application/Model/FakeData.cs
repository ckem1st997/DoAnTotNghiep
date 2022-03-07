using System;
using System.Collections.Generic;

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
    }
}
