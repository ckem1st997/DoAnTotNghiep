import { BaseModel } from "./BaseModel";

export interface VendorDTO extends BaseModel {
    code: string;
    name: string;
    address: string;
    phone: string;
    email: string;
    contactPerson: string;
    inactive: boolean;
}