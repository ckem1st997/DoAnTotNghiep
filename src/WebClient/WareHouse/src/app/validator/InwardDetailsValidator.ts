import { Validator } from "fluentvalidation-ts";
import { InwardDetail } from "../entity/InwardDetail";

export class InwardDetailsValidator extends Validator<InwardDetail> {
    constructor() {
        super();

        this.ruleFor('inwardId')
            .notEmpty().withMessage('Xin vui lòng chọn phiếu của bạn !')
            .notNull()
            .withMessage('Xin vui lòng chọn phiếu của bạn !');
        this.ruleFor('id')
            .notEmpty()
            .notNull()
            .withMessage('Xin vui lòng nhập id của bạn !');
        this.ruleFor('itemId')
            .notEmpty().withMessage('Xin vui lòng vật tư !')
            .notNull().withMessage('Xin vui lòng vật tư !');
        this.ruleFor('unitId')
            .notEmpty().withMessage('Xin vui lòng chọn đơn vị tính !')
            .notNull().withMessage('Xin vui lòng chọn đơn vị tính !');
    }
}
