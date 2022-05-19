import { Audit } from "./Audit";
import { BaseEntity } from "./BaseEntity";
import { BeginningWareHouse } from "./BeginningWareHouse";
import { Inward } from "./Inward";
import { Outward } from "./Outward";
import { WareHouseLimit } from "./WareHouseLimit";

export interface WareHouse extends BaseEntity {
    code: string;
    name: string;
    address: string;
    description: string;
    parentId: string;
    path: string;
    inactive: boolean;
    audits: Audit[];
    beginningWareHouses: BeginningWareHouse[];
    inwards: Inward[];
    outwardToWareHouses: Outward[];
    outwardWareHouses: Outward[];
    wareHouseLimits: WareHouseLimit[];
}