import { AuditDetail } from "./AuditDetail";
import { AuditDetailSerial } from "./AuditDetailSerial";
import { BaseEntity } from "./BaseEntity";
import { BeginningWareHouse } from "./BeginningWareHouse";
import { InwardDetail } from "./InwardDetail";
import { OutwardDetail } from "./OutwardDetail";
import { SerialWareHouse } from "./SerialWareHouse";
import { Unit } from "./Unit";
import { Vendor } from "./Vendor";
import { WareHouseItemCategory } from "./WareHouseItemCategory";
import { WareHouseItemUnit } from "./WareHouseItemUnit";
import { WareHouseLimit } from "./WareHouseLimit";

export interface WareHouseItem extends BaseEntity {
    code: string;
    name: string;
    categoryId: string;
    description: string;
    vendorId: string;
    vendorName: string;
    country: string;
    unitId: string;
    inactive: boolean | null;
    category: WareHouseItemCategory;
    unit: Unit;
    vendor: Vendor;
    auditDetailSerials: AuditDetailSerial[];
    auditDetails: AuditDetail[];
    beginningWareHouses: BeginningWareHouse[];
    inwardDetails: InwardDetail[];
    outwardDetails: OutwardDetail[];
    serialWareHouses: SerialWareHouse[];
    wareHouseItemUnits: WareHouseItemUnit[];
    wareHouseLimits: WareHouseLimit[];
}