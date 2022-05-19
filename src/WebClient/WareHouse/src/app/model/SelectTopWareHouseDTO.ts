import { BaseModel } from "./BaseModel";

export interface SelectTopWareHouseDTO extends BaseModel {
    wareHouseId: string;
    name: string;
    sumBalance: string;
}