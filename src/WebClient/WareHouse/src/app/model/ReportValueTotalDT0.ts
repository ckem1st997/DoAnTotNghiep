import { BaseModel } from "./BaseModel";


export interface ReportValueTotalDT0 extends BaseModel {
    balance: number,
    export: number,
    import: number,
    beginning: number,
    unitName: string,
    wareHouseItemName: string,
    wareHouseItemCode: string,
}