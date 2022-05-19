import { BaseEntity } from './BaseEntity';
export interface WarehouseBalance extends BaseEntity {
    itemId: string;
    warehouseId: string;
    quantity: number;
    amount: number;
}