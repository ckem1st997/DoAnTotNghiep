import { Outward } from './Outward';
import { BaseEntity } from "./BaseEntity";
import { Inward } from "./Inward";
import { SerialWareHouse } from "./SerialWareHouse";
import { Unit } from "./Unit";
import { WareHouseItem } from "./WareHouseItem";

export interface OutwardDetail extends BaseEntity {
    outwardId: string;
    itemId: string;
    unitId: string;
    uiquantity: number;
    uiprice: number;
    amount: number;
    quantity: number;
    price: number;
    departmentId: string;
    departmentName: string;
    employeeId: string;
    employeeName: string;
    stationId: string;
    stationName: string;
    projectId: string;
    projectName: string;
    customerId: string;
    customerName: string;
    item: WareHouseItem;
    outward: Outward;
    unit: Unit;
    serialWareHouses: SerialWareHouse[];
}