
import { BaseCountChartByMouthOrYearDTO } from './BaseCountChartByMouthOrYearDTO';
export interface DashBoardChartInAndOutCountDTO {
    inward: BaseCountChartByMouthOrYearDTO[]|null;
    outward: BaseCountChartByMouthOrYearDTO[]|null;
}