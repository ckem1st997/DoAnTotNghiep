import { BaseEntity } from "./BaseEntity";
import { Inward } from "./Inward";
import { InwardDetail } from "./InwardDetail";
import { OutwardDetail } from "./OutwardDetail";
import { WareHouseItem } from "./WareHouseItem";

export interface SerialWareHouse extends BaseEntity {
    itemId: string;
    serial: string;
    inwardDetailId: string;
    outwardDetailId: string;
    isOver: boolean;
    inwardDetail: InwardDetail;
    item: WareHouseItem;
    outwardDetail: OutwardDetail;
}