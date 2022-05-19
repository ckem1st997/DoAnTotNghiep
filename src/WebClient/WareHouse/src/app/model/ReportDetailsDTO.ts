import { BaseModel } from "./BaseModel";


export interface ReportDetailsDTO extends BaseModel {

    voucherDate: string;
    code: string;
    name: string;
    voucherCode: string;
    unitName: string;
    wareHouseId: string;
    reason: string;
    beginning: number;
    import: number;
    export: number;
    balance: number;
    employeeName: string;
    departmentName: string;
    projectName: string;
    description: string;

}