import { BaseModel } from "./BaseModel";
import { UnitDTO } from "./UnitDTO";
import { WareHouseItemDTO } from "./WareHouseItemDTO";

export interface BeginningWareHouseDTO extends BaseModel {
    wareHouseId: string|null;
    itemId: string|null;
    unitId: string|null;
    unitName: string|null;
    quantity: number;
    createdDate: string;
    createdBy: string;
    modifiedDate: string;
    modifiedBy: string;
    item: WareHouseItemDTO|null;
    unit: UnitDTO|null;
    wareHouse: WareHouseItemDTO|null;
    wareHouseItemDTO: WareHouseItemDTO[]|null;
    unitDTO: UnitDTO[]|null;
    wareHouseDTO: WareHouseItemDTO[]|null;
}