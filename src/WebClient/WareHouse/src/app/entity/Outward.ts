import { BaseEntity } from "./BaseEntity";
import { Inward } from "./Inward";
import { OutwardDetail } from "./OutwardDetail";
import { WareHouse } from "./WareHouse";
import { WareHouseItem } from "./WareHouseItem";

export interface Outward extends BaseEntity {
    voucher:string;
    voucherCode: string;
    voucherDate: string;
    wareHouseId: string;
    toWareHouseId: string;
    deliver: string;
    receiver: string;
    reason: number;
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
    toWareHouse: WareHouse;
    wareHouse: WareHouse;
    outwardDetails: OutwardDetail[];
}