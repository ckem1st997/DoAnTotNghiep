import { BaseModel } from "./BaseModel";

export interface WareHouseItemCategoryDTO extends BaseModel {
    code: string;
    name: string;
    parentId: string;
    path?: any;
    description?: any;
    inactive: boolean;
    parent?: any;
    inverseParent: WareHouseItemCategoryDTO[];
    wareHouseItems: any[];
}