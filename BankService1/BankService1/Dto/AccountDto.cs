using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace BankService1.Dto
{
    [DataContract]
    public class AccountDto
    {
        [DataMember]
        [Key]
        [Required(ErrorMessage = "Vui lòng nhập số tài khoản")]
        public string AccountNumber { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Vui lòng nhâp tên")]
        public string FirstName { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Vui lòng nhâp họ.")]
        public string LastName { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Vui lòng nhâp tên tài khoản")]
        public string UserName { get; set; }
        [DataMember]
        [Required]
        [StringLength(100, ErrorMessage = "Mật khẩu phải lớn hơn sáu kí tự", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [DataMember]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Mật khẩu không khớp")]
        public string PasswordConfirm { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Vui lòng nhâp số điện thoại")]
        public string Phone { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Vui lòng nhâp địa chỉ")]
        public string Address { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Vui lòng nhâp email")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Vui lòng số CMND/CCCD")]
        public string IdentityNumber { get; set; }
        [DataMember]
        public double Balance { get; set; }
        [DataMember]
        [Required]
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }
        [DataMember]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [DataMember]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        [DataMember]
        [DefaultValue(1)]
        public int Status { get; set; }
    }
}