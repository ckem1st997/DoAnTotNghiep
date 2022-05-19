import { BaseModel } from "./BaseModel";
import { BaseSelectDTO } from "./BaseSelectDTO";
import { InwardDTO } from "./InwardDTO";
import { SerialWareHouseDTO } from "./SerialWareHouseDTO";
import { UnitDTO } from "./UnitDTO";
import { WareHouseItemDTO } from "./WareHouseItemDTO";

export interface OutwardDetailDTO extends BaseModel {
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
    accountMore: string;
    accountYes: string;
    status: string;
    serialWareHouses: SerialWareHouseDTO[];
    outward: InwardDTO|null;
    item: WareHouseItemDTO|null;
    unit: UnitDTO|null;
    unitDTO: UnitDTO[];
    wareHouseItemDTO: WareHouseItemDTO[];
    getDepartmentDTO: BaseSelectDTO[];
    getEmployeeDTO: BaseSelectDTO[];
    getStationDTO: BaseSelectDTO[];
    getProjectDTO: BaseSelectDTO[];
    getCustomerDTO: BaseSelectDTO[];
    getAccountDTO: BaseSelectDTO[];
    //BaseSelectDTO
   // serialWareHouses: SerialWareHouse[];
}