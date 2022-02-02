using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WareHouse.API.Application.Commands.Models;
using WareHouse.API.Application.Model;

namespace WareHouse.API.Application.AutoMapper.WareHouses
{
    public class WareHouseCommandProfile : Profile
    {
        public WareHouseCommandProfile()
        {
            CreateMap<WareHouseCommands, Domain.Entity.WareHouse>()
                .ForMember(x => x.OutwardWareHouses, opt => opt.Ignore())
                .ForMember(x => x.OutwardToWareHouses, opt => opt.Ignore())
                .ForMember(x => x.BeginningWareHouses, opt => opt.Ignore())
                .ForMember(x => x.DomainEvents, opt => opt.Ignore())
                .ForMember(x => x.Audits, opt => opt.Ignore())
                .ForMember(x => x.Inwards, opt => opt.Ignore())
                     .ForMember(x => x.OnDelete, opt => opt.Ignore())

                .ForMember(x => x.WareHouseLimits, opt => opt.Ignore());
            //   CreateMap<Domain.Entity.WareHouse, WareHouseCommands>();
            //
            CreateMap<WareHouseDTO, Domain.Entity.WareHouse>()
                .ForMember(x => x.OutwardWareHouses, opt => opt.Ignore())
                .ForMember(x => x.OutwardToWareHouses, opt => opt.Ignore())
                .ForMember(x => x.BeginningWareHouses, opt => opt.Ignore())
                .ForMember(x => x.DomainEvents, opt => opt.Ignore())
                .ForMember(x => x.Audits, opt => opt.Ignore())
                .ForMember(x => x.Inwards, opt => opt.Ignore())
                .ForMember(x => x.OnDelete, opt => opt.Ignore())

                .ForMember(x => x.WareHouseLimits, opt => opt.Ignore());
            CreateMap<Domain.Entity.WareHouse,WareHouseDTO>();

            CreateMap<VendorCommands, Domain.Entity.Vendor>()
                .ForMember(x => x.WareHouseItems, opt => opt.Ignore())
                .ForMember(x => x.Inwards, opt => opt.Ignore())
                     .ForMember(x => x.OnDelete, opt => opt.Ignore())

                .ForMember(x => x.DomainEvents, opt => opt.Ignore());
            CreateMap<UnitCommands, Domain.Entity.Unit>()
     .ForMember(x => x.WareHouseItems, opt => opt.Ignore())
     .ForMember(x => x.WareHouseLimits, opt => opt.Ignore())
     .ForMember(x => x.BeginningWareHouses, opt => opt.Ignore())
     .ForMember(x => x.InwardDetails, opt => opt.Ignore())
     .ForMember(x => x.OutwardDetails, opt => opt.Ignore())
     .ForMember(x => x.WareHouseItemUnits, opt => opt.Ignore())
     .ForMember(x => x.WareHouseLimits, opt => opt.Ignore())
     .ForMember(x => x.OnDelete, opt => opt.Ignore())
     .ForMember(x => x.DomainEvents, opt => opt.Ignore());
            //   CreateMap<Domain.Entity.WareHouse, WareHouseCommands>();
        }
    }
}