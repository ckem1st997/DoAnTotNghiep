import { BaseModel } from "./BaseModel";
import { InwardDetailDTO } from "./InwardDetailDTO";
import { WareHouseItemDTO } from "./WareHouseItemDTO";

export interface SerialWareHouseDTO extends BaseModel {
    itemId: string;
    serial: string;
    inwardDetailId: string;
    outwardDetailId: string;
    isOver: boolean;
    inwardDetail: InwardDetailDTO;
    item: WareHouseItemDTO;
    outwardDetail: InwardDetailDTO;
}