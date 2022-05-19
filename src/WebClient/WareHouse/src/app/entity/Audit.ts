import { BaseEntity } from "./BaseEntity";
import { WareHouse } from "./WareHouse";
import { AuditCouncil } from "./AuditCouncil";
import { AuditDetail } from "./AuditDetail";
export interface Audit extends BaseEntity {
    voucherCode: string;
    voucherDate: string;
    wareHouseId: string;
    description: string;
    createdDate: string;
    createdBy: string;
    modifiedDate: string;
    modifiedBy: string;
    wareHouse: WareHouse;
    auditCouncils: AuditCouncil[];
    auditDetails: AuditDetail[];
}