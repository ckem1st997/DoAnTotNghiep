import { Validator } from 'fluentvalidation-ts';
import { WareHouse } from '../entity/WareHouse';


export class WareHouseValidator extends Validator<WareHouse> {
    constructor() {
        super();

        this.ruleFor('name')
            .notEmpty().withMessage('Xin vui lòng nhập tên của bạn !')
            .notNull()
            .withMessage('Xin vui lòng nhập tên của bạn !')
            .minLength(5).withMessage('Độ dài tên min là 5')
            .maxLength(50).withMessage('Độ dài tên max là 50');
        this.ruleFor('id')
            .notEmpty()
            .notNull()
            .withMessage('Xin vui lòng nhập id của bạn !');
        this.ruleFor('code')
            .notEmpty().withMessage('Xin vui lòng nhập mã của bạn !')
            .notNull()
            .withMessage('Xin vui lòng nhập mã của bạn !')
            .minLength(5).withMessage('Độ dài mã min là 3')
            .maxLength(50).withMessage('Độ dài mã max là 50');
    }
}
