import { Validator } from "fluentvalidation-ts";
import { OutwardDetail } from './../entity/OutwardDetail';

export class OutwardDetailsValidator extends Validator<OutwardDetail> {
    constructor() {
        super();

        this.ruleFor('outwardId')
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
