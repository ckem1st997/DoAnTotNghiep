import { BaseModel } from "./BaseModel";
import { SelectListItem } from "./SelectListItem";

export interface UserMaster extends BaseModel {
    userName: string;
    password: string;
    inActive: boolean | null;
    role: string;
    roleNumber: number;
    read: boolean;
    create: boolean;
    edit: boolean;
    delete: boolean;
    warehouseId: string;
    listWarehouseId: string;
    roleSelect: SelectListItem[];
}