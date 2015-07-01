using System;

namespace QsTech.Core.Interface.Account
{
    public  class SsoAccountModel
    {
        public SsoAccountModel()
        {
            
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int? Age { get; set; }

        public string Gender { get; set; }

        public string Telephone { get; set; }

        public string Mobile { get; set; }

        public string Email { get; set; }

        public DateTime? Birthday { get; set; }

        public string AccountName { get; set; }

        public string NickName { get; set; }

    }
}
