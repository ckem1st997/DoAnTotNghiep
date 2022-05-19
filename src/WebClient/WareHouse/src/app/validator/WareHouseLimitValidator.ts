import { Validator } from "fluentvalidation-ts";
import { WareHouseLimit } from "../entity/WareHouseLimit";

export class WareHouseLimitValidator extends Validator<WareHouseLimit> {
    constructor() {
        super();

        this.ruleFor('minQuantity').greaterThanOrEqualTo(0).withMessage('Tồn tối thiểu phải lớn hơn hoặc bằng 0 !');
        this.ruleFor('maxQuantity').greaterThanOrEqualTo(1).withMessage('Tồn tối thiểu phải lớn hơn hoặc bằng 1 !');
        this.ruleFor('itemId')
            .notEmpty()
            .withMessage('Xin vui lòng chọn vật tư của bạn !')
            .notNull()
            .withMessage('Xin vui lòng chọn vật tư của bạn !');
        this.ruleFor('unitId')
            .notEmpty()
            .withMessage('Xin vui lòng chọn đơn vị tính của bạn !')
            .notNull()
            .withMessage('Xin vui lòng chọn đơn vị tính của bạn !');
        this.ruleFor('wareHouseId')
            .notEmpty()
            .withMessage('Xin vui lòng chọn kho của bạn !')
            .notNull()
            .withMessage('Xin vui lòng chọn kho của bạn !');
        this.ruleFor('id')
            .notEmpty()
            .withMessage('Xin vui lòng nhập id của bạn !')
            .notNull()
            .withMessage('Xin vui lòng nhập id của bạn !');
    }
}
