using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WareHouse.API.Application.Queries.BaseModel;
using WareHouse.Domain.IRepositories;

namespace Report.API.Application.Queries.GetAll.Reports
{
    public class ReportGetTreeViewCommand : IRequest<IEnumerable<ReportTreeView>>
    {

    }

    public class
        ReportGetTreeViewCommandHandler : IRequestHandler<ReportGetTreeViewCommand, IEnumerable<ReportTreeView>>
    {
        private readonly IDapper _repository;
        public ReportGetTreeViewCommandHandler(IDapper repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<IEnumerable<ReportTreeView>> Handle(ReportGetTreeViewCommand request,
            CancellationToken cancellationToken)
        {
            if (request == null)
                return null;
            var convertToRoot = new List<ReportTreeView>();
            var tem = new ReportTreeView
            {
                children = new List<ReportTreeView>(),
                folder = false,
                key = "wh/report-total",
                Code = "",
                Name = "Báo cáo tổng hợp"
            };
            convertToRoot.Add(tem);
            var tem1 = new ReportTreeView
            {
                children = new List<ReportTreeView>(),
                folder = false,
                key = "wh/report-details",
                Code = "",
                Name = "Báo cáo chi tiết"
            };
            convertToRoot.Add(tem1);
            return await Task.FromResult(convertToRoot);
        }



    }
}