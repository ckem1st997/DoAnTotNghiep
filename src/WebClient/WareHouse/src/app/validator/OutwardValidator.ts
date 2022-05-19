import { Validator } from "fluentvalidation-ts";
import { Outward } from "../entity/Outward";

export class OutwardValidator extends Validator<Outward> {
    constructor() {
        super();

        this.ruleFor('voucher')
            .notEmpty().withMessage('Xin vui lòng nhập số chứng từ  !')
            .notNull().withMessage('Xin vui lòng nhập số chứng từ  !');
        this.ruleFor('voucherCode')
            .notEmpty().withMessage('Xin vui lòng nhập số chứng từ thực tế  !')
            .notNull().withMessage('Xin vui lòng nhập số chứng từ thực tế  !');
        this.ruleFor('voucherDate')
            .notEmpty().withMessage('Xin vui lòng chọn ngày tạo  !')
            .notNull().withMessage('Xin vui lòng chọn ngày tạo  !');
        this.ruleFor('wareHouseId')
            .notEmpty().withMessage('Xin vui lòng chọn kho  !')
            .notNull().withMessage('Xin vui lòng chọn kho  !');
        this.ruleFor('deliver')
            .notEmpty().withMessage('Xin vui lòng nhập người giao  !')
            .notNull().withMessage('Xin vui lòng nhập người giao  !');
        this.ruleFor('receiver')
            .notEmpty().withMessage('Xin vui lòng nhập người nhận  !')
            .notNull().withMessage('Xin vui lòng nhập người nhận  !');
        this.ruleFor('deliver')
            .notEmpty().withMessage('Xin vui lòng nhập số chứng từ  !')
            .notNull().withMessage('Xin vui lòng nhập số chứng từ  !');
        this.ruleFor('createdBy')
            .notEmpty().withMessage('Xin vui lòng chọn người tạo  !')
            .notNull().withMessage('Xin vui lòng chọn người tạo  !');
        this.ruleFor('toWareHouseId').notEqual('wareHouseId').withMessage('Xin vui lòng chọn người tạo  !');
        this.ruleFor('id')
            .notEmpty()
            .notNull()
            .withMessage('Xin vui lòng nhập id  !');
    }
}
