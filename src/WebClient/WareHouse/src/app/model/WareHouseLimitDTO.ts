import { BaseModel } from "./BaseModel";
import { WareHouseItemDTO } from 'src/app/model/WareHouseItemDTO';
import { UnitDTO } from 'src/app/model/UnitDTO';
import { WareHouseDTO } from 'src/app/model/WareHouseDTO';

export interface WareHouseLimitDTO extends BaseModel {
    wareHouseId: string|null;
    itemId: string|null;
    unitId: string|null;
    unitName: string|null;
    minQuantity: number;
    maxQuantity: number;
    createdDate: string|null;
    createdBy: string|null;
    modifiedDate: string|null;
    modifiedBy: string|null;
    item: WareHouseItemDTO|null;
    unit: UnitDTO|null;
    wareHouse: WareHouseDTO|null;
    wareHouseItemDTO: WareHouseItemDTO[]|null;
    unitDTO: UnitDTO[]|null;
    wareHouseDTO: WareHouseItemDTO[]|null;
}