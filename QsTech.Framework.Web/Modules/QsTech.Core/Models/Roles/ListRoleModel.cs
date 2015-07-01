using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QsTech.Core.Models.Roles
{
    public class ListRoleModel
    {
        public ListRoleModel(Role role)
        {
            Id = role.Id;
            Name = role.Name;
            Description = role.Description;
            Category = role.Category;
            BelongsTo = role.BelongsTo;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public string BelongsTo { get; set; }
    }
}