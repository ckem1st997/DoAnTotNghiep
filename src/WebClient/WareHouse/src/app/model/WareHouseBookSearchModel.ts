import { BaseSearchModel } from "./BaseSearchModel";

export interface WareHouseBookSearchModel extends BaseSearchModel {
    typeWareHouseBook: string | null;
    fromDate: string | null;
    toDate: string | null;
    wareHouseId:string|null;
}