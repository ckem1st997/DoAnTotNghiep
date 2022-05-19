import { BaseModel } from "./BaseModel";
import { UnitDTO } from "./UnitDTO";
import { WareHouseItemDTO } from "./WareHouseItemDTO";

export interface WareHouseItemUnitDTO extends BaseModel {
    itemId: string;
    unitId: string;
    unitName: string;
    convertRate: number;
    isPrimary: boolean | null;
    item: WareHouseItemDTO| null;
    unit: UnitDTO| null;
}