using System;
using System.ComponentModel.DataAnnotations;
using QsTech.Framework.Validation;
using QsTech.Framework.Localization;

namespace QsTech.Accounts.Models.Accounts
{
    public class CreateAccountModel
    {

        public CreateAccountModel()
        {
            
        }

        public CreateAccountModel(Account acount)
        {
            Name = acount.Name;
            Email = acount.Email;
            Gender = acount.Gender;
            Mobile = acount.Mobile;
            Id = acount.Id;
        }
        //[LRequired(ErrorMessage = "姓名不能为空")]
        public string Name { get; set; }

        [LRequired(ErrorMessage = "登录账号不能为空")]
        public string LoginName { get; set; }

        [LRequired(ErrorMessage = "密码不能为空")]
        [LRegularExpression(RegularPattern.Password, ErrorMessage = "密码只能输入字母或数字")]
        public string Password { get; set; }

        [LRequired(ErrorMessage = "确认密码不能为空")]
        [LCompare("Password", ErrorMessage = "两次输入的密码不匹配")]
        [LRegularExpression(RegularPattern.Password, ErrorMessage = "密码只能输入字母或数字")]
        public string ConfirmPassword { get; set; }

        [Range(0, 119, ErrorMessage="年龄范围为0 - 119")]
        public int? Age { get; set; }

        public string Gender { get; set; }

        public string Telephone { get; set; }

        //[LRequired(ErrorMessage = "移动电话不能为空")]
        [LRegularExpression(RegularPattern.Mobile, ErrorMessage = "移动电话的格式不正确")]
        public string Mobile { get; set; }

        public DateTime? Birthday { get; set; }

        //[LRequired(ErrorMessage = "电子邮件不能为空")]
        [LRegularExpression(RegularPattern.Email, ErrorMessage = "电子邮件的格式不正确")]
        [LDataType(DataType.EmailAddress, ErrorMessage = "电子邮件的格式不正确")]
        public string Email { get; set; }

        public string JobNumber { get; set; }
   
        public string Post { get; set; }

        public int? Id { get; set; }

        public int[] Clients { get; set; }
    }

    public class InitPasswordModel
    {
        public InitPasswordModel()
        {

        }

        public InitPasswordModel(Account account)
        {
            this.Name = account.Name;
            LoginName = account.AccountName;
            Id = account.Id;
        }
        public string Name { get; set; }

        public string LoginName { get; set; }

        [LRequired(ErrorMessage = "密码不能为空")]
        [LRegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "密码只能输入字母或数字")]
        public string Password { get; set; }

        [LRequired(ErrorMessage = "确认密码不能为空")]
        [LCompare("Password", ErrorMessage = "两次输入的密码不匹配")]
        [LRegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "密码只能输入字母或数字")]
        public string ConfirmPassword { get; set; }

        public int Id { get; set; }
    }


    public class ClientAccountCreateModel
    {
        public ClientAccountCreateModel()
        {

        }

        public string Name { get; set; }

        public string Password { get; set; }

        public int? Age { get; set; }

        public string Gender { get; set; }

        public string Telephone { get; set; }

        public string Mobile { get; set; }

        public string Email { get; set; }

        public DateTime? Birthday { get; set; }

        public string AccountName { get; set; }

        public string NickName { get; set; }

        public string JobNumber { get; set; }

        public string ApplicationId { get; set; }

        public Account  ConvertToEntity()
        {
            var account = new Account();
            account.Name = Name;
            account.AccountName = AccountName;
            account.Gender = Gender;
            account.Age = Age;
            account.Telephone = Telephone;
            account.Mobile = Mobile;
            account.Birthday = Birthday;
            account.Email = Email;
            account.NickName = NickName;
            account.JobNumber = JobNumber;
            account.Password = Password;
            return account;

        }

    }

}