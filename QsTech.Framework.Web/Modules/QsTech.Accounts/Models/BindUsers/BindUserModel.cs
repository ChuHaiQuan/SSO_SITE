using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QsTech.Accounts.Models.Accounts;

namespace QsTech.Accounts.Models.BindUsers
{
    public class BindUserModel
    {
        public BindUserModel()
        {
            
        }
        public BindUserModel(Account account)
        {
            //if(account.Users.Count>0)
            //{
            //    Id = account.Users.FirstOrDefault().Id;
            //    UserId = account.Users.FirstOrDefault().UserId;
            //    UserName = account.Users.FirstOrDefault().UserName;
            //}
            AccountId = account.Id;
            AccountName = account.AccountName;
        }

        public int? Id { get; set; }

        public int AccountId { get; set; }

        public string AccountName { get; set; }

        public int? UserId { get; set; }

        public string UserName { get; set; }

    }
}