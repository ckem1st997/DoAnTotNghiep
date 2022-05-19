import { BaseSearchModel } from "./BaseSearchModel";

export interface ReportTotalSearchModel extends BaseSearchModel {
    wareHouseId:string|null;
    itemId:string|null;
    start:string|null;
    end:string|null;
}