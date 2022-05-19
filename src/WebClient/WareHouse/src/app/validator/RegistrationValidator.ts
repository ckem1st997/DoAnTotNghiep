import { Validator } from "fluentvalidation-ts";
import { LoginDTO } from "../model/LoginDTO";
import { RegistrationDTO } from "../model/RegistrationDTO";

export class RegistrationValidator extends Validator<RegistrationDTO> {
    constructor() {
        super();

        this.ruleFor('username').emailAddress().withMessage('Tên tài khoản chưa đúng định dạng email !');
        this.ruleFor('password').notNull().withMessage('Chưa nhập password').minLength(6).withMessage('Password phải có ít nhất 6 ký tự !').maxLength(20).withMessage('Password không được vượt quá 20 ký tự !');
        this.ruleFor('repassword').notEqual('password').withMessage('Hai mật khẩu chưa giống nhau !').minLength(6).withMessage('Password phải có ít nhất 6 ký tự !').maxLength(20).withMessage('Password không được vượt quá 20 ký tự !');

    }
}
