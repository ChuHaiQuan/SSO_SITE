using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QsTech.Framework.Localization;
using QsTech.Framework.Validation;
using System.ComponentModel.DataAnnotations;

namespace QsTech.Authentication.Sso.Models
{
    public class RegisterModel
    {
        [LRequired(ErrorMessage = "CompanyNameIsRequired")]
        public string CompanyName { get; set; }

        public string CompanyShortName { get; set; }

        [LRequired(ErrorMessage = "CompanyAddressIsRequired")]
        public string CompanyAddress { get; set; }

        public string CompanyPhone { get; set; }

        public string CompanyFax { get; set; }

        [LRegularExpression(RegularPattern.Email, ErrorMessage = "EmailInputFormatTips")]
        [LDataType(DataType.EmailAddress, ErrorMessage = "EmailInputFormatTips")]
        public string CompanyEmail { get; set; }

        public string CompanyLinkman { get; set; }

        public string CompanyLinkmanPhone { get; set; }

        [LRequired(ErrorMessage = "NameIsRequired")]
        public string Name { get; set; }

        //[LRequired(ErrorMessage = "LoginNameIsRequired")]
        public string LoginName { get; set; }

        //[LRequired(ErrorMessage = "LoginPasswordIsRequired")]
        //[LRegularExpression(RegularPattern.Password, ErrorMessage = "PasswordInputTips")]
        public string LoginPassword { get; set; }

        //[LRequired(ErrorMessage = "ConfirmLoginPasswordIsRequired")]
        //[LCompare("LoginPassword", ErrorMessage = "PasswordsNotMatch")]
        //[LRegularExpression(RegularPattern.Password, ErrorMessage = "PasswordInputTips")]
        public string ConfirmLoginPassword { get; set; }

        public string Gender { get; set; }

        [Range(0, 119, ErrorMessage = "AgeRange")]
        public int? Age { get; set; }

        public DateTime? Birthday { get; set; }

        [LRequired(ErrorMessage = "MobilePhoneIsRequired")]
       // [LRegularExpression(RegularPattern.Mobile, ErrorMessage = "MobileInputFormatTips")]
        public string MobilePhone { get; set; }

        [LRequired(ErrorMessage = "EmailIsRequired")]
        [LRegularExpression(RegularPattern.Email, ErrorMessage = "EmailInputFormatTips")]
        [LDataType(DataType.EmailAddress, ErrorMessage = "EmailInputFormatTips")]
        public string Email { get; set; }

        public string Telephone { get; set; }

        public string ApplicationId { get; set; }

        public string redirect_uri { get; set; }
    }

    public class RegisterSuccessModel
    {
        public string LoginName { get; set; }

        public string ReturnUrl { get; set; }
    }
}