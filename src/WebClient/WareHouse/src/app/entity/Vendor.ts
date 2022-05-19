import { BaseEntity } from "./BaseEntity";
import { Inward } from "./Inward";
import { WareHouseItem } from "./WareHouseItem";

export interface Vendor extends BaseEntity {
    code: string;
    name: string;
    address: string;
    phone: string;
    email: string;
    contactPerson: string;
    inactive: boolean;
    inwards: Inward[];
    wareHouseItems: WareHouseItem[];
}