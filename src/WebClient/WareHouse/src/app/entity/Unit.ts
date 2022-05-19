import { BaseEntity } from "./BaseEntity";
import { BeginningWareHouse } from "./BeginningWareHouse";
import { Inward } from "./Inward";
import { InwardDetail } from "./InwardDetail";
import { OutwardDetail } from "./OutwardDetail";
import { WareHouseItem } from "./WareHouseItem";
import { WareHouseItemUnit } from "./WareHouseItemUnit";
import { WareHouseLimit } from "./WareHouseLimit";
export interface Unit extends BaseEntity {
    unitName: string;
    inactive: boolean;
    beginningWareHouses: BeginningWareHouse[];
    inwardDetails: InwardDetail[];
    outwardDetails: OutwardDetail[];
    wareHouseItemUnits: WareHouseItemUnit[];
    wareHouseItems: WareHouseItem[];
    wareHouseLimits: WareHouseLimit[];
}


