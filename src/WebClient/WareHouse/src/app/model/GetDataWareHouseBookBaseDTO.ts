import { BaseSelectDTO } from "./BaseSelectDTO";
import { UnitDTO } from "./UnitDTO";
import { WareHouseItemDTO } from "./WareHouseItemDTO";


export interface GetDataWareHouseBookBaseDTO {
    wareHouseItemDTO: WareHouseItemDTO[];
    unitDTO: UnitDTO[];
    getDepartmentDTO: BaseSelectDTO[];
    getEmployeeDTO: BaseSelectDTO[];
    getStationDTO: BaseSelectDTO[];
    getProjectDTO: BaseSelectDTO[];
    getCustomerDTO: BaseSelectDTO[];
    getAccountDTO: BaseSelectDTO[];
}