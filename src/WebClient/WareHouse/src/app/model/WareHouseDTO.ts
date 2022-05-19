import { BaseModel } from "./BaseModel";

export interface WareHouseDTO extends BaseModel {
    code: string;
    name: string;
    address: string;
    description?: any;
    parentId: string;
    path?: any;
    inactive: boolean;
    id: string;
    wareHouseDTOs:WareHouseDTO[];
}