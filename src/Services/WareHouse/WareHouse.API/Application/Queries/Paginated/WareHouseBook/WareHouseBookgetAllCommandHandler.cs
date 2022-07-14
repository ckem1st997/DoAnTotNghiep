using Dapper;
using MediatR;
using System;
using System.Data;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WareHouse.API.Application.Interface;
using WareHouse.API.Application.Model;
using WareHouse.Domain.IRepositories;

namespace WareHouse.API.Application.Queries.Paginated.WareHouseBook
{
    public class WareHouseBookgetAllCommand : IRequest<IPaginatedList<WareHouseBookDTO>>
    {

    }

    public class WareHouseBookgetAllCommandHandler : IRequestHandler<WareHouseBookgetAllCommand,
        IPaginatedList<WareHouseBookDTO>>
    {
        private readonly IDapper _repository;
        private readonly IPaginatedList<WareHouseBookDTO> _list;


        public WareHouseBookgetAllCommandHandler(IDapper repository, IPaginatedList<WareHouseBookDTO> list)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _list = list ?? throw new ArgumentNullException(nameof(list));
        }

        public async Task<IPaginatedList<WareHouseBookDTO>> Handle(WareHouseBookgetAllCommand request,
            CancellationToken cancellationToken)
        {
            if (request == null)
                return _list;


            // BuildMyString.com generated code. Please enjoy your string responsibly.

            StringBuilder sb = new StringBuilder();

            sb.Append("select  d1.*,WareHouse.Name as WareHouseName ");
            sb.Append("from  (select Inward.Id,WareHouseId, VoucherCode,VoucherDate,CreatedBy,ModifiedBy,Deliver,Receiver,Reason,N'Phiếu nhập' as Type,Inward.OnDelete ");
            sb.Append("from Inward inner join InwardDetail on Inward.Id=InwardDetail.InwardId   where  Inward.OnDelete=0  ");
            sb.Append("union all   ");
            sb.Append("select Outward.Id,WareHouseId, VoucherCode,VoucherDate,CreatedBy,ModifiedBy,Deliver,Receiver,Reason,N'Phiếu xuất' as Type,Outward.OnDelete ");
            sb.Append("from Outward inner join OutwardDetail on Outward.Id=OutwardDetail.OutwardId   where  Outward.OnDelete=0 ) d1  ");
            sb.Append("inner join WareHouse on d1.WareHouseID=WareHouse.Id  ");
            sb.Append("group by d1.Id,d1.WareHouseID,d1.CreatedBy,d1.Deliver,d1.Reason,d1.Type,d1.VoucherCode,d1.VoucherDate,d1.ModifiedBy,d1.Receiver,d1.OnDelete,WareHouse.Name  ");
            sb.Append("order by d1.VoucherDate desc   ");
            _list.Result = await _repository.GetList<WareHouseBookDTO>(sb.ToString(), new DynamicParameters(), CommandType.Text);
            _list.totalCount = _list.Result.AsList().Count;
            return _list;
        }
    }
}