import { DashBoardSelectTopInAndOutDTO } from "./DashBoardSelectTopInAndOutDTO";
import { SelectTopWareHouseDTO } from "./SelectTopWareHouseDTO";

export interface SelectTopDashBoardDTO {
    itemCountMax: DashBoardSelectTopInAndOutDTO|undefined;
    itemCountMin: DashBoardSelectTopInAndOutDTO|undefined;
    wareHouseBeginningCountMax: SelectTopWareHouseDTO|undefined;
    wareHouseBeginningCountMin: SelectTopWareHouseDTO|undefined;
}