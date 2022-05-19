import { AuditDetailSerial } from './AuditDetailSerial';
import { Inward } from './Inward';
import { BaseEntity } from './BaseEntity';
import { WareHouseItem } from './WareHouseItem';
import { Audit } from './Audit';
export interface AuditDetail extends BaseEntity {
    auditId: string;
    itemId: string;
    quantity: number;
    auditQuantity: number;
    conclude: string;
    audit: Audit;
    item: WareHouseItem;
    auditDetailSerials: AuditDetailSerial[];
}