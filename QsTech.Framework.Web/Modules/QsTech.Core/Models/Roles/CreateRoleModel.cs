using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace QsTech.Core.Models.Roles
{
    public class CreateRoleModel
    {
        [Required(ErrorMessage = "角色名不能为空")]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public string BelongsTo { get; set; }

        public IEnumerable<RoleBelongsSystemEntry> BelongsArray { get; set; }
    }
}