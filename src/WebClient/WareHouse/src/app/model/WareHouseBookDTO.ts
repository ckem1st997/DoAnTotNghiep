import { BaseModel } from "./BaseModel";


export interface WareHouseBookDTO extends BaseModel {
    voucherCode: string;
    voucherDate: Date;
    wareHouseId?: any;
    deliver: string;
    receiver: string;
    vendorId?: any;
    reason: string|null;
    reasonDescription?: any;
    description?: any;
    reference?: any;
    createdDate: Date;
    createdBy: string;
    modifiedDate: Date;
    modifiedBy?: any;
    type: string;
    wareHouseName:string |null;
}