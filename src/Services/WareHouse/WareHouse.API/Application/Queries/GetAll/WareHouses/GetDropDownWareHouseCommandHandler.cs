using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using WareHouse.API.Application.Model;
using WareHouse.Domain.IRepositories;

namespace WareHouse.API.Application.Queries.GetAll.WareHouses
{
    public class
        GetDropDownWareHouseCommandHandler : IRequestHandler<GetDropDownWareHouseCommand, IEnumerable<WareHouseDTO>>
    {
        private readonly IDapper _repository;

        public GetDropDownWareHouseCommandHandler(IDapper repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<IEnumerable<WareHouseDTO>> Handle(GetDropDownWareHouseCommand request,
            CancellationToken cancellationToken)
        {
            if (request == null)
                return null;
            var models = await GetWareHousesAsync(request.Active);
            return GetWareHouseTreeModel(models);
        }
        private async Task<IList<WareHouseDTO>> GetWareHousesAsync(bool showHidden = false, bool showList = false)
        {
            string sql = "select Id,ParentId,Code,Name from WareHouse where Inactive =@active and OnDelete=0 ";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@active", showHidden ? 1 : 0);
            var getAll = await _repository.GetAllAync<WareHouseDTO>(sql, parameter, CommandType.Text);
            var result = getAll
                .Select(s => new WareHouseDTO
                {
                    Id = s.Id,
                    ParentId = s.ParentId,
                    Code = s.Code,
                    Name = s.Name
                })
                .OrderBy(o => o.Name)
                .ToList();
            return result;
        }

        private List<WareHouseDTO> GetWareHouseTreeModel(IEnumerable<WareHouseDTO> models)
        {
            var parents = models.Where(w => string.IsNullOrEmpty(w.ParentId))
                .OrderBy(o => o.Name);

            var result = new List<WareHouseDTO>();
            var level = 0;
            foreach (var parent in parents)
            {
                result.Add(new WareHouseDTO
                {
                    Id = parent.Id,
                    ParentId = parent.ParentId,
                    Name = "[" + parent.Code + "] " + parent.Name,
                    Code = parent.Code
                });
                GetChildWareHouseTreeModel(ref models, parent.Id, ref result, level);
            }

            return result;
        }

        private void GetChildWareHouseTreeModel(ref IEnumerable<WareHouseDTO> models, string parentId,
            ref List<WareHouseDTO> result, int level)
        {
            level++;
            var childs = models
                .Where(w => w.ParentId == parentId)
                .OrderBy(o => o.Name);

            if (childs.Any())
            {
                foreach (var child in childs)
                {
                    child.Name = "[" + child.Code + "] " + child.Name;
                    result.Add(new WareHouseDTO()
                    {
                        Id = child.Id,
                        ParentId = child.ParentId,
                        Name = GetTreeLevelString(level) + child.Name,
                        Code = child.Code
                    });
                    GetChildWareHouseTreeModel(ref models, child.Id, ref result, level);
                }
            }
        }

        public static string GetTreeLevelString(int level)
        {
            if (level <= 0)
                return "";

            var result = "";
            for (var i = 1; i <= level; i++)
            {
                result += "–";
            }

            return result;
        }
    }
}