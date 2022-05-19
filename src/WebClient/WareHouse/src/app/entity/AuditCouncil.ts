import { Inward } from './Inward';
import { BaseEntity } from "./BaseEntity";
import { WareHouseItem } from "./WareHouseItem";
import { Audit } from './Audit';
export interface AuditCouncil extends BaseEntity {
    auditId: string;
    employeeId: string;
    employeeName: string;
    role: string;
    audit: Audit;
}