using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Share.Base.Service.Controller
{
    [AllowAnonymous]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public abstract class BasePublicControllerWareHouse : ControllerBase
    {
        //or stogare => create PARTITION

        // crate PARTITION


        //        USE[WarehouseManagement]
        //GO
        //BEGIN TRANSACTION
        //CREATE PARTITION FUNCTION[VoucherDateInward] (datetime2(0)) AS RANGE LEFT FOR VALUES()


        //CREATE PARTITION SCHEME[VoucherDateInwardS] AS PARTITION[VoucherDateInward] TO([PRIMARY])


        //ALTER TABLE[dbo].[InwardDetail]
        //        DROP CONSTRAINT[FK_InwardDetails_PK_Inward]






        //ALTER TABLE[dbo].[Inward] DROP CONSTRAINT[PK__Inward__3214EC07BF1204AC] WITH(ONLINE = OFF)


        //SET ANSI_PADDING ON

        //ALTER TABLE[dbo].[Inward] ADD PRIMARY KEY NONCLUSTERED
        //(
        //    [Id] ASC
        //)WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON[PRIMARY]


        //CREATE CLUSTERED INDEX[ClusteredIndex_on_VoucherDateInwardS_638070540877190714] ON[dbo].[Inward]
        //        (

        //    [VoucherDate]
        //) WITH(SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON[VoucherDateInwardS] ([VoucherDate])


        //DROP INDEX[ClusteredIndex_on_VoucherDateInwardS_638070540877190714] ON[dbo].[Inward]




        //        ALTER TABLE[dbo].[InwardDetail] WITH CHECK ADD CONSTRAINT[FK_InwardDetails_PK_Inward] FOREIGN KEY([InwardId])
        //REFERENCES[dbo].[Inward]
        //        ([Id])
        //ON DELETE CASCADE
        //ALTER TABLE[dbo].[InwardDetail]
        //        CHECK CONSTRAINT[FK_InwardDetails_PK_Inward]








        //     COMMIT TRANSACTION
        //


    }
}
