using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace QsTech.Authentication.Sso.Models
{
    public class Member
    {
        public int Id { get; set; }

        public MemberType Type { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public Member ParentMember { get; set; }

        public IList<Member> SubMembers { get; set; }
    }
}
