using System.ComponentModel.DataAnnotations;

namespace Master.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Xin vui lòng nhập Email !"), MaxLength(50), DataType(DataType.EmailAddress), RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Xin vui lòng nhập mật khẩu !"), DataType(DataType.Password), MaxLength(20, ErrorMessage = "Mật khẩu phải ít hơn 20 kí tự"), MinLength(5, ErrorMessage = "Mật khẩu phải nhiều hơn 4 kí tự")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Xin vui lòng nhập mật khẩu !"), DataType(DataType.Password), Compare(nameof(Password), ErrorMessage = "Hai mật khẩu chưa khớp nhau !"), MaxLength(20, ErrorMessage = "Mật khẩu phải ít hơn 20 kí tự"), MinLength(5, ErrorMessage = "Mật khẩu phải nhiều hơn 4 kí tự")]
        public string ConfirmPassword { get; set; }
    }
    public class LoginViewModel
    {

        [Required(ErrorMessage = "Xin vui lòng nhập Email !"), MaxLength(50), Display(Name = "Email"), DataType(DataType.EmailAddress), RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Xin vui lòng nhập mật khẩu !"), DataType(DataType.Password), MaxLength(20, ErrorMessage = "Mật khẩu phải ít hơn 20 kí tự"), MinLength(5, ErrorMessage = "Mật khẩu phải nhiều hơn 4 kí tự")]
        public string Password { get; set; }

        [Display(Name = "Nhớ mật khẩu")]
        public bool Remember { get; set; }
    }
    public class RegisterModel
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }


    public class LoginModel
    {

        public string Username { get; set; }

        public string Password { get; set; }

        public bool Remember { get; set; }
    }
}
