using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WareHouse.API.Application.Commands.Models;
using WareHouse.API.Application.Model;
using WareHouse.Domain.Entity;

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
               // .ForMember(x => x.DomainEvents, opt => opt.Ignore())
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
             //   .ForMember(x => x.DomainEvents, opt => opt.Ignore())
                .ForMember(x => x.Audits, opt => opt.Ignore())
                .ForMember(x => x.Inwards, opt => opt.Ignore())
                .ForMember(x => x.OnDelete, opt => opt.Ignore())
                .ForMember(x => x.WareHouseLimits, opt => opt.Ignore());
            CreateMap<Domain.Entity.WareHouse, WareHouseDTO>()
                   .ForMember(x => x.WareHouseDTOs, opt => opt.Ignore());


            CreateMap<Vendor, VendorDTO>();
            CreateMap<VendorDTO, Vendor>();
            CreateMap<VendorCommands, Domain.Entity.Vendor>()
                .ForMember(x => x.WareHouseItems, opt => opt.Ignore())
                .ForMember(x => x.Inwards, opt => opt.Ignore())
                .ForMember(x => x.OnDelete, opt => opt.Ignore());
               // .ForMember(x => x.DomainEvents, opt => opt.Ignore());

            CreateMap<Unit, UnitDTO>();
            CreateMap<UnitDTO, Unit>();
            CreateMap<UnitCommands, Domain.Entity.Unit>()
                .ForMember(x => x.WareHouseItems, opt => opt.Ignore())
                .ForMember(x => x.WareHouseLimits, opt => opt.Ignore())
                .ForMember(x => x.BeginningWareHouses, opt => opt.Ignore())
                .ForMember(x => x.InwardDetails, opt => opt.Ignore())
                .ForMember(x => x.OutwardDetails, opt => opt.Ignore())
                .ForMember(x => x.WareHouseItemUnits, opt => opt.Ignore())
                .ForMember(x => x.WareHouseLimits, opt => opt.Ignore())
                .ForMember(x => x.OnDelete, opt => opt.Ignore());
                //.ForMember(x => x.DomainEvents, opt => opt.Ignore());
            //   CreateMap<Domain.Entity.WareHouse, WareHouseCommands>();


            CreateMap<WareHouseItemCategory, WareHouseItemCategoryDTO>();
            CreateMap<WareHouseItemCategoryDTO, WareHouseItemCategory>();
            CreateMap<WareHouseItemCategoryCommands, Domain.Entity.WareHouseItemCategory>()
                .ForMember(x => x.OnDelete, opt => opt.Ignore())
                .ForMember(x => x.WareHouseItems, opt => opt.Ignore())
                .ForMember(x => x.InverseParent, opt => opt.Ignore());
            CreateMap<Domain.Entity.WareHouseItemCategory, WareHouseItemCategoryCommands>();

            //
            CreateMap<Domain.Entity.WareHouseItem, WareHouseItemCommands>()
                .ForMember(x => x.wareHouseItemUnits, opt => opt.Ignore());

            CreateMap<WareHouseItemCommands, Domain.Entity.WareHouseItem>()
                .ForMember(x => x.OnDelete, opt => opt.Ignore());

            CreateMap<Domain.Entity.WareHouseItem, WareHouseItemDTO>();


            ///

            CreateMap<Domain.Entity.WareHouseItemUnit, WareHouseItemUnitCommands>();

            CreateMap<WareHouseItemUnitCommands, Domain.Entity.WareHouseItemUnit>().ForMember(x => x.Unit, opt => opt.Ignore()).ForMember(x => x.Item, opt => opt.Ignore())
                .ForMember(x => x.OnDelete, opt => opt.Ignore());

            CreateMap<Domain.Entity.WareHouseItemUnit, WareHouseItemUnitDTO>();


            ///

            CreateMap<Domain.Entity.BeginningWareHouse, BeginningWareHouseCommands>();

            CreateMap<BeginningWareHouseCommands, Domain.Entity.BeginningWareHouse>()
                .ForMember(x => x.Unit, opt => opt.Ignore())
                .ForMember(x => x.Item, opt => opt.Ignore())
                .ForMember(x => x.WareHouse, opt => opt.Ignore())
               // .ForMember(x => x.DomainEvents, opt => opt.Ignore())
                .ForMember(x => x.OnDelete, opt => opt.Ignore());

            CreateMap<Domain.Entity.BeginningWareHouse, BeginningWareHouseDTO>();


            //
            CreateMap<Domain.Entity.WareHouseItemCategory, WareHouseItemCategoryCommands>();

            CreateMap<WareHouseItemCategoryCommands, Domain.Entity.WareHouseItemCategory>()
                .ForMember(x => x.WareHouseItems, opt => opt.Ignore())
             //   .ForMember(x => x.DomainEvents, opt => opt.Ignore())
                .ForMember(x => x.OnDelete, opt => opt.Ignore());

            CreateMap<Domain.Entity.WareHouseItemCategory, WareHouseItemCategoryDTO>();

            //
            CreateMap<Domain.Entity.WareHouseLimit, WareHouseLimitCommands>();

            CreateMap<WareHouseLimitCommands, Domain.Entity.WareHouseLimit>()
                .ForMember(x => x.WareHouse, opt => opt.Ignore())
                .ForMember(x => x.Item, opt => opt.Ignore())
                .ForMember(x => x.Unit, opt => opt.Ignore())
              //  .ForMember(x => x.DomainEvents, opt => opt.Ignore())
                .ForMember(x => x.OnDelete, opt => opt.Ignore());

            CreateMap<Domain.Entity.WareHouseLimit, WareHouseLimitDTO>();
            //
            CreateMap<Domain.Entity.InwardDetail, InwardDetailCommands>();

            CreateMap<InwardDetailCommands, Domain.Entity.InwardDetail>()
                .ForMember(x => x.Item, opt => opt.Ignore())
                // .ForMember(x => x.SerialWareHouses, opt => opt.Ignore())
                .ForMember(x => x.Unit, opt => opt.Ignore())
               // .ForMember(x => x.DomainEvents, opt => opt.Ignore())
                .ForMember(x => x.OnDelete, opt => opt.Ignore());

            CreateMap<Domain.Entity.InwardDetail, InwardDetailDTO>();

            //
            CreateMap<Domain.Entity.Inward, InwardCommands>();

            CreateMap<InwardCommands, Domain.Entity.Inward>()
                // .ForMember(x => x.InwardDetails, opt => opt.Ignore())
                //.ForMember(x => x.DomainEvents, opt => opt.Ignore())
                .ForMember(x => x.OnDelete, opt => opt.Ignore());

            CreateMap<Domain.Entity.Inward, InwardDTO>();

            //
            CreateMap<Domain.Entity.SerialWareHouse, SerialWareHouseCommands>();

            CreateMap<SerialWareHouseCommands, Domain.Entity.SerialWareHouse>()
                 .ForMember(x => x.InwardDetail, opt => opt.Ignore())
                  .ForMember(x => x.OutwardDetail, opt => opt.Ignore())
                   .ForMember(x => x.Item, opt => opt.Ignore())
               // .ForMember(x => x.DomainEvents, opt => opt.Ignore())
                .ForMember(x => x.OnDelete, opt => opt.Ignore());

            CreateMap<Domain.Entity.SerialWareHouse, SerialWareHouseDTO>();
            //


            //
            CreateMap<Domain.Entity.OutwardDetail, OutwardDetailCommands>();

            CreateMap<OutwardDetailCommands, Domain.Entity.OutwardDetail>().ForMember(x => x.Item, opt => opt.Ignore()).ForMember(x => x.Unit, opt => opt.Ignore())
                .ForMember(x => x.OnDelete, opt => opt.Ignore());

            CreateMap<Domain.Entity.OutwardDetail, OutwardDetailDTO>();

            //
            CreateMap<Domain.Entity.Outward, OutwardCommands>();

            CreateMap<OutwardCommands, Domain.Entity.Outward>()
                .ForMember(x => x.OnDelete, opt => opt.Ignore());

            CreateMap<Domain.Entity.Outward, OutwardDTO>();

            //map to event bus
            //CreateMap<InwardIntegrationEvent, InwardCommands>();
            //CreateMap<InwardDetailIntegrationEvent, InwardDetailCommands>();
            //CreateMap<SerialWareHouseIntegrationEvent, SerialWareHouseCommands>();
        }
    }
}