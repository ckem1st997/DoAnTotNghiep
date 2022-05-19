
import { BaseEntity } from "./BaseEntity";
import { InwardDetail } from "./InwardDetail";
import { Vendor } from "./Vendor";
import { WareHouse } from "./WareHouse";
export interface Inward extends BaseEntity {
    voucher:string;
    voucherCode: string;
    voucherDate: string;
    wareHouseId: string;
    deliver: string;
    receiver: string;
    vendorId: string;
    reason: string;
    reasonDescription: string;
    description: string;
    reference: string;
    createdDate: string;
    createdBy: string;
    modifiedDate: string;
    modifiedBy: string;
    deliverPhone: string;
    deliverAddress: string;
    deliverDepartment: string;
    receiverPhone: string;
    receiverAddress: string;
    receiverDepartment: string;
    vendor: Vendor;
    wareHouse: WareHouse;
    inwardDetails: InwardDetail[];
}