import { BaseModel } from "./BaseModel";
import { BaseSelectDTO } from "./BaseSelectDTO";
import { OutwardDetailDTO } from "./OutwardDetailDTO";
import { VendorDTO } from "./VendorDTO";
import { WareHouseDTO } from "./WareHouseDTO";

export interface OutwardDTO extends BaseModel {
  voucher: string | null;
  voucherCode: string | null;
  voucherDate: string | null;
  wareHouseId?: string | null;
  toWareHouseId:string|null;
  deliver: string | null;
  receiver: string | null;
  vendorId: string | null;
  reason: string | null;
  reasonDescription: string | null;
  description: string | null;
  reference: string | null;
  createdDate: Date | null;
  createdBy: string | null;
  modifiedDate: string | null;
  modifiedBy: string | null;
  deliverPhone: string | null;
  deliverAddress: string | null;
  deliverDepartment: string | null;
  receiverPhone: string | null;
  receiverAddress: string | null;
  receiverDepartment: string | null;
  wareHouseDTO: WareHouseDTO[];
  getCreateBy: BaseSelectDTO[];
  outwardDetails: OutwardDetailDTO[];

}