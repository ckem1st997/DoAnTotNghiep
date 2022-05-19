import { BaseEntity } from "./BaseEntity";
import { Unit } from "./Unit";
import { WareHouseItem } from "./WareHouseItem";

export interface WareHouseItemUnit extends BaseEntity {
    itemId: string;
    unitId: string;
    unitName: string;
    convertRate: number;
    isPrimary: boolean | null;
    item: WareHouseItem;
    unit: Unit;
}