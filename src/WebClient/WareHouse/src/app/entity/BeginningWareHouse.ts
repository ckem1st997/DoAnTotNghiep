import { WareHouseItem } from './WareHouseItem';
import { Inward } from './Inward';
import { BaseEntity } from './BaseEntity';
import { Unit } from './Unit';
import { WareHouse } from './WareHouse';
export interface BeginningWareHouse extends BaseEntity {
    wareHouseId: string;
    itemId: string;
    unitId: string;
    unitName: string;
    quantity: number;
    createdDate: string;
    createdBy: string;
    modifiedDate: string;
    modifiedBy: string;
    item: WareHouseItem;
    unit: Unit;
    wareHouse: WareHouse;
}