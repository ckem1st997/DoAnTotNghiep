import { BaseEntity } from "./BaseEntity";
import { WareHouseItem } from "./WareHouseItem";

export interface WareHouseItemCategory extends BaseEntity {
    code: string;
    name: string;
    parentId: string;
    path: string;
    description: string;
    inactive: boolean | null;
    parent: WareHouseItemCategory;
    inverseParent: WareHouseItemCategory[];
    wareHouseItems: WareHouseItem[];
}