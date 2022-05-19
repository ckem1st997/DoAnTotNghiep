import { Validator } from 'fluentvalidation-ts';
import { WareHouse } from '../entity/WareHouse';
import { WareHouseItem } from '../entity/WareHouseItem';
import { WareHouseItemUnit } from '../entity/WareHouseItemUnit';
import { WareHouseItemDTO } from '../model/WareHouseItemDTO';


export class WareHouseItemUnitValidator extends Validator<WareHouseItemUnit> {
    constructor() {
        super();

        this.ruleFor('id')
            .notEmpty()
            .notNull()
            .withMessage('Xin vui lòng nhập id của bạn !');
        this.ruleFor('unitId')
            .notEmpty()
            .notNull()
            .withMessage('Xin vui lòng chọn đơn vị tính của bạn !');
        this.ruleFor('convertRate').greaterThanOrEqualTo(0).withMessage('Tỉ lệ phải lớn hơn hoặc bằng 0 !');
            
    }
}
