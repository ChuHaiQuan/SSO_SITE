using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace QsTech.Accounts.Models.Profiles
{
    public class ChangePasswordModel : IValidatableObject
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "原密码不能为空")]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "密码只能输入字母或数字")]
        public string OriginalPassword { get; set; }

        [Required(ErrorMessage = "新密码不能为空")]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "密码只能输入字母或数字")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "确认新密码不能为空")]
        [Compare("NewPassword", ErrorMessage = "两次输入的密码不匹配")]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "密码只能输入字母或数字")]
        public string ConfirmationPassword { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.Compare(OriginalPassword, NewPassword, false) == 0)
            {
                yield return new ValidationResult("新密码不能与原密码相同", new string[] { "NewPassword" });
            }
        }


        public string Redirect_uri { get; set; }
    }
}