import { BaseModel } from "./BaseModel";
import { UnitDTO } from "./UnitDTO";
import { VendorDTO } from "./VendorDTO";
import { WareHouseItemCategoryDTO } from "./WareHouseItemCategoryDTO";
import { WareHouseItemUnitDTO } from "./WareHouseItemUnitDTO";

export interface WareHouseItemDTO extends BaseModel {
    code: string;
    name: string;
    categoryId: string;
    description: string;
    vendorId: string;
    vendorName: string;
    country: string;
    unitId: string;
    inactive: boolean | null;
    categoryDTO: WareHouseItemCategoryDTO[];
    unitDTO: UnitDTO[];
    vendorDTO: VendorDTO[];
    wareHouseItemUnits:WareHouseItemUnitDTO[];
}