import { BaseModel } from "./BaseModel";


export interface DashBoardSelectTopInAndOutDTO extends BaseModel {
    count: number|null;
    name: string|null;
    code: string|null;
    unitName: string|null;
    sumQuantity: number|null;
    sumPrice: number|null;
}