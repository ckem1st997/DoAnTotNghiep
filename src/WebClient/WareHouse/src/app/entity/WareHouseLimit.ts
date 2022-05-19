import { BaseEntity } from "./BaseEntity";
import { Unit } from "./Unit";
import { WareHouse } from "./WareHouse";
import { WareHouseItem } from "./WareHouseItem";

export interface WareHouseLimit extends BaseEntity {
    wareHouseId: string;
    itemId: string;
    unitId: string;
    unitName: string;
    minQuantity: number;
    maxQuantity: number;
    createdDate: string;
    createdBy: string;
    modifiedDate: string;
    modifiedBy: string;
    item: WareHouseItem;
    unit: Unit;
    wareHouse: WareHouse;
}