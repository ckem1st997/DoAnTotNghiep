import { BaseModel } from "./BaseModel";
import { SelectListItem } from "./SelectListItem";

export interface ListApp extends BaseModel {
    name: string;
    inActive: boolean;
    description: string;
    parentId: boolean | null;
    softShow: number | null;
    isAPI: boolean | null;
}


export interface ListAuthozire extends BaseModel {
    name: string;
    inActive: boolean;
    description: string;
    parentId: boolean | null;
    softShow: number | null;
}


export interface ListAuthozireRoleByUser extends BaseModel {
    listAuthozireId: string;
    userId: string;
}


export interface ListRole extends BaseModel {
    appId: string;
    name: string;
    inActive: boolean;
    description: string;
    key: string;
    parentId: boolean | null;
    softShow: number | null;
    isAPI: boolean | null;
}


export interface ListRoleByUser extends BaseModel {
    listRoleId: string;
    userId: string;
}