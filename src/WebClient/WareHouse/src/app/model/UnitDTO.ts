import { BaseModel } from "./BaseModel";
export interface UnitDTO extends BaseModel {
    unitName: string;
    inactive: boolean;
    beginningWareHouses: any[];
    inwardDetails: any[];
    outwardDetails: any[];
    wareHouseItemUnits: any[];
    wareHouseItems: any[];
    wareHouseLimits: any[];
}