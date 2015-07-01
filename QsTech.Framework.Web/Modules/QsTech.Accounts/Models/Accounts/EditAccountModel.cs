using System;
using System.ComponentModel.DataAnnotations;
using QsTech.Framework.Localization;
using QsTech.Framework.Validation;

namespace QsTech.Accounts.Models.Accounts
{
    public class EditAccountModel
    {
        public EditAccountModel() { }

        public EditAccountModel(Account user)
        {
            AccountName = user.AccountName;
            Id = user.Id;
            Name = user.Name;
            Age = user.Age;
            Gender = user.Gender;
            Birthday = user.Birthday;
            Telephone = user.Telephone;
            Mobile = user.Mobile;
            Email = user.Email;
            JobNumber=user.JobNumber;
            PhotoId = user.PhotoId;
            Remark = user.Remark;
        }

        public string AccountName { get; set; }

        public int Id { get; set; }
        
        [LRequired(ErrorMessage = "姓名不能为空")]
        public string Name { get; set; }

        [LRange(0, 119, ErrorMessage = "年龄范围为0 - 119")]
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

        public int? PhotoId { get; set; }

        public string Remark { get; set; }
    }

   

    public class EditAccountProfileModel
    {
        public EditAccountProfileModel() { }

        public EditAccountProfileModel(Account account)
        {
            Id = account.Id;
            Name = account.Name;
            Age = account.Age;
            Gender = account.Gender;
            Birthday = account.Birthday;
            Telephone = account.Telephone;
            Mobile = account.Mobile;
            Email = account.Email;
        }

        public int Id { get; set; }

        [LRequired(ErrorMessage = "姓名不能为空")]
        public string Name { get; set; }

        [LRange(0, 119, ErrorMessage = "年龄范围为0 - 119")]
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
    }
}
