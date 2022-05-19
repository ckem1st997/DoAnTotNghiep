import { Validator } from 'fluentvalidation-ts';
import { BeginningWareHouse } from '../entity/BeginningWareHouse';

export class BeginningWareHouseValidator extends Validator<BeginningWareHouse> {
    constructor() {
        super();

        this.ruleFor('quantity').greaterThanOrEqualTo(0).withMessage('Tỉ lệ phải lớn hơn hoặc bằng 0 !');
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
