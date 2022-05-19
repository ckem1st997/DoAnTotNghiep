import { AuditDetail } from './AuditDetail';
import { WareHouseItem } from './WareHouseItem';
import { BaseEntity } from './BaseEntity';
import { Inward } from './Inward';
export interface AuditDetailSerial extends BaseEntity {
    itemId: string;
    serial: string;
    auditDetailId: string;
    auditDetail: AuditDetail;
    item: WareHouseItem;
}